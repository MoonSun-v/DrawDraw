using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonCollision : MonoBehaviour
{
    public Transform[] triangles;  // 육각형을 구성하는 여섯 개의 삼각형 오브젝트

    private Vector3[] initialOffsets;  // 처음 삼각형들 간의 오프셋
    private float fixedZPosition;  // Z축 위치 고정

    void Start()
    {
        // 삼각형들 간의 초기 오프셋 계산
        initialOffsets = new Vector3[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            initialOffsets[i] = triangles[i].position - transform.position;
        }

        // 초기 설정 - 현재 Z축 위치 고정
        fixedZPosition = transform.position.z;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // "baseSquare" 태그를 가진 오브젝트와 충돌을 감지
        if (collision.CompareTag("baseSquare"))
        {
            // 가장 가까운 "baseSquare" 오브젝트를 찾음
            GameObject nearestBaseSquare = FindNearestBaseSquare();

            if (nearestBaseSquare != null)
            {
                // 가장 가까운 baseSquare의 위치로 이동
                Vector3 newPosition = nearestBaseSquare.transform.position;
                Vector3 displacement = newPosition - transform.position;

                // Z축 위치를 유지하면서 육각형을 구성하는 모든 삼각형을 이동
                foreach (Transform triangle in triangles)
                {
                    Vector3 newTrianglePosition = triangle.position + displacement;
                    newTrianglePosition.z = fixedZPosition;  // Z축 위치 고정
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