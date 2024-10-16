using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangeOnTrigger : MonoBehaviour
{
    public GameObject dogPanel; // 강아지 패널 오브젝트
    public GameObject catPanel; // 고양이 패널 오브젝트
    public GameObject triggerObject1; // 첫 번째 트리거 오브젝트
    public GameObject triggerObject2; // 두 번째 트리거 오브젝트
    private bool userPreference = false; // 사용자 정보를 기반으로 결정 ("dog" 또는 "cat")
    public string text1 = "Text 1"; // 트리거 1이 활성화될 때 표시할 텍스트
    public string text2 = "Text 2"; // 트리거 2가 활성화될 때 표시할 텍스트

    private Text dogText; // 강아지 패널 안의 텍스트
    private Text catText; // 고양이 패널 안의 텍스트
    private Text targetText; // 현재 변경될 대상 패널의 텍스트

    private bool previousTriggerObject1State = false; // 트리거1 이전 상태
    private bool previousTriggerObject2State = false; // 트리거2 이전 상태

    public int index = 0;

    void Start()
    {
        // dogPanel과 catPanel 안에서 Text 컴포넌트를 찾음
        if (dogPanel != null)
        {
            dogText = dogPanel.GetComponentInChildren<Text>();
        }

        if (catPanel != null)
        {
            catText = catPanel.GetComponentInChildren<Text>();
        }

        // userPreference에 따라 타겟 텍스트 설정
        SetTargetTextBasedOnPreference();
    }

    void Update()
    {
        // 트리거 오브젝트가 활성화되었는지 확인
        bool isTrigger1Active = triggerObject1 != null && triggerObject1.activeSelf;
        bool isTrigger2Active = triggerObject2 != null && triggerObject2.activeSelf;

        // 트리거 오브젝트 1이 새롭게 활성화되면 텍스트 변경
        if (isTrigger1Active && !previousTriggerObject1State)
        {
            ChangeText(text1); // 트리거 1 활성화 시 텍스트를 text1으로 변경
            index = 0;
        }

        // 트리거 오브젝트 2가 새롭게 활성화되면 텍스트 변경
        if (isTrigger2Active && !previousTriggerObject2State)
        {
            ChangeText(text2); // 트리거 2 활성화 시 텍스트를 text2로 변경
            index = 1;
        }

        // 이전 트리거 상태 업데이트
        previousTriggerObject1State = isTrigger1Active;
        previousTriggerObject2State = isTrigger2Active;
    }

    // userPreference에 따라 텍스트 타겟 설정
    void SetTargetTextBasedOnPreference()
    {
        if (userPreference == false && dogText != null)
        {
            targetText = dogText; // 강아지 패널을 타겟으로 설정
        }
        else if (userPreference == true && catText != null)
        {
            targetText = catText; // 고양이 패널을 타겟으로 설정
        }
        else
        {
            Debug.LogWarning("userPreference 값이 유효하지 않거나 패널 텍스트가 없습니다.");
        }
    }

    // 텍스트 변경 함수
    void ChangeText(string newText)
    {
        if (targetText != null)
        {
            targetText.text = newText;
        }
        else
        {
            Debug.LogWarning("Target Text component is null.");
        }
    }
}