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
        // �ݶ��̴� ����ŭ �迭 �ʱ�ȭ
        hasCollided = new bool[2]; //2�� �ݶ��̴��� ����
    }

    private void Update()
    {
        // ���콺 �Է� �Ǵ� ��ġ �Է��� �޾Ƽ� �巡�׸� ����
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && !isDragging)
        {
            isDragging = true;
            lastPosition = GetInputWorldPosition();

            // �浹 ���� �ʱ�ȭ
            for (int i = 0; i < hasCollided.Length; i++)
            {
                hasCollided[i] = false;
            }
        }

        if ((Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && isDragging)
        {
            isDragging = false;
        }

        // �巡�� ���� �� �浹�� �����Ͽ� �浹 Ƚ���� ����
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
                    // �浹�� �ݶ��̴��� �ε��� ��������
                    int colliderIndex_in = hit.collider.CompareTag("baseSquare_inside") ? 0 : 1;

                    // �̹� �浹�� �������� Ȯ��
                    if (!hasCollided[colliderIndex_in])
                    {
                        hasCollided[colliderIndex_in] = true; // �ش� �ݶ��̴��� �浹������ ǥ��

                        // �� �ݶ��̴� ��� �浹�� ��찡 �ƴ϶�� �浹 Ƚ�� ����
                        if (!(hasCollided[0] && hasCollided[1]))
                        {
                            collisionCount++;
                            Debug.Log("Collision Count: " + collisionCount);

                            scoreText.text = Score(collisionCount, pass);

                            // ���� Base �±׿� �浹�ϸ� ���� ���� ���� ��ȣ ����
                            //SetIsSafe(true);
                        }
                        else // �� �ݶ��̴� ��� �浹�� ���
                        {
                            // �浹 ���� �ʱ�ȭ
                            for (int i = 0; i < hasCollided.Length; i++)
                            {
                                hasCollided[i] = false;
                            }
                        }
                    }
                }

                if (hit.collider != null && (hit.collider.CompareTag("baseSquare_inside") || hit.collider.CompareTag("baseSquare_outside")))
                {
                    // ���� Base �±׿� �浹�ϸ� ���� ���� ���� ��ȣ ����
                    SetIsSafe(true);
                }


                lastPosition = currentPosition;
            }
        }

    }

    // ���콺 �Ǵ� ��ġ �Է� ��ġ�� ���� ��ǥ�� ��ȯ�ϴ� �޼���
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

    // �̱��� �������� �ν��Ͻ��� ����
    public static CollisionCounter Instance { get; private set; }

    private bool isSafe = false;
    void Awake()
    {
        // �̱��� �ν��Ͻ� ����, �ν��Ͻ��� �̹� �����ϴ��� Ȯ��
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ν��Ͻ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }

    }

    // ���� ������ ����(Base �±�)�� �浹�ߴ��� ����
    public void SetIsSafe(bool safe)
    {
        isSafe = safe;
    }

    // ���� ������ ����(Base �±�)�� �浹�ߴ��� Ȯ��
    public bool IsSafe()
    {
        return isSafe;
    }

    // ���� ���� ó��
    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");
        //SceneManager.LoadScene("GameOverScene");
    }

    public int maxCollisions = 20; // ���� �浹 Ƚ�� (20�� �浹�ϸ� 0��)
    public float maxScore = 100f; // ���� ���� (�ִ� 100��)
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
                // �浹 Ƚ���� ���� ���� ���
                maxScore = 100 * (float)(maxCollisions - collisionCount) / maxCollisions;
                // ������ ������ �������� ���� ����
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
