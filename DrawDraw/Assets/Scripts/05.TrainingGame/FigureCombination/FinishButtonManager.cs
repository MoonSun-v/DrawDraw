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
        int puzzlePieceCount = puzzlePieceClones.Length;  // ���� ���� �����ϴ� ���� ���� Ŭ�е��� �� ���� ���
        int basePieceCount = basePieceGroup.transform.childCount;  // �ر׸� �������� ���� ��� (basePieceGroup�� �ڽ� ����)

        Debug.Log("���� ���� ����: " + puzzlePieceCount);  // ���� ���� ������ ����� ���
        Debug.Log("�ر׸� ���� ����: " + basePieceCount);  // �ر׸� ���� ������ ����� ���

        // ��ġ�� ���� ���� ���� �ʱ�ȭ
        matchingPieceCount = 0;  // ����� �ر׸��� ��ġ�� ������ ������ ������ ������ 0���� �ʱ�ȭ

        // �� ���� ���� Ŭ�а� �ر׸� ���� ���� ��ġ�� ��
        foreach (GameObject puzzlePiece in puzzlePieceClones)  // ��� ���� ������ ��ȸ�ϸ�
        {
            // ���� ������ �ر׸� ���� ��ġ ���� ��
            CheckMatch(puzzlePiece);  // �� ���� ������ �ر׸� ������ ������ ��ġ ���θ� Ȯ���ϴ� �Լ� ȣ��
        }

        // ��ġ�� ���� ���� ���� ���
        Debug.Log("��ġ�� ���� ���� ����: " + matchingPieceCount);  // ��ġ�� ���� ������ ���� ������ ����� ���
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

            // 2. ȸ�� ��
            bool isRotationMatch = Mathf.Abs(Quaternion.Angle(puzzlePiece.transform.rotation, basePiece.transform.rotation)) < rotationTolerance;  // ���� ������ �ر׸� ������ ȸ�� ������ ��� ���� ���� �ִ��� Ȯ��

            // 3. ũ�� ��
            bool isScaleMatch = puzzlePiece.transform.localScale == basePiece.transform.localScale;  // ���� ������ �ر׸� ������ ũ�Ⱑ ������ Ȯ��
                                                                                                     

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
