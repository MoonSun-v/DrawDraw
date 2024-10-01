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
        // 모든 버튼 텍스트의 불투명도 변경
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            if (i == clickedIndex)
            {
                // 클릭된 버튼의 텍스트는 불투명도 100%
                buttonTexts[i].color = new Color(buttonTexts[i].color.r, buttonTexts[i].color.g, buttonTexts[i].color.b, 1f);
            }
            else
            {
                // 나머지 버튼의 텍스트는 불투명도 50%
                buttonTexts[i].color = new Color(buttonTexts[i].color.r, buttonTexts[i].color.g, buttonTexts[i].color.b, 0.5f);
            }
        }
    }

}
