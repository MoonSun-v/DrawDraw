using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollision : MonoBehaviour
{
    public GameObject parallelogram; // ����纯�� ������Ʈ

    private Vector3 initialTriangle1Position; // ù ��° �ﰢ���� �ʱ� ��ġ
    private Vector3 initialTriangle2Position; // �� ��° �ﰢ���� �ʱ� ��ġ
    private Vector3 parallelogramOffset;      // ����纯���� �߽ɰ� �ﰢ���� ��ġ ����

    void Start()
    {
        // parallelogram ������ �ν����Ϳ��� �Ҵ���� ���� ��� �ڵ����� �Ҵ�
        if (parallelogram == null)
        {
            parallelogram = transform.parent.gameObject;
        }

        // ����纯�� ���� �� �ﰢ���� �ʱ� ��ġ�� ���
        Transform triangle1 = parallelogram.transform.Find("Triangle (1)");
        Transform triangle2 = parallelogram.transform.Find("Triangle (2)");

        if (triangle1 != null && triangle2 != null)
        {
            initialTriangle1Position = triangle1.localPosition;
            initialTriangle2Position = triangle2.localPosition;

            parallelogramOffset = (initialTriangle1Position + initialTriangle2Position) / 2;
        }
        else
        {
            Debug.LogError("Triangle objects not found in Parallelogram.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        // �浹�� ������Ʈ�� base �±׸� ������ �ִ��� Ȯ���մϴ�.
        if (collision.CompareTag("baseSquare"))
        {
            Debug.Log("collision");
            // �浹�� ������Ʈ�� ��ġ�� ����纯���� �̵��մϴ�.
            Vector3 targetPosition = collision.transform.position;
            MoveParallelogram(targetPosition);
        }
    }

    void MoveParallelogram(Vector3 targetPosition)
    {
        // ����纯���� �߽� ��ġ�� �����մϴ�.
        Vector3 centerPosition = targetPosition - parallelogramOffset;
        parallelogram.transform.position = centerPosition;
    }
}