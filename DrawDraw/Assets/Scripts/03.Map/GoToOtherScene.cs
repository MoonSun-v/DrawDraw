using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToOtherScene : MonoBehaviour
{

    public void GoToMapButton() { SceneManager.LoadScene("MapScene"); }

    public void GoToSelectSceneButton() { SceneManager.LoadScene("SelectScene"); }



    // ★ [ 각 훈련 게임 스테이지로 이동하는 버튼 메소드 ] ★
    //
    // 1. 각 훈련 게임 1단계는 튜토리얼 영상 씬으로 이동
    //    ( 도형조합, 칠교는 도안 선택 후 튜토리얼 영상 시청 후 게임 씬으로 이동 ) 

    // 점선 따라 그리기
    public void GoToDotLineButton_1() { SceneManager.LoadScene("T_DotLineScene1"); }
    public void GoToDotLineButton_2() { SceneManager.LoadScene("DotLineScene2"); }
    public void GoToDotLineButton_3() { SceneManager.LoadScene("DotLineScene3"); }


    // 선 따라 그리기
    public void GoToLineButton_1() { SceneManager.LoadScene("T_1LineScene"); }
    public void GoToLineButton_2() { SceneManager.LoadScene("2LineScene"); }
    public void GoToLineButton_3() { SceneManager.LoadScene("3LineScene"); }
    public void GoToLineButton_4() { SceneManager.LoadScene("4LineScene"); }
    public void GoToLineButton_5() { SceneManager.LoadScene("5LineScene"); }
    public void GoToLineButton_6() { SceneManager.LoadScene("6LineScene"); }


    // 스크래치
    public void GoToScratchButton_1() { SceneManager.LoadScene("T_ScratchScene1"); }
    public void GoToScratchButton_2() { SceneManager.LoadScene("ScratchScene2"); }


    // 칠교
    public void GoToTangramButton_1() { SceneManager.LoadScene("1TangramSelect"); }
    public void GoToTangramButton_2() { SceneManager.LoadScene("TangramScene_Lv2"); }
    public void GoToTangramButton_3() { SceneManager.LoadScene("TangramScene_Lv3"); }


    // 도형 조합
    public void GoToFigureCombinationButton_1() { SceneManager.LoadScene("1FigurSelect"); }
    public void GoToFigureCombinationButton_2() { SceneManager.LoadScene("2FigurSelect"); }
    public void GoToFigureCombinationButton_3() { SceneManager.LoadScene("3FigurSelect"); }


    // 퍼즐
    public void GoToPuzzle_1() { SceneManager.LoadScene("T_PuzzleScene_1"); }
    public void GoToPuzzle_2() { SceneManager.LoadScene("PuzzleScene_2"); }


    // -------------------------------------------------------------------------------------------
    // ★ [ 마지막 스테이지 ]
    //
    // 1. 진행 가능한 테스트가 1번 이상 남아있다면 -> 테스트 씬으로 넘어가기
    //    ( 도장 완료 처리 기준 : 엔딩 씬 플레이 했는지? )
    // 2. 모든 테스트를 완료했다면 -> 엔딩씬으로 넘어가기 

    // 테스트 마지막 씬에서 -> 마지막 테스트 였다면? -> 엔딩 씬으로 넘어가기

    public void GoToLastStage()
    {
        if(GameData.instance.testdata.TestResults.Count < 6)
        {
            SceneManager.LoadScene("TestStartScene");
        }
        else
        {
            // 엔딩씬으로 이동
            print("모든 테스트를 완료하여 엔딩 씬으로 이동합니다.");
            SceneManager.LoadScene("EndingScene");
        }
    }

}