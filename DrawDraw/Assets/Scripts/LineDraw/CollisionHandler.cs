using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{// ���� ������ ������Ʈ ����
    private LineRenderer lineRenderer;

    // �浹 ���θ� ������ ����
    private bool collided = false;

    // �浹 Ƚ���� ������ ����
    private int collisionCount = 0;

    // �ϼ� ��ư
    public Button completeButton;

    private void Start()
    {
        // ���� ������ ������Ʈ ��������
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        // �̹� �浹�� ��� �� �̻� �浹 �˻縦 ���� ����
        if (collided)
            return;

        // ���� �������� �������� ���� ��������
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        // ���ΰ� �±װ� "baseSquare"�� ������Ʈ ���� �浹 �˻�
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint);

        // �浹�� �߻��ߴ��� Ȯ��
        if (hit.collider != null && hit.collider.CompareTag("baseSquare"))
        {
            // �浹�� ������Ʈ�� �̸��� ����� ���
            Debug.Log("�浹�� ������Ʈ: " + hit.collider.gameObject.name);

            // �浹������ ǥ��
            collided = true;

            // �浹 Ƚ�� ����
            collisionCount++;

        }
        else
        {
            // �簢�� ������ ������Ƿ� �浹������ �ʱ�ȭ
            collided = false;
        }
    }

    // �ϼ� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void ShowCollisionCount()
    {
        // �浹 Ƚ�� ���
        Debug.Log("�浹 Ƚ��: " + collisionCount);
    }
}
