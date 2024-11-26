using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMatcher : MonoBehaviour
{
    // ���� ������ ����� �±� (���� ���� Ŭ�п� �̸� �±׸� �����ؾ� ��)
    public string puzzlePieceTag = "shape";  // Inspector���� ���� ������ ���� �±� ����

    // �ر׸� �������� ���� �ִ� �� ������Ʈ (�θ� ������Ʈ)
    public GameObject basePieceGroup;

    // ��� ���� ����
    public float positionTolerance = 0.1f;
    public float rotationTolerance = 5f;

    // UI ��ư
    public Button calculateButton;  // Inspector���� ��ư�� �Ҵ�

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ����
        //calculateButton.onClick.AddListener(CalculateMatches);
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    void CalculateMatches()
    {
        // �±׷� ���� ���� Ŭ�е��� �ڵ����� ã��
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // �� ���� ���� Ŭ�а� �ر׸� ���� ���� ��ġ�� ��
        foreach (GameObject puzzlePiece in puzzlePieceClones)
        {
            // ���� ������ �ر׸� ���� ��ġ ���� ��
            CheckMatch(puzzlePiece);
        }
    }

    // ���� ������ ���� �ر׸� ���� ���� ��ġ ���� ��
    void CheckMatch(GameObject puzzlePiece)
    {
        GameObject bestMatch = null;
        float bestMatchPercentage = 0f;

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

            // 4. ��ġ��(��Ȯ��) ���
            float matchPercentage = 0f;
            if (isPositionMatch) matchPercentage += 33.3f;
            if (isRotationMatch) matchPercentage += 33.3f;
            if (isScaleMatch) matchPercentage += 33.3f;

            // ���� �ر׸� ������ ���� �� �´��� Ȯ��
            if (matchPercentage > bestMatchPercentage)
            {
                bestMatch = basePiece;
                bestMatchPercentage = matchPercentage;
            }
        }

        // ���� �� �´� �ر׸� ������ ���� ��� ���
        if (bestMatch != null)
        {
            Debug.Log("���� ���� " + puzzlePiece.name + "�� ���� �� �´� �ر׸� ����: " + bestMatch.name);
            Debug.Log("��ġ��: " + bestMatchPercentage + "%");
        }
        else
        {
            Debug.Log("���� ���� " + puzzlePiece.name + "�� ��ġ�ϴ� �ر׸� ������ �����ϴ�.");
        }
    }
}