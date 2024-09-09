using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonManager : MonoBehaviour
{
    // ���� ������ ����� �±� (���� ���� Ŭ�п� �̸� �±׸� �����ؾ� ��)
    public string puzzlePieceTag = "shape";  // Inspector���� ���� ������ ���� �±� ����

    // �ر׸� �������� ���� �ִ� �� ������Ʈ (�θ� ������Ʈ)
    public GameObject basePieceGroup;

    // ��� ���� ����
    public float positionTolerance = 0.1f;
    public float rotationTolerance = 5f;

    // ��ġ�� ���� ���� ����
    private int matchingPieceCount = 0;

    private ColorButtonManager shapeColorChanger;

    void Start()
    {
        CalculateMatches();

        // ShapeColorChanger ��ũ��Ʈ�� ���� ������Ʈ�� ã��
        GameObject colorChangerObject = GameObject.Find("ColorManager"); // "ColorManager"�� ColorButtonManager �پ� �ִ� ������Ʈ �̸�
        if (colorChangerObject != null)
        {
            shapeColorChanger = colorChangerObject.GetComponent<ColorButtonManager>();
        }
    }

    public void OnFinishButtonClick()
    {
        //Debug.Log("�ϼ� ��ư Ŭ��, ���� ������ ���� ���� ����ϱ�");
        DisplayColorPieceCounts();
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

    // ��ü ���� ���� Ŭ�� ������ ������ ����� ���� ������ ����ϴ� �Լ�
    void DisplayColorPieceCounts()
    {
        /// �±׷� ���� ���� Ŭ�е��� �ڵ����� ã��
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // ���� ���� ���� �� �ر׸� ���� ���� ���
        int totalPieces = puzzlePieceClones.Length;

        // ����� ���� ������ ������ (ShapeColorChanger ��ũ��Ʈ����)
        int changedPieces = shapeColorChanger != null ? shapeColorChanger.GetChangedShapeCount() : 0;

        // �ֿܼ� ���
        Debug.Log($"��ü ���� ���� ����: {totalPieces}");
        Debug.Log($"������ ����� ���� ����: {changedPieces}");
    }
      
}
