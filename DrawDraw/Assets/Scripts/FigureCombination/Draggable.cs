using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb2D;
    private Camera mainCamera; // 카메라를 참조할 변수

    // square 오브젝트의 Collider2D를 참조
    public GameObject squareObject;
    private Collider2D squareCollider;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // 카메라를 설정

        if (squareObject != null)
        {
            squareCollider = squareObject.GetComponent<Collider2D>();

            if (squareCollider == null)
            {
                Debug.LogError("Square object does not have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogError("Square object is not assigned.");
        }
    }

    void OnMouseDown()
    {
        // 드래그 시작
        isDragging = true;
        Debug.Log("마우스 다운"+isDragging);
        // 오브젝트와 마우스 포인터 간의 차이를 계산
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        // 드래그 종료
        isDragging = false;
        Debug.Log("마우스 업" + isDragging);
    }

    void Update()
    {
        // 드래그 중일 때만 오브젝트 위치 업데이트
        if (isDragging && squareCollider != null)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            // squareCollider의 경계 내에서만 이동 가능하도록 제한
            Bounds bounds = squareCollider.bounds;

            // x 좌표 제한
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);

            // y 좌표 제한
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

            rb2D.MovePosition(targetPosition);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 마우스 위치를 화면 좌표에서 월드 좌표로 변환
        Vector3 mousePoint = Input.mousePosition;
        //mousePoint.z = Camera.main.transform.position.z * -1; // 2D에서는 z 축을 고려하지 않음
        mousePoint.z = -mainCamera.transform.position.z; // 카메라와의 거리 설정

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}