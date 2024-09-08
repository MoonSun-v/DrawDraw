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

    private bool isButtonClicked = false; // ��ư�� ���ȴ��� ����

    // ���� ������ ����� �±� (���� ���� Ŭ�п� �̸� �±׸� �����ؾ� ��)
    public string puzzlePieceTag = "shape";  // Inspector���� ���� ������ ���� �±� ����

    // �ر׸� �������� ���� �ִ� �� ������Ʈ (�θ� ������Ʈ)
    public GameObject basePieceGroup;

    // ��� ���� ����
    public float positionTolerance = 0.1f;
    public float rotationTolerance = 5f;

    // ��ġ�� ���� ���� ����
    private int matchingPieceCount = 0;

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

        isButtonClicked = true;  // ��ư�� �������� ǥ��
        CalculateMatches();
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
        // ��ư�� Ŭ���� �Ŀ��� �� �ڵ尡 �����
        if (isButtonClicked && Input.GetMouseButtonDown(0))
        {
            ExecuteRaycast();
        }
    }

    // Raycast ���� ������ �� �Լ��� �и�
    void ExecuteRaycast()
    {
        Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rayOrigin.z = 0f; // 2D������ z ���� 0���� ���� (z ���� ������� ����)

        // "shape" ���̾ �ش��ϴ� ���̾� ����ũ ����
        int layerMask = 1 << LayerMask.NameToLayer("shape");

        // Raycast�� Ư�� ���̾�� ����
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
            Debug.Log($"���õ� ������ �̸�: {hit.transform.name}");

            // �θ� ������Ʈ �������� (�ڽ��� �ֻ����� �θ� null�� �� ����)
            Transform parent = hit.transform.parent;

            if (parent != null) // �θ� �ִ� ���
            {
                // �θ� �Ʒ��� ��� �ڽ� ������Ʈ ��ȸ
                foreach (Transform sibling in parent)
                {
                    SpriteRenderer siblingRenderer = sibling.GetComponent<SpriteRenderer>();
                    Debug.Log(sibling.name);
                    // �ڽĿ� SpriteRenderer�� ������ ���� ����
                    if (siblingRenderer != null)
                    {
                        siblingRenderer.color = selectedColor;
                        Debug.Log($"{sibling.name}�� ������ {selectedColor}�� ����Ǿ����ϴ�.");
                    }
                }
            }
            else // �θ� ���� ��� (�ֻ��� ������Ʈ)
            {
                SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = selectedColor;
                    Debug.Log($"{hit.transform.name}�� ������ {selectedColor}�� ����Ǿ����ϴ�.");
                }
            }
        }
        else
        {
            Debug.Log("�浹�� ������Ʈ�� �����ϴ�.");
        }
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    void CalculateMatches()
    {
        /// �±׷� ���� ���� Ŭ�е��� �ڵ����� ã��
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // ���� ���� ���� �� �ر׸� ���� ���� ���
        int puzzlePieceCount = puzzlePieceClones.Length;
        int basePieceCount = basePieceGroup.transform.childCount;

        Debug.Log("���� ���� ����: " + puzzlePieceCount);
        Debug.Log("�ر׸� ���� ����: " + basePieceCount);

        // ��ġ�� ���� ���� ���� �ʱ�ȭ
        matchingPieceCount = 0;

        // �� ���� ���� Ŭ�а� �ر׸� ���� ���� ��ġ�� ��
        foreach (GameObject puzzlePiece in puzzlePieceClones)
        {
            // ���� ������ �ر׸� ���� ��ġ ���� ��
            CheckMatch(puzzlePiece);
        }

        // ��ġ�� ���� ���� ���� ���
        Debug.Log("��ġ�� ���� ���� ����: " + matchingPieceCount);
    }

    // ���� ������ ���� �ر׸� ���� ���� ��ġ ���� ��
    void CheckMatch(GameObject puzzlePiece)
    {
        // ��ġ ���θ� ������ ����
        bool hasMatched = false;

        // �θ� ������Ʈ(basePieceGroup)�� �ڽ� ������Ʈ���� ��ȸ�ϸ� ��
        foreach (Transform basePieceTransform in basePieceGroup.transform)
        {
            GameObject basePiece = basePieceTransform.gameObject;

            // 1. ��ġ ��
            bool isPositionMatch = Vector3.Distance(puzzlePiece.transform.position, basePiece.transform.position) < positionTolerance;

            // 2. ȸ�� ��
            bool isRotationMatch = Mathf.Abs(Quaternion.Angle(puzzlePiece.transform.rotation, basePiece.transform.rotation)) < rotationTolerance;

            // 3. ũ�� ��
            bool isScaleMatch = puzzlePiece.transform.localScale == basePiece.transform.localScale;

            // 4. ��� ������ ��ġ�ϸ� ��ġ�� ����
            if (isPositionMatch && isRotationMatch && isScaleMatch)
            {
                hasMatched = true;
                break;  // ��ġ�ϴ� �ر׸� ������ ã������ �� �̻� ������ ����
            }
        }

        // ���� ������ ��ġ�� ��� ���� ����
        if (hasMatched)
        {
            matchingPieceCount++;
        }
    }
}