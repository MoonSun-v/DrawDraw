using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectOnCollision : MonoBehaviour
{
    public string baseTag = "baseSquare";  // "base" 태그를 가진 오브젝트를 찾기 위해 사용할 태그 이름

    void Update()
    {
        // 도형1이 "base" 태그를 가진 오브젝트와 충돌했는지 확인
        if (IsObjectAOnAnyBaseObject())
        {
            // Scale 원상복구
            Vector3 scale = transform.localScale;
            if (scale.y < 0)
            {
                scale.y = -Mathf.Abs(scale.y);  // y 값 음수x음수 -> 양수
                transform.localScale = scale;
            }
            // 도형1을 가장 가까운 "base" 태그를 가진 오브젝트의 위치로 이동
            MoveObjectAToClosestBaseObject(baseTag);
        }
    }

    // 도형1이 "base" 태그를 가진 오브젝트와 충돌했는지 확인하는 메서드
    bool IsObjectAOnAnyBaseObject()
    {

        Collider2D colliderA = GetComponent<Collider2D>();

        // "base" 태그를 가진 모든 오브젝트를 찾음
        GameObject[] baseObjects = GameObject.FindGameObjectsWithTag(baseTag);

        // 각 "base" 태그 오브젝트와의 충돌 여부 확인
        foreach (GameObject baseObject in baseObjects)
        {
            Collider2D colliderB = baseObject.GetComponent<Collider2D>();
            if (colliderA != null && colliderB != null && colliderA.IsTouching(colliderB))
            {
                // 충돌한 경우 콘솔에 메시지 출력
                //Debug.Log("도형1이 'base' 태그를 가진 오브젝트와 충돌했습니다: " + baseObject.name);
                return true;
            }
        }

        return false;
    }

        // 충돌한 "base" 태그 오브젝트 중 가장 가까운 오브젝트의 위치로 도형1을 이동하는 메서드
        void MoveObjectAToClosestBaseObject(string tag)
    {
        Collider2D colliderA = GetComponent<Collider2D>();
        Transform closestBaseObject = null;
        float closestDistance = float.MaxValue;

        // "base" 태그를 가진 모든 오브젝트를 순회하며 가장 가까운 충돌한 오브젝트 찾기
        GameObject[] baseObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject baseObject in baseObjects)
        {
            Collider2D colliderB = baseObject.GetComponent<Collider2D>();
            if (colliderA != null && colliderB != null && colliderA.IsTouching(colliderB))
            {
                // 도형1과 "base" 오브젝트 사이의 거리 계산
                float distance = Vector2.Distance(transform.position, baseObject.transform.position);
                if (distance < closestDistance)
                {
                    // 더 가까운 "base" 오브젝트를 찾았을 경우 업데이트
                    closestDistance = distance;
                    closestBaseObject = baseObject.transform;
                }
            }
        }

        // 가장 가까운 "base" 오브젝트의 위치로 도형1을 이동
        if (closestBaseObject != null)
        {
            transform.position = closestBaseObject.position;
        }
    }
}