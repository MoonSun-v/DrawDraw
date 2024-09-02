using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // ���� ��ư �׷�
    public CanvasGroup colorButtonGroup;  // ��ĥ ��ư �׷�

    private ColorButtonMover[] colorButtonMovers;  // ��� ��ĥ ��ư�� �����ϱ� ���� �迭

    void Start()
    {
        // ó�� ������ �� ���� ��ư�� ���̰�, ��ĥ ��ư�� ������ �ʵ��� ����
        SetCanvasGroupActive(shapeButtonGroup, true);
        SetCanvasGroupActive(colorButtonGroup, false);

        // ��� ColorButtonMover�� ã���ϴ�.
        colorButtonMovers = FindObjectsOfType<ColorButtonMover>();
    }

    public void OnNextButtonClick()
    {
        // ���� ��ư Ŭ�� �� ���� ��ư�� ����� ��ĥ ��ư�� ���̵��� ����
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
        // Ŭ���� ��ư�� ������ ��� ��ư�� ��ġ�� �����մϴ�.
        foreach (ColorButtonMover buttonMover in colorButtonMovers)
        {
            if (buttonMover != clickedButton)
            {
                buttonMover.ResetPosition();
            }
        }
    }
}