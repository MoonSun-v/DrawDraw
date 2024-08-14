using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveLine : MonoBehaviour
{
    public Transform[] controlPoints; // 16개의 제어점을 담을 배열
    public int segmentCount = 50; // 곡선의 세밀함을 결정하는 세그먼트 수
    private LineRenderer lineRenderer; // LineRenderer 컴포넌트

    private EdgeCollider2D edgeCollider;

    void Start()
    {
        // 제어점이 16개인지 확인
        if (controlPoints.Length != 16)
        {
            Debug.LogError("You must assign exactly 16 control points.");
            return;
        }

        // LineRenderer 컴포넌트를 가져옴
        lineRenderer = GetComponent<LineRenderer>();

        // EdgeCollider2D 컴포넌트를 가져오거나 추가합니다.
        edgeCollider = GetComponent<EdgeCollider2D>();
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }

        // LineRenderer의 포인트 수를 설정
        lineRenderer.positionCount = (segmentCount + 1) * 4; // 4개의 곡선

        // Bezier 곡선을 그림
        DrawBezierCurves();
    }

    // 4개의 Bezier 곡선을 계산하고 LineRenderer에 설정하는 메서드
    void DrawBezierCurves()
    {
        int index = 0; // LineRenderer에 설정할 포인트 인덱스
        List<Vector2> points = new List<Vector2>();


        // 4개의 Bezier 곡선을 그리기 위해 루프
        for (int i = 0; i < 4; i++)
        {
            // 4개의 제어점을 가져옴
            Vector3 p0 = controlPoints[i * 4].position;
            Vector3 p1 = controlPoints[i * 4 + 1].position;
            Vector3 p2 = controlPoints[i * 4 + 2].position;
            Vector3 p3 = controlPoints[i * 4 + 3].position;

            // 각 곡선의 포인트를 계산
            for (int j = 0; j <= segmentCount; j++)
            {
                float t = j / (float)segmentCount;
                Vector3 point = CalculateBezierPoint(t, p0, p1, p2, p3);

                if (index < lineRenderer.positionCount) // 인덱스가 범위 내인지 확인
                {
                    lineRenderer.SetPosition(index++, point);
                    // 현재 점을 2D 포인트로 변환합니다.
                    points.Add(new Vector2(point.x, point.y));
                }
                else
                {
                    Debug.LogError("Index out of bounds for LineRenderer.SetPosition.");
                    return;
                }
            }
        }
        // EdgeCollider2D에 포인트를 설정합니다.
        edgeCollider.points = points.ToArray();
    }

    // t 값을 기준으로 Bezier 곡선의 특정 점을 계산하는 메서드
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;        // 1 - t 값을 계산
        float tt = t * t;       // t의 제곱을 계산
        float uu = u * u;       // u의 제곱을 계산
        float uuu = uu * u;     // u의 세제곱을 계산
        float ttt = tt * t;     // t의 세제곱을 계산

        // Bezier 곡선 공식에 따라 곡선의 포인트 계산
        Vector3 p = uuu * p0;   // (1-t)^3 * p0
        p += 3 * uu * t * p1;   // 3 * (1-t)^2 * t * p1
        p += 3 * u * tt * p2;   // 3 * (1-t) * t^2 * p2
        p += ttt * p3;          // t^3 * p3

        return p;               // 계산된 점을 반환
    }

    // 에디터 모드에서 Bezier 곡선을 Gizmos로 그리는 메서드
    void OnDrawGizmos()
    {
        if (controlPoints.Length == 16)
        {
            Gizmos.color = Color.red;

            // 4개의 Bezier 곡선을 그리기 위해 루프
            for (int i = 0; i < 4; i++)
            {
                Vector3 p0 = controlPoints[i * 4].position;
                Vector3 p1 = controlPoints[i * 4 + 1].position;
                Vector3 p2 = controlPoints[i * 4 + 2].position;
                Vector3 p3 = controlPoints[i * 4 + 3].position;

                Vector3 previousPoint = p0;

                // 세그먼트 수만큼 반복하여 곡선을 그림
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
