using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonCollision : MonoBehaviour
{
    public Transform[] triangles;  // �������� �����ϴ� ���� ���� �ﰢ�� ������Ʈ

    private Vector3[] initialOffsets;  // ó�� �ﰢ���� ���� ������
    private float fixedZPosition;  // Z�� ��ġ ����

    void Start()
    {
        // �ﰢ���� ���� �ʱ� ������ ���
        initialOffsets = new Vector3[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            initialOffsets[i] = triangles[i].position - transform.position;
        }

        // �ʱ� ���� - ���� Z�� ��ġ ����
        fixedZPosition = transform.position.z;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // "baseSquare" �±׸� ���� ������Ʈ�� �浹�� ����
        if (collision.CompareTag("baseSquare"))
        {
            // ���� ����� "baseSquare" ������Ʈ�� ã��
            GameObject nearestBaseSquare = FindNearestBaseSquare();

            if (nearestBaseSquare != null)
            {
                // ���� ����� baseSquare�� ��ġ�� �̵�
                Vector3 newPosition = nearestBaseSquare.transform.position;
                Vector3 displacement = newPosition - transform.position;

                // Z�� ��ġ�� �����ϸ鼭 �������� �����ϴ� ��� �ﰢ���� �̵�
                foreach (Transform triangle in triangles)
                {
                    Vector3 newTrianglePosition = triangle.position + displacement;
                    newTrianglePosition.z = fixedZPosition;  // Z�� ��ġ ����
                    triangle.position = newTrianglePosition;
                }

                Debug.Log("Hexagon moved to " + newPosition);
            }
        }
    }

    GameObject FindNearestBaseSquare()
    {
        GameObject[] baseSquares = GameObject.FindGameObjectsWithTag("baseSquare");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject baseSquare in baseSquares)
        {
            float distance = Vector3.Distance(currentPosition, baseSquare.transform.position);
            if (distance < minDistance)
            {
                nearest = baseSquare;
                minDistance = distance;
            }
        }

        return nearest;
    }
}