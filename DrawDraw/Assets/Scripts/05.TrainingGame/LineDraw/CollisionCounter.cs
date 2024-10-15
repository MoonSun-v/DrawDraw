using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    private int collisionCount = 0;

    private bool isDragging = false;
    private Vector3 lastPosition;

    private bool[] hasCollided;

    public Text scoreText;

    private bool pass=false;

    private void Start()
    {
        // 콜라이더 수만큼 배열 초기화
        hasCollided = new bool[2]; //2는 콜라이더의 개수
    }

    private void Update()
    {
        // 마우스 입력 또는 터치 입력을 받아서 드래그를 감지
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && !isDragging)
        {
            isDragging = true;
            lastPosition = GetInputWorldPosition();

            // 충돌 여부 초기화
            for (int i = 0; i < hasCollided.Length; i++)
            {
                hasCollided[i] = false;
            }
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && isDragging)
        {
            isDragging = false;
        }

        // 드래그 중일 때 충돌을 감지하여 충돌 횟수를 증가
        if (isDragging)
        {
            Vector3 currentPosition = GetInputWorldPosition();

            if (currentPosition != lastPosition)
            {
                RaycastHit2D hit = Physics2D.Linecast(lastPosition, currentPosition);

                if (hit.collider != null && hit.collider.CompareTag("baseSquare"))
                {
                    pass = true;
                }

                if (hit.collider != null && (hit.collider.CompareTag("baseSquare_inside") || hit.collider.CompareTag("baseSquare_outside")))
                {
                    // 충돌한 콜라이더의 인덱스 가져오기
                    int colliderIndex_in = hit.collider.CompareTag("baseSquare_inside") ? 0 : 1;

                    // 이미 충돌한 상태인지 확인
                    if (!hasCollided[colliderIndex_in])
                    {
                        hasCollided[colliderIndex_in] = true; // 해당 콜라이더에 충돌했음을 표시

                        // 두 콜라이더 모두 충돌한 경우가 아니라면 충돌 횟수 증가
                        if (!(hasCollided[0] && hasCollided[1]))
                        {
                            collisionCount++;
                            Debug.Log("Collision Count: " + collisionCount);

                            scoreText.text = Score(collisionCount, pass);

                            // 선이 Base 태그와 충돌하면 게임 오버 방지 신호 전송
                            //SetIsSafe(true);
                        }
                        else // 두 콜라이더 모두 충돌한 경우
                        {
                            // 충돌 여부 초기화
                            for (int i = 0; i < hasCollided.Length; i++)
                            {
                                hasCollided[i] = false;
                            }
                        }
                    }
                }

                if (hit.collider != null && (hit.collider.CompareTag("baseSquare_inside") || hit.collider.CompareTag("baseSquare_outside")))
                {
                    // 선이 Base 태그와 충돌하면 게임 오버 방지 신호 전송
                    SetIsSafe(true);
                }


                lastPosition = currentPosition;
            }
        }

    }

    // 마우스 또는 터치 입력 위치를 월드 좌표로 변환하는 메서드
    private Vector3 GetInputWorldPosition()
    {
        if (Input.touchCount > 0)
        {
            return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    // 싱글톤 패턴으로 인스턴스를 관리
    public static CollisionCounter Instance { get; private set; }

    private bool isSafe = false;
    void Awake()
    {
        // 싱글톤 인스턴스 설정, 인스턴스가 이미 존재하는지 확인
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 씬 전환 시에도 인스턴스를 유지
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }

    }

    // 선이 안전한 영역(Base 태그)과 충돌했는지 설정
    public void SetIsSafe(bool safe)
    {
        isSafe = safe;
    }

    // 선이 안전한 영역(Base 태그)과 충돌했는지 확인
    public bool IsSafe()
    {
        return isSafe;
    }

    // 게임 오버 처리
    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        //SceneManager.LoadScene("GameOverScene");
    }

    public int maxCollisions = 20; // 기준 충돌 횟수 (20번 충돌하면 0점)
    public float maxScore = 100f; // 현재 점수 (최대 100점)
    private string Score(int collisionCount, bool pass)
    {
        if (IsSafe())
        {
            if(collisionCount < 10 && pass)
            {
                maxScore = 100;
            }
            else
            {
                // 충돌 횟수에 따른 점수 계산
                maxScore = 100 * (float)(maxCollisions - collisionCount) / maxCollisions;
                // 점수가 음수로 내려가는 것을 방지
                if (maxScore < 0)
                {
                    maxScore = 0;
                }
            }

            scoreText.text = maxScore.ToString("F0");
        }
        else
        {
            scoreText.text = "0";
        }
        return scoreText.text;
    }
}
