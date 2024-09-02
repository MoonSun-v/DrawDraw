using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // 도형 버튼 그룹
    public CanvasGroup colorButtonGroup;  // 색칠 버튼 그룹

    public GameObject[] prefabsToDisable; // 비활성화할 프리팹의 배열

    private ColorButtonMover[] colorButtonMovers;  // 모든 색칠 버튼을 관리하기 위한 배열
    private Color selectedColor; // 선택된 색상
    private int shapeLayer; // "Shape" 레이어의 인덱스를 저장

    void Start()
    {
        // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
        SetCanvasGroupActive(shapeButtonGroup, true);
        SetCanvasGroupActive(colorButtonGroup, false);

        // 모든 ColorButtonMover를 찾습니다.
        colorButtonMovers = FindObjectsOfType<ColorButtonMover>();

        // "Shape" 레이어의 인덱스를 가져옵니다.
        shapeLayer = LayerMask.NameToLayer("Shape");
    }

    public void OnNextButtonClick()
    {
        // 다음 버튼 클릭 시 도형 버튼을 숨기고 색칠 버튼을 보이도록 설정
        SetCanvasGroupActive(shapeButtonGroup, false);
        SetCanvasGroupActive(colorButtonGroup, true);

        // 각 프리팹의 모든 인스턴스에 있는 모든 스크립트를 비활성화합니다.
        foreach (GameObject prefab in prefabsToDisable)
        {
            DisableScriptsOnPrefabInstances(prefab);
        }
    }

    private void DisableScriptsOnPrefabInstances(GameObject prefab)
    {
        // 특정 프리팹의 모든 인스턴스를 찾습니다.
        GameObject[] allInstances = GameObject.FindGameObjectsWithTag(prefab.tag);

        foreach (GameObject instance in allInstances)
        {
            // 해당 인스턴스에 붙어 있는 모든 MonoBehaviour 스크립트를 비활성화합니다.
            MonoBehaviour[] scripts = instance.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
                Debug.Log($"{instance.name}의 {script.GetType().Name} 스크립트가 비활성화되었습니다.");
            }
        }
    }

    void SetCanvasGroupActive(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit hit;

            // 레이어 마스크를 사용하여 Raycast가 "Shape" 레이어에만 적용되도록 설정
            int layerMask = 1 << shapeLayer;
            // Raycast를 특정 레이어에만 적용
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // 충돌된 오브젝트의 이름을 출력합니다.
                Debug.Log($"선택된 도형의 이름: {hit.transform.name}");

                Renderer renderer = hit.transform.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = selectedColor;
                    Debug.Log($"{hit.transform.name}의 색상이 {selectedColor}로 변경되었습니다.");
                }
            }
        }
    }

}