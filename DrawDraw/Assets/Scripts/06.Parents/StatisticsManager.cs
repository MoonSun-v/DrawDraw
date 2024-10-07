using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    public Text[] buttonTexts;

    public ImageLoad imageLoad;

    // 각 키에 해당하는 슬라이더 그룹 (4개의 슬라이더가 각각 1개의 키에 대응)
    public Slider[] slidersKey0; // 키 0에 대응하는 4개의 슬라이더
    public Slider[] slidersKey1; // 키 1에 대응하는 4개의 슬라이더
    public Slider[] slidersKey2; // 키 2에 대응하는 4개의 슬라이더
    public Slider[] slidersKey3; // 키 3에 대응하는 4개의 슬라이더
    public Slider[] slidersKey4; // 키 4에 대응하는 4개의 슬라이더



    private void Start()
    {
        OnButtonClick(0);
        imageLoad.ResultIndexClick(1);


        // 막대 그래프(슬라이더) 값 적용 
        ApplyScoresToSliders();
    }


    // [ 항목별 통계의 버튼을 선택 ]
    // 
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


    // [ 슬라이더에 점수 적용 ]
    // 
    void ApplyScoresToSliders()
    {
        // 각 키의 데이터를 대응하는 슬라이더 그룹에 할당
        ApplyScoresToGroup(0, slidersKey0); // 키 0에 대한 점수 슬라이더 적용
        ApplyScoresToGroup(1, slidersKey1); // 키 1에 대한 점수 슬라이더 적용
        ApplyScoresToGroup(2, slidersKey2); // 키 2에 대한 점수 슬라이더 적용
        ApplyScoresToGroup(3, slidersKey3); // 키 3에 대한 점수 슬라이더 적용
        ApplyScoresToGroup(4, slidersKey4); // 키 4에 대한 점수 슬라이더 적용
    }

    // [ 그룹 별로 매핑 ]
    // 
    void ApplyScoresToGroup(int key, Slider[] sliders)
    {
        // 해당 키의 TestResultData를 가져옴
        if (GameData.instance.testdata.TestResults.TryGetValue(key, out TestResultData data))
        {
            // 각 슬라이더에 값을 적용
            sliders[0].value = data.Game7Score ;
            sliders[1].value = data.Game8Score ;
            sliders[2].value = data.Game9Score ;
            sliders[3].value = data.Game10Score ;
        }
        else
        {
            // Debug.LogWarning($"키 {key}에 대한 데이터가 존재하지 않습니다.");

            // 데이터가 없을 때 슬라이더 값을 0으로 설정
            sliders[0].value = 0f;
            sliders[1].value = 0f;
            sliders[2].value = 0f;
            sliders[3].value = 0f;
        }
    }
}
