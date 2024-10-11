using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    // [ 게임 시작 버튼 클릭 ]
    // 사전 테스트 데이터가 있으면 프롤로그 건너뛰기 : 키가 0인 테스트의 Game10Score가 존재하는지 확인 
    // 
    public void GameStart()
    {

        if (GameData.instance.testdata.TestResults.ContainsKey(0))
        {
            TestResultData resultData = GameData.instance.testdata.TestResults[0];

            if (resultData.Game10Score != 0)
            {
                Debug.Log("기존 데이터가 존재합니다");
                SceneManager.LoadScene("SelectScene");
            }
            else
            {
                Debug.Log("사전 테스트 정보가 충분하지 않습니다. 프롤로그에 입장합니다.");
                SceneManager.LoadScene("Profile");
            }
        }
        else
        {
            Debug.Log("사전 테스트 정보가 없습니다. 프롤로그에 입장합니다.");
            SceneManager.LoadScene("Profile");
        }

    }

    
}
