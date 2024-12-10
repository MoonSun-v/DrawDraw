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
        // 시연용 
        // 경험치 리셋 , 스테이지 리셋
        GameData.instance.playerdata.PlayerExp = 0;

        for (int i = 0; i < GameData.instance.trainingdata.ClearStage.Length; i++)
        {
            GameData.instance.trainingdata.ClearStage[i] = false;
        }

        SceneManager.LoadScene("Prolog");

        /*
        if (GameData.instance.testdata.TestResults.ContainsKey(0))
        {
            // bool shouldGoToTestStartScene = false;
            // int playerExp = GameData.instance.playerdata.PlayerExp;

            // 전시 시연용 : 테스트는 한번만 진행
            // 
            // PlayerExp에 따라 확인할 TestResults 키 결정
            
            if (playerExp >= 40 && playerExp < 80)
            {
                shouldGoToTestStartScene = CheckTestResultForGame10Score(1);
            }
            else if (playerExp >= 80 && playerExp < 120)
            {
                shouldGoToTestStartScene = CheckTestResultForGame10Score(2);
            }
            else if (playerExp >= 120 && playerExp < 160)
            {
                shouldGoToTestStartScene = CheckTestResultForGame10Score(3);
            }
            else if (playerExp >= 160 && playerExp < 190)
            {
                shouldGoToTestStartScene = CheckTestResultForGame10Score(4);
            }
            else if (playerExp >= 190)
            {
                shouldGoToTestStartScene = CheckTestResultForGame10Score(5);
            }
            

            if (shouldGoToTestStartScene)
            {
                Debug.Log("조건에 따라 Game10Score가 0인 데이터가 있습니다. TestStartScene으로 이동합니다.");
                SceneManager.LoadScene("TestStartScene");
                return;
            }
            

            if (GameData.instance.testdata.TestResults[0].Game10Score != 0)
            {
                Debug.Log("기존 데이터가 존재합니다. MapScene으로 이동합니다.");
                SceneManager.LoadScene("MapScene");
            }
            else
            {
                Debug.Log("사전 테스트 정보가 충분하지 않습니다. 프롤로그에 입장합니다.");
                SceneManager.LoadScene("Prolog"); // 프로필 -> 프롤로그 영상 씬으로 수정
            }
        }
        else
        {
            Debug.Log("사전 테스트 정보가 없습니다. 프롤로그에 입장합니다.");
            SceneManager.LoadScene("Prolog"); // 프로필 -> 프롤로그 영상 씬으로 수정
        }*/
    }

    private bool CheckTestResultForGame10Score(int key)
    {
        // 지정된 키에 대한 TestResults 검사
        if (GameData.instance.testdata.TestResults.ContainsKey(key)) 
        {
            TestResultData resultData = GameData.instance.testdata.TestResults[key];
            return resultData.Game10Score == 0;
        }
        else
        {
            return true;
        }
    }
}
