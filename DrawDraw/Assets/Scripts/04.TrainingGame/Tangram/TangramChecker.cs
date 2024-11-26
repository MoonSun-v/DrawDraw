using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TangramChecker : MonoBehaviour
{
    public Tangram[] puzzlePieces;

    public GameObject CheckPopup; // Ȯ�� �˾� â
    //public Text ScoreText; // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ
    public GameResultSO gameResult;

    public GameObject AnswerImage;
    public GameObject Silhouettes;
    public GameObject Pieces;
    public GameObject hintBtn;

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
            //ScoreText.text = "����";
            gameResult.score = 100; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        }
        else
        {
            print("����");
            //ScoreText.text = "����";
            gameResult.score = 0; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        }

        HidePopup();
        Silhouettes.SetActive(false);
        Pieces.SetActive(false);
        hintBtn.SetActive(false);
        AnswerImage.SetActive(true);

        // ��� ȭ������ �Ѿ��
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� )

    }

    private void HidePopup()
    {
        CanvasGroup canvasGroup = CheckPopup.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;  // �˾��� �����ϰ� ����
            canvasGroup.blocksRaycasts = false;  // Ŭ�� ���� ����
        }
    }

    IEnumerator ResultSceneDelay()
    {
        // 5 �� �� ����
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("ResultScene");
    }
}
