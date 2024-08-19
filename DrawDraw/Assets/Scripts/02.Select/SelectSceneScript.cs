using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneScipt : MonoBehaviour
{
    public void ChangeScene_TrainingGame()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void ChangeScene_TestGame()
    {
        SceneManager.LoadScene("TestGameScene");
    }

    public void ChangeScene_Parents()
    {
        SceneManager.LoadScene("");
    }
}
