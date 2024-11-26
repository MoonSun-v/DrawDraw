using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChangeOnTrigger : MonoBehaviour
{
    public GameObject dogPanel; // ������ �г� ������Ʈ
    public GameObject catPanel; // ����� �г� ������Ʈ
    public GameObject triggerObject1; // ù ��° Ʈ���� ������Ʈ
    public GameObject triggerObject2; // �� ��° Ʈ���� ������Ʈ
    private bool userPreference = false; // ����� ������ ������� ���� ("dog" �Ǵ� "cat")
    public string text1 = "Text 1"; // Ʈ���� 1�� Ȱ��ȭ�� �� ǥ���� �ؽ�Ʈ
    public string text2 = "Text 2"; // Ʈ���� 2�� Ȱ��ȭ�� �� ǥ���� �ؽ�Ʈ

    private Text dogText; // ������ �г� ���� �ؽ�Ʈ
    private Text catText; // ����� �г� ���� �ؽ�Ʈ
    private Text targetText; // ���� ����� ��� �г��� �ؽ�Ʈ

    private bool previousTriggerObject1State = false; // Ʈ����1 ���� ����
    private bool previousTriggerObject2State = false; // Ʈ����2 ���� ����

    public int index = 0;

    void Start()
    {
        // dogPanel�� catPanel �ȿ��� Text ������Ʈ�� ã��
        if (dogPanel != null)
        {
            dogText = dogPanel.GetComponentInChildren<Text>();
        }

        if (catPanel != null)
        {
            catText = catPanel.GetComponentInChildren<Text>();
        }

        // userPreference�� ���� Ÿ�� �ؽ�Ʈ ����
        SetTargetTextBasedOnPreference();
    }

    void Update()
    {
        // Ʈ���� ������Ʈ�� Ȱ��ȭ�Ǿ����� Ȯ��
        bool isTrigger1Active = triggerObject1 != null && triggerObject1.activeSelf;
        bool isTrigger2Active = triggerObject2 != null && triggerObject2.activeSelf;

        // Ʈ���� ������Ʈ 1�� ���Ӱ� Ȱ��ȭ�Ǹ� �ؽ�Ʈ ����
        if (isTrigger1Active && !previousTriggerObject1State)
        {
            ChangeText(text1); // Ʈ���� 1 Ȱ��ȭ �� �ؽ�Ʈ�� text1���� ����
            index = 0;
        }

        // Ʈ���� ������Ʈ 2�� ���Ӱ� Ȱ��ȭ�Ǹ� �ؽ�Ʈ ����
        if (isTrigger2Active && !previousTriggerObject2State)
        {
            ChangeText(text2); // Ʈ���� 2 Ȱ��ȭ �� �ؽ�Ʈ�� text2�� ����
            index = 1;
        }

        // ���� Ʈ���� ���� ������Ʈ
        previousTriggerObject1State = isTrigger1Active;
        previousTriggerObject2State = isTrigger2Active;
    }

    // userPreference�� ���� �ؽ�Ʈ Ÿ�� ����
    void SetTargetTextBasedOnPreference()
    {
        if (userPreference == false && dogText != null)
        {
            targetText = dogText; // ������ �г��� Ÿ������ ����
        }
        else if (userPreference == true && catText != null)
        {
            targetText = catText; // ����� �г��� Ÿ������ ����
        }
        else
        {
            Debug.LogWarning("userPreference ���� ��ȿ���� �ʰų� �г� �ؽ�Ʈ�� �����ϴ�.");
        }
    }

    // �ؽ�Ʈ ���� �Լ�
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