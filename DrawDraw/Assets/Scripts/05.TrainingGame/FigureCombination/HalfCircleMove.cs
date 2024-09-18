using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCircleMove : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // 드래그 시 마우스/터치와 도형 간의 위치 차이
    private Rigidbody2D rb2D;
    private Camera mainCamera; // 카메라를 참조할 변수

    // 드래그 범위를 제한할 square 오브젝트
    public CircleCollider2D Collider; // 삼각형 콜라이더
    public GameObject squareObject;        // 동적으로 할당된 사각형 오브젝트
    private BoxCollider2D squareCollider;  // 사각형 오브젝트의 BoxCollider2D

    //// Y 스케일이 양수일 때 사용할 offset과 size 값
    //private Vector2 positiveOffset = new Vector2(0f, (float)-0.3460994);
    //private Vector2 positiveSize = new Vector2(10, (float)9.707803);

    //// Y 스케일이 음수일 때 사용할 offset과 size 값
    //private Vector2 negativeOffset = new Vector2(0f, (float)0.3096588);
    //private Vector2 negativeSize = new Vector2(10, (float)9.862097);

    private float lastTapTime; // 마지막 입력 시간
    private const float doubleTapThreshold = 0.3f; // 더블 클릭/터치 간의 시간 간격 (초)

    public float angle;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // 카메라를 설정

        Collider = GetComponent<CircleCollider2D>();

        // 사각형 오브젝트의 BoxCollider2D 가져오기
        if (squareObject != null)
        {
            squareCollider = squareObject.GetComponent<BoxCollider2D>();

            if (squareCollider == null)
            {
                Debug.LogError("squareObject에 BoxCollider2D가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("squareObject가 할당되지 않았습니다.");
        }
    }

    void Update()
    {
        // 마우스 클릭 또는 터치 입력이 있는지 확인
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // 입력 위치를 월드 좌표로 변환

            Vector3 rayOrigin;
            // 마우스 클릭 위치에서 Raycast를 발사하여 Scene에서 Ray를 볼 수 있게 함
            if (Input.touchCount > 0)
            {
                rayOrigin = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                rayOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            rayOrigin.z = 0f; // 2D에서는 z 값을 0으로 설정 (z 축을 고려하지 않음)

            // "shape" 레이어에 해당하는 레이어 마스크 생성
            int layerMask = 1 << LayerMask.NameToLayer("shape");

            // Raycast를 특정 레이어에만 적용
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //Debug.Log(hit.collider.gameObject);

                // 입력을 확인하여 더블 클릭 또는 더블 터치 처리
                float currentTime = Time.time;
                if (currentTime - lastTapTime < doubleTapThreshold)
                {

                    if (angle < 0)
                    {
                        ToggleScale();
                    }
                    else
                    {
                        RotateDegrees(angle);
                    }

                }
                lastTapTime = currentTime;
                isDragging = true; // 드래그 상태로 전환
                offset = transform.position - mouseOrTouchPosition; // 마우스/터치와 도형 간의 위치 차이 계산
            }
        }

        // 드래그 중일 때 도형 위치 업데이트
        else if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) && isDragging)
        {

            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // 입력 위치를 월드 좌표로 변환
            Vector3 targetPosition = mouseOrTouchPosition + offset; // 목표 위치 계산

            // squareCollider의 경계 내에서만 이동 가능하도록 제한
            Bounds bounds = squareCollider.bounds; // 사각형 콜라이더의 경계 가져오기

            // 삼각형의 꼭짓점 가져오기
            //Vector2[] vertices = Collider.points;

            // 삼각형의 꼭짓점 위치를 고려하여 이동 가능 범위를 설정
            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    // 삼각형 꼭짓점을 월드 좌표로 변환
            //    Vector3 worldVertex = targetPosition + (Vector3)vertices[i];

            //    // X축 이동 제한: 삼각형 꼭짓점이 사각형 경계를 넘지 않도록 제한
            //    if (worldVertex.x < bounds.min.x)
            //    {
            //        targetPosition.x += bounds.min.x - worldVertex.x; // 왼쪽 경계를 넘지 않도록 이동
            //    }
            //    else if (worldVertex.x > bounds.max.x)
            //    {
            //        targetPosition.x -= worldVertex.x - bounds.max.x; // 오른쪽 경계를 넘지 않도록 이동
            //    }

            //    // Y축 이동 제한: 삼각형 꼭짓점이 사각형 경계를 넘지 않도록 제한
            //    if (worldVertex.y < bounds.min.y)
            //    {
            //        targetPosition.y += bounds.min.y - worldVertex.y; // 아래쪽 경계를 넘지 않도록 이동
            //    }
            //    else if (worldVertex.y > bounds.max.y)
            //    {
            //        targetPosition.y -= worldVertex.y - bounds.max.y; // 위쪽 경계를 넘지 않도록 이동
            //    }
            //}

            // Rigidbody2D를 사용하여 도형의 위치를 이동
            rb2D.MovePosition(targetPosition);

        }

        // 드래그 종료
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            // 드래그 상태를 종료하고 선택된 오브젝트를 초기화
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

    private void ToggleScale()
    {
        // 현재 스케일 값을 가져옴
        Vector3 currentScale = transform.localScale;

        // 현재 위치를 저장
        Vector3 originalPosition = transform.position;

        // 스케일 변경
        if (currentScale.x > 0)
        {
            // y 스케일 값을 음수로 변경
            //transform.localScale = new Vector3(currentScale.x, -Mathf.Abs(currentScale.y), currentScale.z);

            // x 스케일 값을 음수로 변경
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);

        }
        else
        {
            // x 스케일 값을 원상 복구
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }

        // 위치 보정: 스케일 변경 전의 위치로 되돌림
        transform.position = originalPosition;
    }

    //void UpdateCollider()
    //{
    //    // 현재 오브젝트의 Y 스케일 값을 확인
    //    if (transform.localScale.y > 0)
    //    {
    //        // Y 스케일이 양수일 때, positiveOffset과 positiveSize를 적용
    //        squareCollider.GetComponent<BoxCollider2D>().offset = positiveOffset;
    //        squareCollider.GetComponent<BoxCollider2D>().size = positiveSize;
    //    }
    //    else
    //    {
    //        // Y 스케일이 음수일 때, negativeOffset과 negativeSize를 적용
    //        squareCollider.GetComponent<BoxCollider2D>().offset = negativeOffset;
    //        squareCollider.GetComponent<BoxCollider2D>().size = negativeSize;
    //    }
    //}

    // Z축으로 45도 반시계 방향 회전하는 함수
    void RotateDegrees(float angle)
    {
        transform.Rotate(0f, 0f, angle);
    }


}