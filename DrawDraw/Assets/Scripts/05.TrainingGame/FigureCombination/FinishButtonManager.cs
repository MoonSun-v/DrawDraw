using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishButtonManager : MonoBehaviour
{
    // ���� ������ ����� �±� (���� ���� Ŭ�п� �̸� �±׸� �����ؾ� ��)
    public string puzzlePieceTag = "shape";  // Inspector���� ���� ������ ���� �±� ����

    private ColorButtonManager shapeColorChanger;

    public GameObject check_popup; // PopupManager ��ũ��Ʈ�� ������ ����
    public GameObject result_popup;

    public Text Text_GameResult; // ������ ����� ǥ������ Text Ui
    public Text ScoreText; // ������ ����� ������ Text Ui

    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO
    internal object onClick;

    void Start()
    {
        // ShapeColorChanger ��ũ��Ʈ�� ���� ������Ʈ�� ã��
        GameObject colorChangerObject = GameObject.Find("ColorManager"); // "ColorManager"�� ColorButtonManager �پ� �ִ� ������Ʈ �̸�
        if (colorChangerObject != null)
        {
            shapeColorChanger = colorChangerObject.GetComponent<ColorButtonManager>();
        }
    }

    public void onCheckPop() // Ȯ��â ����
    {
        check_popup.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
    }

    public void OnFinishButtonClick()
    {
        

        // ���� ���� -> ��� ȭ�鿡�� ����Ŭ����/���� ���� ���ؼ�
        gameResult.score = int.Parse(ScoreText.text);


        // ���� �� �̸� ���� : ��� â���� �ش� �������� ���ƿ��� ���ؼ� 
        gameResult.previousScene = SceneManager.GetActiveScene().name;

        // ��� ȭ������ �Ѿ�� 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }

    public void OnResult()
    {
        //Debug.Log("�ϼ� ��ư Ŭ��, ���� ������ ���� ���� ����ϱ�");
        DisplayColorPieceCounts();
        Text_GameResult.text = "�ùٸ� ��ġ�� ��ĥ���� �� ���� ���� : "+ ScoreText.text;

        check_popup.SetActive(false); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
        result_popup.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ�� 
        //Debug.Log("�ϼ� ��ư Ŭ��, ���� ������ ���� ���� ����ϱ�");
        //DisplayColorPieceCounts();
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

        ScoreText.text = changedPieces.ToString();  

        // �ֿܼ� ���
        Debug.Log($"��ü ���� ���� ����: {totalPieces}");
        Debug.Log($"������ ����� ���� ����: {changedPieces}");
    }

    public void GotoResultScene()
    {
        Debug.Log("�ϼ� ��ư Ŭ��, ��� ������ �̵�");

    }

}
