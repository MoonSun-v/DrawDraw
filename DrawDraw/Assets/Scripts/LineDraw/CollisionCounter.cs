using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    private int collisionCount = 0;

    private bool isDragging = false;
    private Vector3 lastPosition;

    private bool[] hasCollided;

    public Text scoreText;

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
                                scoreText.text = collisionCount.ToString();
                                Debug.Log("Collision Count: " + collisionCount);
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
}
