using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToOtherScene : MonoBehaviour
{
    public void GoToMapButton()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void GoToSelectSceneButton()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void GoToDotLineButton()
    {
        SceneManager.LoadScene("DotLineScene");
    }

    public void GoToLineButton()
    {
        SceneManager.LoadScene("LineScene");
    }

    public void GoToScratchButton()
    {
        SceneManager.LoadScene("ScratchScene");
    }

    public void GoToTangramButton()
    {
        SceneManager.LoadScene("TangramScene");
    }

    public void GoToFigureCombinationButton()
    {
        SceneManager.LoadScene("FigureSelectionScene");
    }

    public void GoToPuzzle()
    {
        SceneManager.LoadScene("PuzzleScene");
    }
}