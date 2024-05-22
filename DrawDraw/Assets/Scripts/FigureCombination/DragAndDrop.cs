using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop: MonoBehaviour
{
    private bool isDragging = false; // 현재 드래그 중인지 여부를 저장하는 변수
    private GameObject currentObject; // 현재 드래그 중인 오브젝트를 저장하는 변수
    public GameObject[] objectPrefabs; // 복사할 오브젝트의 프리팹 배열
    private GameObject selectedPrefab; // 선택된 프리팹

    void Start()
    {
        if (objectPrefabs.Length > 0)
        {
            selectedPrefab = objectPrefabs[0]; // 기본으로 첫 번째 프리팹을 선택
        }
    }

    void Update()
    {
        // 마우스 입력 처리
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) // 마우스 왼쪽 버튼을 눌렀고 UI 오브젝트 위가 아닐 때
        {
            HandleInput(Input.mousePosition); // 입력 처리 메서드 호출
        }

        if (Input.GetMouseButton(0) && isDragging) // 마우스 왼쪽 버튼을 누르고 드래그 중일 때
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
            mousePosition.z = 0; // z축 값을 0으로 설정
            currentObject.transform.position = mousePosition; // 현재 드래그 중인 오브젝트 위치를 마우스 위치로 설정
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // 마우스 왼쪽 버튼을 놓았을 때
        {
            isDragging = false; // 드래그 상태 해제
            currentObject = null; // 현재 드래그 중인 오브젝트 해제
        }

        // 터치 입력 처리
        if (Input.touchCount > 0 && IsPointerOverUIObject()) // 하나 이상의 터치가 발생했고 UI 오브젝트 위가 아닐 때
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치 정보를 가져옴
            if (touch.phase == TouchPhase.Began) // 터치가 시작되었을 때
            {
                HandleInput(touch.position); // 입력 처리 메서드 호출
            }

            if (touch.phase == TouchPhase.Moved && isDragging) // 터치 이동 중이고 드래그 중일 때
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // 터치 위치를 월드 좌표로 변환
                touchPosition.z = 0; // z축 값을 0으로 설정
                currentObject.transform.position = touchPosition; // 현재 드래그 중인 오브젝트 위치를 터치 위치로 설정
            }

            if (touch.phase == TouchPhase.Ended && isDragging) // 터치가 끝났을 때
            {
                isDragging = false; // 드래그 상태 해제
                currentObject = null; // 현재 드래그 중인 오브젝트 해제
            }
        }
    }

    // 입력 처리를 담당하는 메서드
    void HandleInput(Vector3 inputPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition); // 입력 위치를 월드 좌표로 변환
        worldPosition.z = 0; // z축 값을 0으로 설정

        if (currentObject == null) // 현재 드래그 중인 오브젝트가 없을 때
        {
            currentObject = Instantiate(selectedPrefab, worldPosition, Quaternion.identity); // 새로운 오브젝트 생성
            // 오브젝트의 SpriteRenderer 가져오기
            SpriteRenderer spriteRenderer = currentObject.GetComponent<SpriteRenderer>();

            // Sorting Layer와 Order in Layer 설정
            if (spriteRenderer != null)
            {
                //spriteRenderer.sortingLayerName = "Foreground";  // 원하는 Sorting Layer 이름
                spriteRenderer.sortingOrder = 10;  // 원하는 Order in Layer 값 (높을수록 앞에 렌더링됨)
            }

        }

        Collider2D collider = Physics2D.OverlapPoint(worldPosition); // 입력 위치에 있는 Collider2D를 탐지
        if (collider != null && collider.gameObject == currentObject) // 탐지된 Collider2D가 현재 드래그 중인 오브젝트일 때
        {
            isDragging = true; // 드래그 상태로 설정
        }
    }

    // 선택된 프리팹을 설정하는 메서드
    public void SetSelectedPrefab(int index)
    {
        if (index >= 0 && index < objectPrefabs.Length)
        {
            selectedPrefab = objectPrefabs[index]; // 선택된 프리팹으로 설정
        }
        else
        {
            Debug.LogError("잘못된 프리팹 인덱스입니다. 올바른 인덱스를 입력해 주세요.");
        }
    }

    // UI 오브젝트 위에 있는지 확인하는 메서드
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // 버튼 클릭 시 선택된 프리팹을 마우스 위치에 생성하는 메서드
    public void CreateObjectAtMousePosition()
    {
        if (selectedPrefab != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 위치를 월드 좌표로 변환
            mousePosition.z = 0; // z축 값을 0으로 설정
            currentObject = Instantiate(selectedPrefab, mousePosition, Quaternion.identity); // 선택된 프리팹을 마우스 위치에 생성
            
            // 오브젝트의 SpriteRenderer 가져오기
            SpriteRenderer spriteRenderer = currentObject.GetComponent<SpriteRenderer>();

            // Sorting Layer와 Order in Layer 설정
            if (spriteRenderer != null)
            {
                //spriteRenderer.sortingLayerName = "Foreground";  // 원하는 Sorting Layer 이름
                spriteRenderer.sortingOrder = 10;  // 원하는 Order in Layer 값 (높을수록 앞에 렌더링됨)
            }

        }
        else
        {
            Debug.LogError("선택된 프리팹이 null입니다. 프리팹을 선택해 주세요.");
        }
    }
}