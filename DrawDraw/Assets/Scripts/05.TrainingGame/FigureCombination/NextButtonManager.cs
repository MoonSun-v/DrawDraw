using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // 도형 버튼 그룹
    public CanvasGroup colorButtonGroup;  // 색칠 버튼 그룹

    public GameObject[] prefabsToDisable; // 비활성화할 프리팹의 배열

    public GameObject completeButtonObject;  // Inspector에서 "완성" 버튼 오브젝트를 할당

    public GameObject check_popup; // PopupManager 스크립트를 참조할 변수


    void Start()
    {
        // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
        SetCanvasGroupActive(shapeButtonGroup, true);
        SetCanvasGroupActive(colorButtonGroup, false);
    }

    public void onCheckPop() // 확인창 띄우기
    {
        check_popup.SetActive(true); // 확인 팝업 창을 화면에 표시
    }

    public void OnNextButtonClick()
    {
        // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
        SetCanvasGroupActive(shapeButtonGroup, false);
        SetCanvasGroupActive(colorButtonGroup, true);

        // 각 프리팹의 모든 인스턴스에 있는 모든 스크립트를 비활성화합니다.
        foreach (GameObject prefab in prefabsToDisable)
        {
            DisableScriptsOnPrefabInstances(prefab);
        }

        //isButtonClicked = true;  // 버튼이 눌렸음을 표시
        //CalculateMatches();
        gameObject.SetActive(false);  // "다음" 버튼 오브젝트 비활성화
        completeButtonObject.SetActive(true);  // "완성" 버튼 오브젝트 활성화

        check_popup.SetActive(false);
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
                //Debug.Log($"{instance.name}의 {script.GetType().Name} 스크립트가 비활성화되었습니다.");
            }
        }
    }

    void SetCanvasGroupActive(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }

}