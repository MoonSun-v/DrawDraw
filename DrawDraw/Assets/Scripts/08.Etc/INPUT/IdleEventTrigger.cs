using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleEventTrigger : MonoBehaviour
{
    public GameObject dogObject; // �������� �� Ȱ��ȭ�� ���� ������Ʈ
    public GameObject catObject; // ������� �� Ȱ��ȭ�� ���� ������Ʈ

    public GameObject triggerObject1; // ù ��° Ʈ���� ������Ʈ
    public GameObject triggerObject2; // �� ��° Ʈ���� ������Ʈ

    private float idleTimeThreshold = 10f; // �Է��� ���� �� ������Ʈ�� Ȱ��ȭ�Ǵ� �ð� (��)
    private float activeDuration = 5f; // ������Ʈ�� Ȱ��ȭ�� �� �ڵ����� ��Ȱ��ȭ�Ǵ� �ð� (��)
    private float idleTimer = 0f;
    private bool isObjectActive = false;
    private int activationCount = 0; // ������Ʈ�� Ȱ��ȭ�� Ƚ��
    private int maxActivations = 3; // �ִ� Ȱ��ȭ Ƚ��

    public string userPreference = "dog"; // ����� ������ ������� ���� ("dog" �Ǵ� "cat")

    void Update()
    {
        // triggerObject�� �� �� �ϳ��� null�� �ƴϰ� Ȱ��ȭ�Ǿ� ������ ����
        if ((triggerObject1 == null || triggerObject1.activeSelf) || (triggerObject2 == null || triggerObject2.activeSelf))
        {
            // ��ġ �Է� ���� (�����)
            bool touchInputDetected = Input.touchCount > 0;

            // ���콺 �Է� ���� (PC)
            bool mouseInputDetected = Input.GetMouseButton(0); // 0�� ���� Ŭ���� �ǹ�

            // �Է��� ���� ��� Ÿ�̸� �ʱ�ȭ
            if (touchInputDetected || mouseInputDetected)
            {
                idleTimer = 0f;
                if (isObjectActive)
                {
                    DisableObject(); // �Է��� ������ ������Ʈ ��Ȱ��ȭ
                }
            }
            else
            {
                // �Է��� ���� ��� Ÿ�̸� ����
                idleTimer += Time.deltaTime;

                // �Է��� ����, ���� �ð��� ������ ������Ʈ Ȱ��ȭ
                if (idleTimer >= idleTimeThreshold && !isObjectActive && activationCount < maxActivations)
                {
                    ActivateObjectBasedOnUserPreference();
                }
            }

            // ������Ʈ�� Ȱ��ȭ�� ���¶�� Ȱ��ȭ�� �ð��� üũ
            if (isObjectActive && idleTimer >= idleTimeThreshold + activeDuration)
            {
                DisableObject(); // Ȱ��ȭ�� �ð��� ������ ��Ȱ��ȭ
            }
        }
    }

    // ����� ������ ���� �ٸ� ������Ʈ Ȱ��ȭ
    void ActivateObjectBasedOnUserPreference()
    {
        if (userPreference == "dog" && dogObject != null)
        {
            dogObject.SetActive(true);
            Debug.Log("������ ������Ʈ�� Ȱ��ȭ�Ǿ����ϴ�.");
        }
        else if (userPreference == "cat" && catObject != null)
        {
            catObject.SetActive(true);
            Debug.Log("����� ������Ʈ�� Ȱ��ȭ�Ǿ����ϴ�.");
        }
        isObjectActive = true;
        activationCount++; // Ȱ��ȭ Ƚ�� ����
    }

    // ���� ������Ʈ ��Ȱ��ȭ�ϴ� �Լ�
    void DisableObject()
    {
        if (dogObject != null && dogObject.activeSelf)
        {
            dogObject.SetActive(false);
            Debug.Log("������ ������Ʈ�� ��Ȱ��ȭ�Ǿ����ϴ�.");
        }

        if (catObject != null && catObject.activeSelf)
        {
            catObject.SetActive(false);
            Debug.Log("����� ������Ʈ�� ��Ȱ��ȭ�Ǿ����ϴ�.");
        }

        isObjectActive = false; // ������Ʈ�� ��Ȱ��ȭ�� ���¸� ���
        idleTimer = 0f; // Ÿ�̸� �ʱ�ȭ
    }
}