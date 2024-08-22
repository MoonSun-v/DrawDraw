using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollision : MonoBehaviour
{
    public Transform otherTriangle;  // �Բ� ������ �ٸ� �ﰢ�� ������Ʈ
    private Vector3 initialOffset;   // ó�� �ﰢ���� ���� ������

    void Start()
    {
        // �ٸ� �ﰢ������ �ʱ� ������ ���
        initialOffset = otherTriangle.position - transform.position;
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

                // ���� �ﰢ���� ���� ����� baseSquare ��ġ�� �̵�
                transform.position = newPosition;

                // �ٸ� �ﰢ���� ������ ������ �̵��Ͽ� ����纯���� ����� ����
                otherTriangle.position += displacement;

                //Debug.Log(gameObject.name + " moved to " + newPosition + " and " + otherTriangle.name + " moved to " + otherTriangle.position);
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