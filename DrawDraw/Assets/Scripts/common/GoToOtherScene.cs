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
}
