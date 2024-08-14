using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLine : MonoBehaviour
{
    public Transform[] controlPoints; // 16���� �������� ���� �迭
    public int segmentCount = 50; // ��� �������� �����ϴ� ���׸�Ʈ ��
    private LineRenderer lineRenderer; // LineRenderer ������Ʈ

    private EdgeCollider2D edgeCollider;

    void Start()
    {
        // �������� 16������ Ȯ��
        if (controlPoints.Length != 16)
        {
            Debug.LogError("You must assign exactly 16 control points.");
            return;
        }

        // LineRenderer ������Ʈ�� ������
        lineRenderer = GetComponent<LineRenderer>();

        // EdgeCollider2D ������Ʈ�� �������ų� �߰��մϴ�.
        edgeCollider = GetComponent<EdgeCollider2D>();
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }

        // LineRenderer�� ����Ʈ ���� ����
        lineRenderer.positionCount = (segmentCount + 1) * 4; // 4���� �

        // Bezier ��� �׸�
        DrawBezierCurves();
    }

    // 4���� Bezier ��� ����ϰ� LineRenderer�� �����ϴ� �޼���
    void DrawBezierCurves()
    {
        int index = 0; // LineRenderer�� ������ ����Ʈ �ε���
        List<Vector2> points = new List<Vector2>();


        // 4���� Bezier ��� �׸��� ���� ����
        for (int i = 0; i < 4; i++)
        {
            // 4���� �������� ������
            Vector3 p0 = controlPoints[i * 4].position;
            Vector3 p1 = controlPoints[i * 4 + 1].position;
            Vector3 p2 = controlPoints[i * 4 + 2].position;
            Vector3 p3 = controlPoints[i * 4 + 3].position;

            // �� ��� ����Ʈ�� ���
            for (int j = 0; j <= segmentCount; j++)
            {
                float t = j / (float)segmentCount;
                Vector3 point = CalculateBezierPoint(t, p0, p1, p2, p3);

                if (index < lineRenderer.positionCount) // �ε����� ���� ������ Ȯ��
                {
                    lineRenderer.SetPosition(index++, point);
                    // ���� ���� 2D ����Ʈ�� ��ȯ�մϴ�.
                    points.Add(new Vector2(point.x, point.y));
                }
                else
                {
                    Debug.LogError("Index out of bounds for LineRenderer.SetPosition.");
                    return;
                }
            }
        }
        // EdgeCollider2D�� ����Ʈ�� �����մϴ�.
        edgeCollider.points = points.ToArray();
    }

    // t ���� �������� Bezier ��� Ư�� ���� ����ϴ� �޼���
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;        // 1 - t ���� ���
        float tt = t * t;       // t�� ������ ���
        float uu = u * u;       // u�� ������ ���
        float uuu = uu * u;     // u�� �������� ���
        float ttt = tt * t;     // t�� �������� ���

        // Bezier � ���Ŀ� ���� ��� ����Ʈ ���
        Vector3 p = uuu * p0;   // (1-t)^3 * p0
        p += 3 * uu * t * p1;   // 3 * (1-t)^2 * t * p1
        p += 3 * u * tt * p2;   // 3 * (1-t) * t^2 * p2
        p += ttt * p3;          // t^3 * p3

        return p;               // ���� ���� ��ȯ
    }

    // ������ ��忡�� Bezier ��� Gizmos�� �׸��� �޼���
    void OnDrawGizmos()
    {
        if (controlPoints.Length == 16)
        {
            Gizmos.color = Color.red;

            // 4���� Bezier ��� �׸��� ���� ����
            for (int i = 0; i < 4; i++)
            {
                Vector3 p0 = controlPoints[i * 4].position;
                Vector3 p1 = controlPoints[i * 4 + 1].position;
                Vector3 p2 = controlPoints[i * 4 + 2].position;
                Vector3 p3 = controlPoints[i * 4 + 3].position;

                Vector3 previousPoint = p0;

                // ���׸�Ʈ ����ŭ �ݺ��Ͽ� ��� �׸�
                for (int j = 1; j <= segmentCount; j++)
                {
                    float t = j / (float)segmentCount;
                    Vector3 currentPoint = CalculateBezierPoint(t, p0, p1, p2, p3);
                    Gizmos.DrawLine(previousPoint, currentPoint);
                    previousPoint = currentPoint;
                }
            }
        }
    }
}
