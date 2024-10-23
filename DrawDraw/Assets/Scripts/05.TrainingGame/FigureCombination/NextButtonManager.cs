using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // ���� ��ư �׷�
    public CanvasGroup colorButtonGroup;  // ��ĥ ��ư �׷�

    public GameObject[] prefabsToDisable; // ��Ȱ��ȭ�� �������� �迭

    public GameObject completeButtonObject;  // Inspector���� "�ϼ�" ��ư ������Ʈ�� �Ҵ�

    public GameObject check_popup; // PopupManager ��ũ��Ʈ�� ������ ����

    // ���� ������ ����� �±� (���� ���� Ŭ�п� �̸� �±׸� �����ؾ� ��)
    public string puzzlePieceTag = "shape";  // Inspector���� ���� ������ ���� �±� ����

    // �ر׸� �������� ���� �ִ� �� ������Ʈ (�θ� ������Ʈ)
    public GameObject basePieceGroup;

    // ��� ���� ����
    private float positionTolerance = 0.1f;
    private float rotationTolerance = 5f;
    private float localScaleTolerance = 0.2f;

    // ��ġ�� ���� ���� ����
    private int matchingPieceCount = 0;

    void Start()
    {
        // ó�� ������ �� ���� ��ư�� ���̰�, ��ĥ ��ư�� ������ �ʵ��� ����
        //SetCanvasGroupActive(shapeButtonGroup, true);
        //SetCanvasGroupActive(colorButtonGroup, false);
    }

    public void OnNextButtonClick()
    {
        CalculateMatches();

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

        check_popup.SetActive(false);
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

    // ��ġ ���� ���� ���

    public float maxScore = 100f;    // �⺻ ���� (100�� ����)
    public Text ScoreText; // ������ ����� ������ Text Ui

    void CalculateMatches()
    {
        /// �±׷� ���� ���� Ŭ�е��� �ڵ����� ã��
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // ���� ���� ���� �� �ر׸� ���� ���� ���
        int puzzlePieceCount = puzzlePieceClones.Length;  // ���� ���� �����ϴ� ���� ���� Ŭ�е��� �� ���� ���
        int basePieceCount = basePieceGroup.transform.childCount;  // �ر׸� �������� ���� ��� (basePieceGroup�� �ڽ� ����)

        //Debug.Log("���� ���� ����: " + puzzlePieceCount);  // ���� ���� ������ ����� ���
        //Debug.Log("�ر׸� ���� ����: " + basePieceCount);  // �ر׸� ���� ������ ����� ���

        // ��ġ�� ���� ���� ���� �ʱ�ȭ
        matchingPieceCount = 0;  // ����� �ر׸��� ��ġ�� ������ ������ ������ ������ 0���� �ʱ�ȭ

        // �� ���� ���� Ŭ�а� �ر׸� ���� ���� ��ġ�� ��
        foreach (GameObject puzzlePiece in puzzlePieceClones)  // ��� ���� ������ ��ȸ�ϸ�
        {
            // ���� ������ �ر׸� ���� ��ġ ���� ��
            CheckMatch(puzzlePiece);  // �� ���� ������ �ر׸� ������ ������ ��ġ ���θ� Ȯ���ϴ� �Լ� ȣ��
        }

        // ��ġ�� ���� ���� ���� ���
        //Debug.Log("��ġ�� ���� ���� ����: " + matchingPieceCount);  // ��ġ�� ���� ������ ���� ������ ����� ���

        float score;
        // ������ ���� ��� 0�� ��ȯ
        if (basePieceCount == 0 || puzzlePieceCount == 0)
        {
            //Debug.Log("�����̳� �ر׸� ������ �����Ƿ� 0���Դϴ�.");
            score = 0;
        }
        else
        {
            // ��ġ�� ���� ���� ������ 100�� �������� ȯ��
            score = (matchingPieceCount / (float)basePieceCount) * 50f;
        }

        // ���� ScoreText.text ���� ���ڷ� ��ȯ �������� Ȯ��
        float scoreValue;

        // ScoreText.text ���� float���� ��ȯ �õ�
        if (float.TryParse(ScoreText.text, out scoreValue))
        {
            // ��ȯ�� ������ ��� ���ο� score�� ����
            scoreValue += score;
        }
        else
        {
            // ��ȯ ���� �� ���� �޽��� ���
            Debug.LogError("ScoreText�� ���� ���ڷ� ��ȯ�� �� �����ϴ�: " + ScoreText.text);
        }

        ScoreText.text = scoreValue.ToString("F0");

        // ���� ���� ���
        Debug.Log("���� ���߱� ����(50�� ����): " + ScoreText.text);
    }

    // ���� ������ ���� �ر׸� ���� ���� ��ġ ���� ��
    void CheckMatch(GameObject puzzlePiece)
    {
        // ��ġ ���θ� ������ ����
        bool hasMatched = false;  // ���� ������ ��ġ�ϴ� �ر׸� ������ ã�Ҵ��� ���θ� ������ ������ false�� �ʱ�ȭ

        // �θ� ������Ʈ(basePieceGroup)�� �ڽ� ������Ʈ���� ��ȸ�ϸ� ��
        foreach (Transform basePieceTransform in basePieceGroup.transform)  // �ر׸� �������� �θ� ������Ʈ�� �ڽĵ�(�ر׸� ������)�� ��ȸ
        {
            GameObject basePiece = basePieceTransform.gameObject;  // ���� �ر׸� ������ ������

            // 1. ��ġ ��
            bool isPositionMatch = Vector3.Distance(puzzlePiece.transform.position, basePiece.transform.position) < positionTolerance;  // ���� ������ �ر׸� ������ ��ġ�� ��� ���� ���� �ִ��� Ȯ��
            //Debug.Log(basePiece+" isPositionMatch :" + isPositionMatch);
            
            // 2. ȸ�� ��
            bool isRotationMatch = Mathf.Abs(Quaternion.Angle(puzzlePiece.transform.rotation, basePiece.transform.rotation)) < rotationTolerance;  // ���� ������ �ر׸� ������ ȸ�� ������ ��� ���� ���� �ִ��� Ȯ��
            //Debug.Log(basePiece + " isRotationMatch :" + isRotationMatch);
            
            // 3. ũ�� ��
            //bool isScaleMatch = puzzlePiece.transform.localScale == basePiece.transform.localScale;  // ���� ������ �ر׸� ������ ũ�Ⱑ ������ Ȯ��
            // �� ���� ������ ���� ������ localScale ��
            bool isScaleMatch = Mathf.Abs(puzzlePiece.transform.localScale.x - basePiece.transform.localScale.x) < localScaleTolerance &&
                                Mathf.Abs(puzzlePiece.transform.localScale.y - basePiece.transform.localScale.y) < localScaleTolerance &&
                                Mathf.Abs(puzzlePiece.transform.localScale.z - basePiece.transform.localScale.z) < localScaleTolerance;     
          
            // 4. ��� ������ ��ġ�ϸ� ��ġ�� ����
            if (isPositionMatch && isRotationMatch && isScaleMatch)  // ��ġ, ȸ��, ũ�� �񱳰� ��� ��ġ�� ���
            {
                hasMatched = true;  // ��Ī�� ������ �����ϰ�, ��ġ �÷��׸� true�� ����
                break;  // ��ġ�ϴ� �ر׸� ������ ã�����Ƿ� �� �̻� ������ �ʰ� �ݺ��� Ż��
            }
        }

        // ���� ������ ��ġ�� ��� ���� ����
        if (hasMatched)  // ��ġ�� ���� ������ �����ϴ� ���
        {
            matchingPieceCount++;  // ��ġ�� ���� ������ ������ 1 ������Ŵ
        }
    }

}