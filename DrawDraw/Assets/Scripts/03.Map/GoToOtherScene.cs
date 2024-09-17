using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToOtherScene : MonoBehaviour
{

    public void GoToMapButton() { SceneManager.LoadScene("MapScene"); }

    public void GoToSelectSceneButton() { SceneManager.LoadScene("SelectScene"); }



    // ★ [ 각 훈련 게임 스테이지로 이동하는 버튼 메소드 ] ★


    // 점선 따라 그리기
    public void GoToDotLineButton_1() { SceneManager.LoadScene("DotLineScene1"); }
    public void GoToDotLineButton_2() { SceneManager.LoadScene("DotLineScene2"); }
    public void GoToDotLineButton_3() { SceneManager.LoadScene("DotLineScene3"); }


    // 선 따라 그리기
    public void GoToLineButton_1() { SceneManager.LoadScene("1LineScene"); }
    public void GoToLineButton_2() { SceneManager.LoadScene("2LineScene"); }
    public void GoToLineButton_3() { SceneManager.LoadScene("3LineScene"); }
    public void GoToLineButton_4() { SceneManager.LoadScene("4LineScene"); }
    public void GoToLineButton_5() { SceneManager.LoadScene("5LineScene"); }
    public void GoToLineButton_6() { SceneManager.LoadScene("6LineScene"); }


    // 스크래치
    public void GoToScratchButton_1() { SceneManager.LoadScene("ScratchScene1"); }
    public void GoToScratchButton_2() { SceneManager.LoadScene("ScratchScene2"); }


    // 칠교 -> 씬 추가 필요
    public void GoToTangramButton_1() { SceneManager.LoadScene("TangramScene"); }


    // 도형 조합
    public void GoToFigureCombinationButton_1() { SceneManager.LoadScene("1FigurSelect"); }
    public void GoToFigureCombinationButton_2() { SceneManager.LoadScene("2FigurSelect"); }
    public void GoToFigureCombinationButton_3() { SceneManager.LoadScene("3FigurSelect"); }


    // 퍼즐 -> 씬 추가 필요 
    public void GoToPuzzle_1() { SceneManager.LoadScene("PuzzleScene"); }

}