using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPop : MonoBehaviour
{
    public GameObject check_popup;
    public GameObject check_popup1;

    public CanvasGroup colorButtonGroup;  // 색칠 버튼 그룹
    private void Start()
    {
            // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
            //SetCanvasGroupActive(shapeButtonGroup, true);
            SetCanvasGroupActive(colorButtonGroup, false);
            Debug.Log("페인트 없애기");
    }

    void SetCanvasGroupActive(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }


    public void onCheckPop() // 확인창 띄우기
    {
        check_popup.SetActive(true); // 확인 팝업 창을 화면에 표시
    }
    public void CloseCheckPop()
    {
        check_popup.SetActive(false);
    }
    public void onCheckPop1() // 확인창 띄우기
    {
        check_popup1.SetActive(true); // 확인 팝업 창을 화면에 표시
    }
    public void CloseCheckPop1()
    {
        check_popup1.SetActive(false);
    }
}
