using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // 도형 버튼 그룹
    public CanvasGroup colorButtonGroup;  // 색칠 버튼 그룹

    private ColorButtonMover[] colorButtonMovers;  // 모든 색칠 버튼을 관리하기 위한 배열

    void Start()
    {
        // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
        SetCanvasGroupActive(shapeButtonGroup, true);
        SetCanvasGroupActive(colorButtonGroup, false);

        // 모든 ColorButtonMover를 찾습니다.
        colorButtonMovers = FindObjectsOfType<ColorButtonMover>();
    }

    public void OnNextButtonClick()
    {
        // 다음 버튼 클릭 시 도형 버튼을 숨기고 색칠 버튼을 보이도록 설정
        SetCanvasGroupActive(shapeButtonGroup, false);
        SetCanvasGroupActive(colorButtonGroup, true);
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
}