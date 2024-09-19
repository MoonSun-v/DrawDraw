using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizeCollider : MonoBehaviour
{
    public float scaleFactor = 0.5f;  // ũ�� ��� ���� (0.5�� ���� ��, �ݶ��̴� ũ�⸦ �������� ���)
    private PolygonCollider2D polygonCollider;
    private CircleCollider2D circleCollider;

    void Start()
    {
        // PolygonCollider2D �Ǵ� CircleCollider2D ������Ʈ ��������
        polygonCollider = GetComponent<PolygonCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        if (polygonCollider != null)
        {
            ScalePolygonCollider();
        }
        else if (circleCollider != null)
        {
            ScaleCircleCollider();
        }
        else
        {
            Debug.LogError("No PolygonCollider2D or CircleCollider2D found on the object.");
        }
    }

    // PolygonCollider2D ũ�⸦ ����ϴ� �Լ�
    void ScalePolygonCollider()
    {
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            Vector2[] pathPoints = polygonCollider.GetPath(i);

            // �� ����Ʈ�� scaleFactor�� ���� ���
            for (int j = 0; j < pathPoints.Length; j++)
            {
                pathPoints[j] *= scaleFactor;
            }

            // ������ ��θ� �ٽ� ����
            polygonCollider.SetPath(i, pathPoints);
        }

        Debug.Log("PolygonCollider2D scaled.");
    }

    // CircleCollider2D ũ�⸦ ����ϴ� �Լ�
    void ScaleCircleCollider()
    {
        // CircleCollider2D�� �������� scaleFactor�� ���� ���
        circleCollider.radius *= scaleFactor;

        Debug.Log("CircleCollider2D scaled.");
    }
}