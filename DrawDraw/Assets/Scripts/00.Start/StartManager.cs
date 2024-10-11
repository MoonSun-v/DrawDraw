using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    // 사전 테스트 데이터가 있으면 프롤로그 건너뛰기
    public void ClickStart()
    {
        // 키가 0인 테스트 결과가 존재하는지 확인
        if (GameData.instance.testdata.TestResults.ContainsKey(0))
        {
            SceneManager.LoadScene("SelectScene");
        }
        else
        {
            Debug.Log("게임을 처음 시작합니다. 프롤로그에 입장합니다.");
            SceneManager.LoadScene("Profile");
        }

    }

    public void ClickDataReset()
    {

    }
}
