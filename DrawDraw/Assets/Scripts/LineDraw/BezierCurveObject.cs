using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveObject : MonoBehaviour
{
    public Transform point0, point1, point2, point3; // Bezier 곡선의 제어점
    public int segmentCount = 50; // 곡선의 세밀함을 결정하는 세그먼트 수
    private LineRenderer lineRenderer; // LineRenderer 컴포넌트

    void Start()
    {
        // LineRenderer 컴포넌트를 가져옴
        lineRenderer = GetComponent<LineRenderer>();

        // LineRenderer의 포인트 수를 설정
        lineRenderer.positionCount = segmentCount + 1;

        // 곡선을 그립니다
        DrawBezierCurve();
    }

    // Bezier 곡선을 계산하고 LineRenderer에 설정하는 메서드
    void DrawBezierCurve()
    {
        // segmentCount 수만큼 반복하여 곡선의 각 포인트를 계산하고 설정
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount; // t 값을 세그먼트 수에 따라 계산
            Vector3 point = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position); // 현재 t 값에 해당하는 Bezier 곡선의 점 계산
            lineRenderer.SetPosition(i, point); // LineRenderer에 계산된 점을 설정
        }
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
        // 모든 제어점이 설정된 경우에만 곡선을 그림
        if (point0 != null && point1 != null && point2 != null && point3 != null)
        {
            // Gizmos로 그릴 선의 색상을 빨간색으로 설정
            Gizmos.color = Color.red;

            // 첫 번째 포인트를 이전 포인트로 초기화
            Vector3 previousPoint = point0.position;

            // 세그먼트 수만큼 반복하여 곡선을 그림
            for (int i = 1; i <= segmentCount; i++)
            {
                // t 값을 세그먼트 수에 따라 계산
                float t = i / (float)segmentCount;

                // 현재 t 값에 해당하는 Bezier 곡선의 점 계산
                Vector3 currentPoint = CalculateBezierPoint(t, point0.position, point1.position, point2.position, point3.position);

                // 이전 점과 현재 점 사이에 선을 그림
                Gizmos.DrawLine(previousPoint, currentPoint);

                // 현재 점을 다음 반복의 이전 점으로 설정
                previousPoint = currentPoint;
            }
        }
    }
}