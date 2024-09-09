using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButtonManager : MonoBehaviour
{
    private ColorButtonMover[] colorButtonMovers;  // 모든 색칠 버튼을 관리하기 위한 배열
    private Color selectedColor; // 선택된 색상
    private int shapeLayer; // "Shape" 레이어의 인덱스를 저장

    private bool isActive = false; // 활성화 여부

    // 색상을 변경한 도형 개수를 저장할 변수
    private int changedShapeCount = 0;

    // 색상 변경 여부를 추적하는 Dictionary
    private Dictionary<GameObject, bool> colorChangedMap = new Dictionary<GameObject, bool>();

    void Start()
    {
        // 모든 ColorButtonMover를 찾습니다.
        colorButtonMovers = FindObjectsOfType<ColorButtonMover>();

        // "Shape" 레이어의 인덱스를 가져옵니다.
        shapeLayer = LayerMask.NameToLayer("shape");
        isActive = true;  // 버튼이 눌렸음을 표시

        // Dictionary 초기화
        //colorChangedMap = new System.Collections.Generic.Dictionary<GameObject, bool>();

    }

    // Update is called once per frame
    void Update()
    {
        // 버튼이 클릭된 후에만 이 코드가 실행됨
        if (isActive && Input.GetMouseButtonDown(0))
        {
            ExecuteRaycast();
        }
    }

    // Raycast 관련 로직을 이 함수로 분리
    void ExecuteRaycast()
    {
        Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rayOrigin.z = 0f; // 2D에서는 z 값을 0으로 설정 (z 축을 고려하지 않음)

        // "shape" 레이어에 해당하는 레이어 마스크 생성
        int layerMask = 1 << LayerMask.NameToLayer("shape");

        // Raycast를 특정 레이어에만 적용
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            Debug.Log($"선택된 도형의 이름: {hit.transform.name}");

            // 부모 오브젝트 가져오기 (자신이 최상위면 부모가 null일 수 있음)
            Transform parent = hit.transform.parent;

            if (parent != null) // 부모가 있는 경우
            {
                // 부모 아래의 모든 자식 오브젝트 순회
                foreach (Transform sibling in parent)
                {
                    SpriteRenderer siblingRenderer = sibling.GetComponent<SpriteRenderer>();

                    // 자식에 SpriteRenderer가 있으면 색상 변경
                    if (siblingRenderer != null && selectedColor != new Color(0, 0, 0, 0))
                    {
                        // 색상이 한 번이라도 변경된 적이 있는지 확인
                        if (!colorChangedMap.ContainsKey(sibling.gameObject) || !colorChangedMap[sibling.gameObject])
                        {
                            // 색상이 한 번도 변경되지 않았다면 색상을 변경
                            siblingRenderer.color = selectedColor;
                            Debug.Log($"{sibling.name}의 색상이 {selectedColor}로 변경되었습니다.");

                            // 변경한 도형으로 표시하고 카운터 증가
                            colorChangedMap[sibling.gameObject] = true;
                            changedShapeCount++;
                            Debug.Log($"색상이 변경된 도형 개수: {changedShapeCount}");
                        }
                        else
                        {
                            Debug.Log($"{sibling.name}의 색상은 이미 변경된 적이 있습니다.");
                        }
                    }
                }
            }
            else // 부모가 없는 경우 (최상위 오브젝트)
            {
                SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && selectedColor != new Color(0, 0, 0, 0))
                {
                    // 색상이 한 번도 변경되지 않았다면 색상을 변경
                    if (!colorChangedMap.ContainsKey(hit.transform.gameObject) || !colorChangedMap[hit.transform.gameObject])
                    {
                        spriteRenderer.color = selectedColor;
                        Debug.Log($"{hit.transform.name}의 색상이 {selectedColor}로 변경되었습니다.");

                        // 변경한 도형으로 표시하고 카운터 증가
                        colorChangedMap[hit.transform.gameObject] = true;
                        changedShapeCount++;
                        Debug.Log($"색상이 변경된 도형 개수: {changedShapeCount}");
                    }
                    else
                    {
                        Debug.Log($"{hit.transform.name}의 색상은 이미 변경된 적이 있습니다.");
                    }

                }
            }
        }
        else
        {
            //Debug.Log("충돌된 오브젝트가 없습니다.");
        }
    }

    public void OnButtonClicked(ColorButtonMover clickedButton)
    {
        // 클릭된 버튼을 제외한 모든 버튼의 위치를 리셋합니다.
        foreach (ColorButtonMover buttonMover in colorButtonMovers)
        {
            if (buttonMover != clickedButton)
            {
                buttonMover.ResetPosition();
            }
        }
    }

    public void SetSelectedButtonID(int buttonID)
    {
        // 클릭된 버튼의 ID에 따라 색상 코드를 결정합니다.
        string colorCode = "#FFFFFF"; // 기본 색상 (흰색)

        // 클릭된 버튼의 ID에 따라 색상을 결정합니다.
        switch (buttonID)
        {
            case 1:
                colorCode = "#E30204";
                break;
            case 2:
                colorCode = "#F0870C";
                break;
            case 3:
                colorCode = "#F9DC00";
                break;
            case 4:
                colorCode = "#3B9C00";
                break;
            case 5:
                colorCode = "#0085FE";
                break;
            case 6:
                colorCode = "#2E33D7";
                break;
            case 7:
                colorCode = "#DB7093";
                break;
            // 추가 버튼에 대한 색상을 설정할 수 있습니다.
            default:

                break;
        }
        // HEX 색상 코드를 Color로 변환합니다.
        if (ColorUtility.TryParseHtmlString(colorCode, out selectedColor))
        {
            Debug.Log($"색상이 {colorCode}로 설정되었습니다.");
        }
        else
        {
            Debug.LogError("색상 코드 변환에 실패했습니다.");
        }
    }

    // Getter 메서드: changedShapeCount 값을 다른 스크립트에서 가져올 수 있음
    public int GetChangedShapeCount()
    {
        return changedShapeCount;
    }
}
