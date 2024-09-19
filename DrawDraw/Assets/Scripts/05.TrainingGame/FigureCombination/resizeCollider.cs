using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resizeCollider : MonoBehaviour
{
    public float scaleFactor = 0.5f;  // 크기 축소 비율 (0.5로 설정 시, 콜라이더 크기를 절반으로 축소)
    private PolygonCollider2D polygonCollider;
    private CircleCollider2D circleCollider;

    void Start()
    {
        // PolygonCollider2D 또는 CircleCollider2D 컴포넌트 가져오기
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

    // PolygonCollider2D 크기를 축소하는 함수
    void ScalePolygonCollider()
    {
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            Vector2[] pathPoints = polygonCollider.GetPath(i);

            // 각 포인트를 scaleFactor에 따라 축소
            for (int j = 0; j < pathPoints.Length; j++)
            {
                pathPoints[j] *= scaleFactor;
            }

            // 수정된 경로를 다시 설정
            polygonCollider.SetPath(i, pathPoints);
        }

        Debug.Log("PolygonCollider2D scaled.");
    }

    // CircleCollider2D 크기를 축소하는 함수
    void ScaleCircleCollider()
    {
        // CircleCollider2D의 반지름을 scaleFactor에 따라 축소
        circleCollider.radius *= scaleFactor;

        Debug.Log("CircleCollider2D scaled.");
    }
}