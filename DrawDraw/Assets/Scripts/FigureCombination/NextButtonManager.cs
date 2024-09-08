using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // ���� ��ư �׷�
    public CanvasGroup colorButtonGroup;  // ��ĥ ��ư �׷�

    public GameObject[] prefabsToDisable; // ��Ȱ��ȭ�� �������� �迭

    public GameObject completeButtonObject;  // Inspector���� "�ϼ�" ��ư ������Ʈ�� �Ҵ�

    void Start()
    {
        // ó�� ������ �� ���� ��ư�� ���̰�, ��ĥ ��ư�� ������ �ʵ��� ����
        SetCanvasGroupActive(shapeButtonGroup, true);
        SetCanvasGroupActive(colorButtonGroup, false);
    }

    public void OnNextButtonClick()
    {
        // ó�� ������ �� ���� ��ư�� ���̰�, ��ĥ ��ư�� ������ �ʵ��� ����
        SetCanvasGroupActive(shapeButtonGroup, false);
        SetCanvasGroupActive(colorButtonGroup, true);

        // �� �������� ��� �ν��Ͻ��� �ִ� ��� ��ũ��Ʈ�� ��Ȱ��ȭ�մϴ�.
        foreach (GameObject prefab in prefabsToDisable)
        {
            DisableScriptsOnPrefabInstances(prefab);
        }

        //isButtonClicked = true;  // ��ư�� �������� ǥ��
        //CalculateMatches();
        gameObject.SetActive(false);  // "����" ��ư ������Ʈ ��Ȱ��ȭ
        completeButtonObject.SetActive(true);  // "�ϼ�" ��ư ������Ʈ Ȱ��ȭ
    }

    private void DisableScriptsOnPrefabInstances(GameObject prefab)
    {
        // Ư�� �������� ��� �ν��Ͻ��� ã���ϴ�.
        GameObject[] allInstances = GameObject.FindGameObjectsWithTag(prefab.tag);
        foreach (GameObject instance in allInstances)
        {
            // �ش� �ν��Ͻ��� �پ� �ִ� ��� MonoBehaviour ��ũ��Ʈ�� ��Ȱ��ȭ�մϴ�.
            MonoBehaviour[] scripts = instance.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
                //Debug.Log($"{instance.name}�� {script.GetType().Name} ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ����ϴ�.");
            }
        }
    }

    void SetCanvasGroupActive(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }

}