using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    public Text[] buttonTexts;

    public ImageLoad imageLoad;

    private void Start()
    {
        OnButtonClick(0);
        imageLoad.ResultIndexClick(1);
    }

    public void OnButtonClick(int clickedIndex)
    {
        // ��� ��ư �ؽ�Ʈ�� ������ ����
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (i == clickedIndex)
            {
                // Ŭ���� ��ư�� �ؽ�Ʈ�� ������ 100%
                buttonTexts[i].color = new Color(buttonTexts[i].color.r, buttonTexts[i].color.g, buttonTexts[i].color.b, 1f);
            }
            else
            {
                // ������ ��ư�� �ؽ�Ʈ�� ������ 50%
                buttonTexts[i].color = new Color(buttonTexts[i].color.r, buttonTexts[i].color.g, buttonTexts[i].color.b, 0.5f);
            }
        }
    }

}
