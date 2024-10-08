using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TangramChecker : MonoBehaviour
{
    public Tangram[] puzzlePieces;

    public GameObject CheckPopup; // 확인 팝업 창
    //public Text ScoreText; // 임시 점수 표시용 텍스트
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

    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        // OnBlocker();
    }

    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        // 팝업 비활성화
        CheckPopup.SetActive(false);
        // OffBlocker();
    }

    // 팝업 : 완성이야 
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
            print("성공");
            //ScoreText.text = "성공";
            gameResult.score = 100; // 점수 저장 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장
        }
        else
        {
            print("실패");
            //ScoreText.text = "실패";
            gameResult.score = 0; // 점수 저장 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장
        }

        HidePopup();
        Silhouettes.SetActive(false);
        Pieces.SetActive(false);
        hintBtn.SetActive(false);
        AnswerImage.SetActive(true);

        // 결과 화면으로 넘어가기
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "메소드이름", 매개변수 )

    }

    private void HidePopup()
    {
        CanvasGroup canvasGroup = CheckPopup.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;  // 팝업을 투명하게 만듦
            canvasGroup.blocksRaycasts = false;  // 클릭 차단 해제
        }
    }

    IEnumerator ResultSceneDelay()
    {
        // 5 초 후 실행
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("ResultScene");
    }
}
