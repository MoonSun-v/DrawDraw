using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveObject : MonoBehaviour
{
    public Transform point0, point1, point2, point3; // Bezier ��� ������
    public int segmentCount = 50; // ��� �������� �����ϴ� ���׸�Ʈ ��
    private LineRenderer lineRenderer; // LineRenderer ������Ʈ

    void Start()
    {
        // LineRenderer ������Ʈ�� ������
        lineRenderer = GetComponent<LineRenderer>();

        // LineRenderer�� ����Ʈ ���� ����
        lineRenderer.positionCount = segmentCount + 1;

        // ��� �׸��ϴ�
        DrawBezierCurve();
    }

    // Bezier ��� ����ϰ� LineRenderer�� �����ϴ� �޼���
    void DrawBezierCurve()
    {
        // segmentCount ����ŭ �ݺ��Ͽ� ��� �� ����Ʈ�� ����ϰ� ����
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount; // t ���� ���׸�Ʈ ���� ���� ���
            Vector3 point = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position); // ���� t ���� �ش��ϴ� Bezier ��� �� ���
            lineRenderer.SetPosition(i, point); // LineRenderer�� ���� ���� ����
        }
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
        // ��� �������� ������ ��쿡�� ��� �׸�
        if (point0 != null && point1 != null && point2 != null && point3 != null)
        {
            // Gizmos�� �׸� ���� ������ ���������� ����
            Gizmos.color = Color.red;

            // ù ��° ����Ʈ�� ���� ����Ʈ�� �ʱ�ȭ
            Vector3 previousPoint = point0.position;

            // ���׸�Ʈ ����ŭ �ݺ��Ͽ� ��� �׸�
            for (int i = 1; i <= segmentCount; i++)
            {
                // t ���� ���׸�Ʈ ���� ���� ���
                float t = i / (float)segmentCount;

                // ���� t ���� �ش��ϴ� Bezier ��� �� ���
                Vector3 currentPoint = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);

                // ���� ���� ���� �� ���̿� ���� �׸�
                Gizmos.DrawLine(previousPoint, currentPoint);

                // ���� ���� ���� �ݺ��� ���� ������ ����
                previousPoint = currentPoint;
            }
        }
    }
}