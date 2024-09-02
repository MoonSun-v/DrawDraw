using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // ���� ��ư �׷�
    public CanvasGroup colorButtonGroup;  // ��ĥ ��ư �׷�

    public GameObject[] prefabsToDisable; // ��Ȱ��ȭ�� �������� �迭

    private ColorButtonMover[] colorButtonMovers;  // ��� ��ĥ ��ư�� �����ϱ� ���� �迭
    private Color selectedColor; // ���õ� ����
    private int shapeLayer; // "Shape" ���̾��� �ε����� ����

    void Start()
    {
        // ó�� ������ �� ���� ��ư�� ���̰�, ��ĥ ��ư�� ������ �ʵ��� ����
        SetCanvasGroupActive(shapeButtonGroup, true);
        SetCanvasGroupActive(colorButtonGroup, false);

        // ��� ColorButtonMover�� ã���ϴ�.
        colorButtonMovers = FindObjectsOfType<ColorButtonMover>();

        // "Shape" ���̾��� �ε����� �����ɴϴ�.
        shapeLayer = LayerMask.NameToLayer("Shape");
    }

    public void OnNextButtonClick()
    {
        // ���� ��ư Ŭ�� �� ���� ��ư�� ����� ��ĥ ��ư�� ���̵��� ����
        SetCanvasGroupActive(shapeButtonGroup, false);
        SetCanvasGroupActive(colorButtonGroup, true);

        // �� �������� ��� �ν��Ͻ��� �ִ� ��� ��ũ��Ʈ�� ��Ȱ��ȭ�մϴ�.
        foreach (GameObject prefab in prefabsToDisable)
        {
            DisableScriptsOnPrefabInstances(prefab);
        }
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
                Debug.Log($"{instance.name}�� {script.GetType().Name} ��ũ��Ʈ�� ��Ȱ��ȭ�Ǿ����ϴ�.");
            }
        }
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
    public void SetSelectedButtonID(int buttonID)
    {
        // Ŭ���� ��ư�� ID�� ���� ���� �ڵ带 �����մϴ�.
        string colorCode = "#FFFFFF"; // �⺻ ���� (���)

        // Ŭ���� ��ư�� ID�� ���� ������ �����մϴ�.
        switch (buttonID)
        {
            case 1:
                colorCode = "#E30204"; 
                break;
            case 2:
                colorCode = "#F0870C"; 
                break;
            case 3:
                colorCode = "#F9DC00";
                break;
            case 4:
                colorCode = "#3B9C00";
                break;
            case 5:
                colorCode = "#0085FE";
                break;
            case 6:
                colorCode = "#2E33D7";
                break;
            case 7:
                colorCode = "#DB7093";
                break;
            // �߰� ��ư�� ���� ������ ������ �� �ֽ��ϴ�.
            default:
                
                break;
        }
        // HEX ���� �ڵ带 Color�� ��ȯ�մϴ�.
        if (ColorUtility.TryParseHtmlString(colorCode, out selectedColor))
        {
            Debug.Log($"������ {colorCode}�� �����Ǿ����ϴ�.");
        }
        else
        {
            Debug.LogError("���� �ڵ� ��ȯ�� �����߽��ϴ�.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit hit;

            // ���̾� ����ũ�� ����Ͽ� Raycast�� "Shape" ���̾�� ����ǵ��� ����
            int layerMask = 1 << shapeLayer;
            // Raycast�� Ư�� ���̾�� ����
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // �浹�� ������Ʈ�� �̸��� ����մϴ�.
                Debug.Log($"���õ� ������ �̸�: {hit.transform.name}");

                Renderer renderer = hit.transform.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = selectedColor;
                    Debug.Log($"{hit.transform.name}�� ������ {selectedColor}�� ����Ǿ����ϴ�.");
                }
            }
        }
    }

}