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
                                scoreText.text = collisionCount.ToString();
                                Debug.Log("Collision Count: " + collisionCount);
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
}
