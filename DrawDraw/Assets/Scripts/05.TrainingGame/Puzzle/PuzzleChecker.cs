using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleChecker : MonoBehaviour
{
    public PuzzleMove[] puzzleMoves;

    public GameObject CheckPopup; // 확인 팝업 창
    public GameResultSO gameResult;

    void Start()
    {

    }

    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        SetMoveEnabled("Ignore Raycast");

        // OnBlocker();
    }

    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        // 팝업 비활성화
        CheckPopup.SetActive(false);
        SetMoveEnabled("Parent");

        // OffBlocker();
    }

    // 팝업 : 완성이야 
    public void NextBtn()
    {
        bool allInCorrectPosition = true;

        foreach (PuzzleMove piece in puzzleMoves)
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
            gameResult.score = 100; // 점수 저장 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장
        }
        else
        {
            print("실패");
            gameResult.score = 0; // 점수 저장 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장
        }

        // 결과 화면으로 넘어가기 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "메소드이름", 매개변수 );
    }

    void SetMoveEnabled(string layer)
    {
        // Puzzle의 레이어 설정
        foreach (var puzzle in puzzleMoves)
        {
            puzzle.gameObject.layer = LayerMask.NameToLayer(layer); // layer 이름으로 레이어 변경
        }
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 초 후 실행
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }
}
