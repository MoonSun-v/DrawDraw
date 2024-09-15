using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollision : MonoBehaviour
{
    public Transform otherTriangle;  // 함께 움직일 다른 삼각형 오브젝트
    private Vector3 initialOffset;   // 처음 삼각형들 간의 오프셋

    void Start()
    {
        // 다른 삼각형과의 초기 오프셋 계산
        initialOffset = otherTriangle.position - transform.position;
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

                // 현재 삼각형을 가장 가까운 baseSquare 위치로 이동
                transform.position = newPosition;

                // 다른 삼각형도 동일한 변위로 이동하여 평행사변형의 모양을 유지
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