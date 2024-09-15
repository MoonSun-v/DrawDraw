using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GoToScene : MonoBehaviour
{
    public void ChangeScene_Sun()
    {
        SceneManager.LoadScene("1Sun");
        Debug.Log("해 도형 조합을 진행합니다.");
    }

    public void ChangeScene_Pinwheel()
    {
        SceneManager.LoadScene("1Pinwheel");
        Debug.Log("바람개비 도형 조합을 진행합니다.");
    }

    public void ChangeScene_map()
    {
        SceneManager.LoadScene("MapScene");
        Debug.Log("맵 화면으로 이동합니다.");
    }

    public void OnRestartButtonClick()
    {
        // 현재 활성화된 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("처음부터 다시 시작합니다.");
    }
}
