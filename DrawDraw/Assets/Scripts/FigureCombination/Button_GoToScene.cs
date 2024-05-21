using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GoToScene : MonoBehaviour
{
    public void ChangeScene_waterDrop()
    {
        SceneManager.LoadScene("WaterDropScene");
        Debug.Log("물방울 도형 조합을 진행합니다.");
    }

    public void ChangeScene_snail()
    {
        SceneManager.LoadScene("SnailScene");
        Debug.Log("달팽이 도형 조합을 진행합니다.");
    }

    public void ChangeScene_map()
    {
        SceneManager.LoadScene("MapScene");
        Debug.Log("맵 화면으로 이동합니다.");
    }
}
