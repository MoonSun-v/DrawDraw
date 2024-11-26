using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _TangramSceneChanage : MonoBehaviour
{
    // 버튼 클릭 시 호출할 함수
    public void BoatTutorialScene()
    {
        // SceneManager를 사용해 씬 이동
        SceneManager.LoadScene("T_TangramScene_Lv1_boat");
    }

    public void duckTutorialScene()
    {
        // SceneManager를 사용해 씬 이동
        SceneManager.LoadScene("T_TangramScene_Lv1_duck");
    }
}
