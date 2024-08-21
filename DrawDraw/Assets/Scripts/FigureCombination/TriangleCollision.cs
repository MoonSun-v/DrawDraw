using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollision : MonoBehaviour
{
    public GameObject parallelogram; // 평행사변형 오브젝트

    private Vector3 initialTriangle1Position; // 첫 번째 삼각형의 초기 위치
    private Vector3 initialTriangle2Position; // 두 번째 삼각형의 초기 위치
    private Vector3 parallelogramOffset;      // 평행사변형의 중심과 삼각형의 위치 차이

    void Start()
    {
        // parallelogram 변수가 인스펙터에서 할당되지 않은 경우 자동으로 할당
        if (parallelogram == null)
        {
            parallelogram = transform.parent.gameObject;
        }

        // 평행사변형 내의 두 삼각형의 초기 위치를 기록
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

        // 충돌한 오브젝트가 base 태그를 가지고 있는지 확인합니다.
        if (collision.CompareTag("baseSquare"))
        {
            Debug.Log("collision");
            // 충돌한 오브젝트의 위치로 평행사변형을 이동합니다.
            Vector3 targetPosition = collision.transform.position;
            MoveParallelogram(targetPosition);
        }
    }

    void MoveParallelogram(Vector3 targetPosition)
    {
        // 평행사변형의 중심 위치를 설정합니다.
        Vector3 centerPosition = targetPosition - parallelogramOffset;
        parallelogram.transform.position = centerPosition;
    }
}