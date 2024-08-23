using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelogramMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // 드래그 시 마우스/터치와 도형 간의 위치 차이
    private Rigidbody2D rb2D;
    private Camera mainCamera; // 카메라를 참조할 변수

    // 드래그 범위를 제한할 square 오브젝트의 Collider2D
    public GameObject squareObject;
    private Collider2D squareCollider;

    // Y 스케일이 양수일 때 사용할 offset과 size 값
    private Vector2 positiveOffset = new Vector2((float)0.5593252, (float)-0.3471658);
    private Vector2 positiveSize = new Vector2((float)8.91976, (float)9.83415);

    // Y 스케일이 음수일 때 사용할 offset과 size 값
    private Vector2 negativeOffset = new Vector2((float)0.5593252, (float)0.3278804);
    private Vector2 negativeSize = new Vector2((float)8.91976, (float)9.872724);

    private float lastTapTime; // 마지막 입력 시간
    private const float doubleTapThreshold = 0.3f; // 더블 클릭/터치 간의 시간 간격 (초)

    private Collider2D col2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // 카메라를 설정

        col2D = GetComponent<Collider2D>();

        if (col2D == null)
        {
            Debug.LogError("Collider2D is not attached to the game object.");
        }

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

        // 마우스 클릭 또는 터치 입력이 있는지 확인
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            //Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // 입력 위치를 월드 좌표로 변환
            // 마우스 클릭 위치에서 Raycast를 발사하여 Scene에서 Ray를 볼 수 있게 함
            Vector3 rayOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            rayOrigin.z = 0f; // 2D에서는 z 값을 0으로 설정 (z 축을 고려하지 않음)

            // "shape" 레이어에 해당하는 레이어 마스크 생성
            int layerMask = 1 << LayerMask.NameToLayer("shape");

            // Raycast를 특정 레이어에만 적용
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

            Debug.Log(hit.collider);

            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                // hitObject가 현재 스크립트가 적용된 gameObject와 같은지 확인
                if (hitObject == gameObject)
                {
                    float currentTime = Time.time;
                    if (currentTime - lastTapTime < doubleTapThreshold)
                    {
                        ToggleScale();
                    }
                    lastTapTime = currentTime;
                    isDragging = true;
                    offset = transform.position - GetInputWorldPosition();
                }
                else
                {
                    //부모 오브젝트를 가져옴
                    GameObject parentObject = hitObject.transform.parent?.gameObject;

                    if (parentObject == gameObject)
                    {
                        float currentTime = Time.time;
                        if (currentTime - lastTapTime < doubleTapThreshold)
                        {
                            ToggleScale();
                        }
                        lastTapTime = currentTime;
                        isDragging = true;
                        offset = transform.position - GetInputWorldPosition();
                    }
                }
            }
        }

        // 드래그 중일 때 도형 위치 업데이트
        else if ((Input.GetMouseButton(0) || Input.touchCount > 0) && isDragging)
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // 입력 위치를 월드 좌표로 변환
            Vector3 targetPosition = mouseOrTouchPosition + offset; // 목표 위치 계산
            
            // 현재 오브젝트의 Y 스케일 값을 확인하고 Collider2D 설정을 업데이트
            UpdateCollider();

            //squareCollider의 경계 내에서만 이동 가능하도록 제한
            Bounds bounds = squareCollider.bounds;
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x); // x 좌표 제한
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y); // y 좌표 제한

            rb2D.MovePosition(targetPosition); // Rigidbody2D를 사용하여 도형의 위치를 이동

        }

        // 드래그 종료
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount == 0))
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

        // 현재 Collider2D의 bounds 중심 계산
        Vector3 originalCenter = col2D.bounds.center;

        // 스케일 변경
        if (currentScale.x > 0)
        {
            // x 스케일 값을 음수로 변경
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else
        {
            // x 스케일 값을 원상 복구
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }

        // 스케일 변경 후 Collider2D의 새로운 bounds 중심 계산
        Vector3 newCenter = col2D.bounds.center;

        // 중심이 이동한 만큼 위치를 보정하여 오브젝트가 화면상에서 이동하지 않도록 함
        Vector3 centerOffset = newCenter - originalCenter;
        transform.position -= centerOffset;
    }

    void UpdateCollider()
    {
        // 현재 오브젝트의 Y 스케일 값을 확인
        if (transform.localScale.x > 0)
        {
            // Y 스케일이 양수일 때, positiveOffset과 positiveSize를 적용
            squareCollider.GetComponent<BoxCollider2D>().offset = positiveOffset;
            squareCollider.GetComponent<BoxCollider2D>().size = positiveSize;
        }
        else
        {
            // Y 스케일이 음수일 때, negativeOffset과 negativeSize를 적용
            squareCollider.GetComponent<BoxCollider2D>().offset = negativeOffset;
            squareCollider.GetComponent<BoxCollider2D>().size = negativeSize;
        }
    }
}