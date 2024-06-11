using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TangramChecker : MonoBehaviour
{
    public Tangram[] puzzlePieces;

    public GameObject CheckPopup; // Ȯ�� �˾� â
    public Text ScoreText; // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ
    public GameResultSO gameResult;

    void Start()
    {
    }

    /*
    public void OnCheckCompletion()
    {
        bool allInCorrectPosition = true;

        foreach (Tangram piece in puzzlePieces)
        {
            if (!piece.IsInCorrectPosition())
            {
                allInCorrectPosition = false;
                break;
            }
        }

        if (allInCorrectPosition)
        {
            Debug.Log("Puzzle completed successfully!");
            
            if (completedText != null)
            {
                completedText.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Puzzle is not completed yet.");
        }
    }
    */

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        // OnBlocker();
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        // �˾� ��Ȱ��ȭ
        CheckPopup.SetActive(false);
        // OffBlocker();
    }

    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        bool allInCorrectPosition = true;

        foreach (Tangram piece in puzzlePieces)
        {
            if (!piece.IsInCorrectPosition())
            {
                allInCorrectPosition = false;
                break;
            }
        }

        if (allInCorrectPosition)
        {
            print("����");
            ScoreText.text = "����";
            gameResult.score = 100; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        }
        else
        {
            print("����");
            ScoreText.text = "����";
            gameResult.score = 100; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        }

        // ��� ȭ������ �Ѿ�� 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }
}
