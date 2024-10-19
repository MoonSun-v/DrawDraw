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

    //public Text Text_GameResult; // ������ ����� ǥ������ Text Ui
    public Text ScoreText; // ������ ����� ������ Text Ui

    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO
    internal object onClick;

    // �⺻ ���� (100�� ����)
    public float maxScore = 100f;

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
        gameResult.score = int.Parse(DisplayColorPieceCounts());

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

    // ��ü ���� ���� Ŭ�� ������ ������ ����� ���� ������ ����ϴ� �Լ�
    string DisplayColorPieceCounts()
    {
        /// �±׷� ���� ���� Ŭ�е��� �ڵ����� ã��
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // ���� ���� ���� �� �ر׸� ���� ���� ���
        int totalPieces = puzzlePieceClones.Length;

        // ����� ���� ������ ������ (ShapeColorChanger ��ũ��Ʈ����)
        int changedPieces = shapeColorChanger != null ? shapeColorChanger.GetChangedShapeCount() : 0; 

        // �ֿܼ� ���
        //Debug.Log($"��ü ���� ���� ����: {totalPieces}");
        //Debug.Log($"������ ����� ���� ����: {changedPieces}");

        // ���� ���: ����� ���� ���� ���� ��ü ���� ���� ���� ���� �� 100���� �������� ������ �ο�
        float score = 0f;
        if (totalPieces > 0)
        {
            score = (changedPieces / (float)totalPieces) * 50f; //100�� ����
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
        Debug.Log("���� ��ĥ�ϱ� ����(50�� ����): " + score);
        Debug.Log("���� ����: " + ScoreText.text);

        return ScoreText.text;
    }
}
