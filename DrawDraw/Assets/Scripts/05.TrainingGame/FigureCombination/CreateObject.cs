using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject prefab; // 생성할 프리팹
    private GameObject spawnedObject; // 생성된 프리팹

    private bool isDragging = false; // 드래그 상태를 나타냄
    private float startPosX; // 마우스와 프리팹 사이의 X 거리
    private float startPosY; // 마우스와 프리팹 사이의 Y 거리
    public BoxCollider2D targetCollider; // 프리팹이 들어가야 하는 BoxCollider
    public BoxCollider2D DragCollider; // 프리팹이 드래그할 수 있는 제한 영역의 BoxCollider

    private Collider2D prefabCollider; // 생성된 프리팹의 Collider

    public int angle = 0;
    void Update()
    {
        // 마우스 버튼을 누르고 있는 동안 프리팹을 드래그
        if (isDragging && spawnedObject != null)
        {
            DragPrefab();
        }
    }

    // 버튼을 눌렀을 때 프리팹을 생성 (IPointerDownHandler 인터페이스 사용)
    public void OnPointerDown(PointerEventData eventData)
    {
        // 마우스 클릭 위치에서 프리팹 생성
        Vector3 spawnPosition = Input.mousePosition;
        spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, 10f));

        // 프리팹을 Canvas 하위에 생성 (필요 시)
        spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // 생성된 프리팹의 Collider를 참조
        prefabCollider = spawnedObject.GetComponent<Collider2D>();

        // 생성된 오브젝트의 Rigidbody2D 컴포넌트를 가져와서 비활성화
        Rigidbody2D rb2D = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;  // 물리가 적용되지 않도록 설정                                        
        }

        // 마우스와 프리팹 사이의 초기 위치 차이를 저장
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosX = mousePos.x - spawnedObject.transform.position.x;
        startPosY = mousePos.y - spawnedObject.transform.position.y;

        // 드래그 시작
        isDragging = true;
    }

    // 마우스를 떼면 드래그를 멈추고, 프리팹이 BoxCollider 안에 없으면 삭제 (IPointerUpHandler 인터페이스 사용)
    public void OnPointerUp(PointerEventData eventData)
    {
        // 드래그 상태 해제
        isDragging = false;

        if (spawnedObject != null)
        {
            // 프리팹의 위치가 BoxCollider 안에 있는지 확인
            if (!IsPrefabInsideTarget())
            {
                // BoxCollider 안에 있지 않으면 오브젝트 삭제
                Destroy(spawnedObject);
            }

            else
            {
                Draggable draggable = spawnedObject.AddComponent<Draggable>();
                spawnedObject.AddComponent<ObjectOnCollision>();

                if (draggable != null)
                {
                    draggable.DragCollider = targetCollider; // squareObject를 설정
                    draggable.angle = angle;
                }
                else
                {
                    Debug.LogWarning("Draggable component not found on the instantiated object.");
                }
            }
        }
    }

    // 프리팹을 마우스 위치로 드래그
    private void DragPrefab()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        // 프리팹 위치를 마우스에 따라 변경하기 전에 제한된 영역 내에서만 드래그되도록 처리
        Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, spawnedObject.transform.position.z);

        // 제한된 영역 내에서만 드래그되도록 위치를 Clamp
        newPosition = ClampPositionToTarget(newPosition);

        // 프리팹의 위치를 제한된 영역 내에서만 업데이트
        spawnedObject.transform.position = newPosition;
    }

    // 프리팹이 targetCollider 안에 있는지 확인하는 함수
    private bool IsPrefabInsideTarget()
    {
        // 프리팹의 현재 위치
        Vector3 prefabPosition = spawnedObject.transform.position;

        // targetCollider의 경계를 기준으로 좌표를 확인
        if (targetCollider != null)
        {
            Bounds bounds = targetCollider.bounds;

            // 프리팹의 위치가 BoxCollider의 경계 안에 있는지 확인
            if (prefabPosition.x > bounds.min.x && prefabPosition.x < bounds.max.x &&
                prefabPosition.y > bounds.min.y && prefabPosition.y < bounds.max.y)
            {
                return true; // 프리팹이 Collider 안에 있음
            }
        }

        return false; // 프리팹이 Collider 안에 없음
    }

    // 프리팹의 위치를 DragCollider 영역 내로 제한하는 함수
    private Vector3 ClampPositionToTarget(Vector3 position)
    {
        if (DragCollider != null && prefabCollider != null)
        {
            Bounds targetBounds = DragCollider.bounds;
            Bounds prefabBounds = prefabCollider.bounds;

            // X, Y 좌표를 DragCollider 경계 내로 제한
            // 프리팹의 콜라이더 크기를 고려하여 벗어나지 않도록 제한
            float clampedX = Mathf.Clamp(position.x, targetBounds.min.x + prefabBounds.extents.x, targetBounds.max.x - prefabBounds.extents.x);
            float clampedY = Mathf.Clamp(position.y, targetBounds.min.y + prefabBounds.extents.y, targetBounds.max.y - prefabBounds.extents.y);

            return new Vector3(clampedX, clampedY, position.z);
        }

        return position; // DragCollider 없으면 제한하지 않음
    }
}
