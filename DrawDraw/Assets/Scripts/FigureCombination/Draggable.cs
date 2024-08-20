using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // 드래그 시 마우스/터치와 도형 간의 위치 차이
    private Rigidbody2D rb2D;
    private Camera mainCamera; // 카메라를 참조할 변수

    // 드래그 범위를 제한할 square 오브젝트의 Collider2D
    public GameObject squareObject;
    private Collider2D squareCollider;

    private float lastTapTime; // 마지막 입력 시간
    private const float doubleTapThreshold = 0.3f; // 더블 클릭/터치 간의 시간 간격 (초)

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

    void Update()
    {
        // 입력을 확인하여 더블 클릭 또는 더블 터치 처리
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < doubleTapThreshold)
            {
                // 더블 클릭/터치로 간주
                ToggleScale();
            }
            lastTapTime = currentTime;
        }

        // 마우스 클릭 또는 터치 입력이 있는지 확인
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // 입력 위치를 월드 좌표로 변환

            if (squareCollider != null && IsPointerOverCollider(mouseOrTouchPosition))
            {
                isDragging = true; // 드래그 상태로 전환
                offset = transform.position - mouseOrTouchPosition; // 마우스/터치와 도형 간의 위치 차이 계산
            }
        }

        // 드래그 중일 때 도형 위치 업데이트
        else if ((Input.GetMouseButton(0) || Input.touchCount > 0) && isDragging)
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // 입력 위치를 월드 좌표로 변환
            Vector3 targetPosition = mouseOrTouchPosition + offset; // 목표 위치 계산

            // squareCollider의 경계 내에서만 이동 가능하도록 제한
            Bounds bounds = squareCollider.bounds;
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x); // x 좌표 제한
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y); // y 좌표 제한
            
            rb2D.MovePosition(targetPosition); // Rigidbody2D를 사용하여 도형의 위치를 이동

        }

        // 드래그 종료
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount == 0 && isDragging))
        {
            isDragging = false; // 드래그 상태를 종료
        }
    }

    private Vector3 GetInputWorldPosition()
    {
        Vector3 inputPosition;

        if (Input.touchCount > 0)
        {
            // 터치 입력이 있을 경우 터치 위치를 가져옴
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            // 마우스 입력이 있을 경우 마우스 위치를 가져옴
            inputPosition = Input.mousePosition;
        }

        // z 값은 카메라와의 거리 설정 (2D에서는 z 축을 고려하지 않음)
        inputPosition.z = -mainCamera.transform.position.z;

        return mainCamera.ScreenToWorldPoint(inputPosition); // 화면 좌표를 월드 좌표로 변환
    }

    private bool IsPointerOverCollider(Vector3 position)
    {
        // 입력 위치가 squareCollider의 경계 내에 있는지 확인
        return squareCollider.OverlapPoint(position);
    }


    private void ToggleScale()
    {
        // 현재 스케일 값을 가져옴
        Vector3 currentScale = transform.localScale;

        if (currentScale.y > 0)
        {
            // y 스케일 값을 음수로 변경
            transform.localScale = new Vector3(currentScale.x, -Mathf.Abs(currentScale.y), currentScale.z);
        }
        else
        {
            // y 스케일 값을 원상 복구
            transform.localScale = new Vector3(currentScale.x, Mathf.Abs(currentScale.y), currentScale.z);
        }
    }
}