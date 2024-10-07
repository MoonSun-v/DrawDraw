using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    public Text[] buttonTexts;

    public ImageLoad imageLoad;

    // �� Ű�� �ش��ϴ� �����̴� �׷� (4���� �����̴��� ���� 1���� Ű�� ����)
    public Slider[] slidersKey0; // Ű 0�� �����ϴ� 4���� �����̴�
    public Slider[] slidersKey1; // Ű 1�� �����ϴ� 4���� �����̴�
    public Slider[] slidersKey2; // Ű 2�� �����ϴ� 4���� �����̴�
    public Slider[] slidersKey3; // Ű 3�� �����ϴ� 4���� �����̴�
    public Slider[] slidersKey4; // Ű 4�� �����ϴ� 4���� �����̴�



    private void Start()
    {
        OnButtonClick(0);
        imageLoad.ResultIndexClick(1);


        // ���� �׷���(�����̴�) �� ���� 
        ApplyScoresToSliders();
    }


    // [ �׸� ����� ��ư�� ���� ]
    // 
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


    // [ �����̴��� ���� ���� ]
    // 
    void ApplyScoresToSliders()
    {
        // �� Ű�� �����͸� �����ϴ� �����̴� �׷쿡 �Ҵ�
        ApplyScoresToGroup(0, slidersKey0); // Ű 0�� ���� ���� �����̴� ����
        ApplyScoresToGroup(1, slidersKey1); // Ű 1�� ���� ���� �����̴� ����
        ApplyScoresToGroup(2, slidersKey2); // Ű 2�� ���� ���� �����̴� ����
        ApplyScoresToGroup(3, slidersKey3); // Ű 3�� ���� ���� �����̴� ����
        ApplyScoresToGroup(4, slidersKey4); // Ű 4�� ���� ���� �����̴� ����
    }

    // [ �׷� ���� ���� ]
    // 
    void ApplyScoresToGroup(int key, Slider[] sliders)
    {
        // �ش� Ű�� TestResultData�� ������
        if (GameData.instance.testdata.TestResults.TryGetValue(key, out TestResultData data))
        {
            // �� �����̴��� ���� ����
            sliders[0].value = data.Game7Score ;
            sliders[1].value = data.Game8Score ;
            sliders[2].value = data.Game9Score ;
            sliders[3].value = data.Game10Score ;
        }
        else
        {
            // Debug.LogWarning($"Ű {key}�� ���� �����Ͱ� �������� �ʽ��ϴ�.");

            // �����Ͱ� ���� �� �����̴� ���� 0���� ����
            sliders[0].value = 0f;
            sliders[1].value = 0f;
            sliders[2].value = 0f;
            sliders[3].value = 0f;
        }
    }
}
