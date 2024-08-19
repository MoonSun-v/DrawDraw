using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // 특정 영역을 정의하는 게임 오브젝트 (경계 영역)
    public GameObject boundaryObject;
    private Collider2D boundaryCollider;

    void Start()
    {
        // boundaryObject로부터 Collider2D 컴포넌트를 가져옴
        if (boundaryObject != null)
        {
            boundaryCollider = boundaryObject.GetComponent<Collider2D>();

            // Collider2D가 존재하지 않으면 경고 메시지 출력
            if (boundaryCollider == null)
            {
                Debug.LogWarning("Boundary Object does not have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogWarning("Boundary Object is not assigned.");
        }
    }

    void Update()
    {
        if (boundaryCollider != null)
        {
            // 현재 오브젝트의 Collider2D 가져오기
            Collider2D objectCollider = GetComponent<Collider2D>();

            if (objectCollider != null)
            {
                // 오브젝트가 경계 콜라이더 내에 있는지 확인
                if (!boundaryCollider.bounds.Contains(objectCollider.bounds.min) ||
                    !boundaryCollider.bounds.Contains(objectCollider.bounds.max))
                {
                    Destroy(gameObject); // 경계 밖으로 나가면 오브젝트 삭제
                }
            }
        }
    }
}
