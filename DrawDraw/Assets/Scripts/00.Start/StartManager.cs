using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    // [ ���� ���� ��ư Ŭ�� ]
    // ���� �׽�Ʈ �����Ͱ� ������ ���ѷα� �ǳʶٱ� : Ű�� 0�� �׽�Ʈ�� Game10Score�� �����ϴ��� Ȯ�� 
    // 
    public void GameStart()
    {
        // �ÿ��� 
        // ����ġ ���� , �������� ����
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

            // ���� �ÿ��� : �׽�Ʈ�� �ѹ��� ����
            // 
            // PlayerExp�� ���� Ȯ���� TestResults Ű ����
            
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
                Debug.Log("���ǿ� ���� Game10Score�� 0�� �����Ͱ� �ֽ��ϴ�. TestStartScene���� �̵��մϴ�.");
                SceneManager.LoadScene("TestStartScene");
                return;
            }
            

            if (GameData.instance.testdata.TestResults[0].Game10Score != 0)
            {
                Debug.Log("���� �����Ͱ� �����մϴ�. MapScene���� �̵��մϴ�.");
                SceneManager.LoadScene("MapScene");
            }
            else
            {
                Debug.Log("���� �׽�Ʈ ������ ������� �ʽ��ϴ�. ���ѷα׿� �����մϴ�.");
                SceneManager.LoadScene("Prolog"); // ������ -> ���ѷα� ���� ������ ����
            }
        }
        else
        {
            Debug.Log("���� �׽�Ʈ ������ �����ϴ�. ���ѷα׿� �����մϴ�.");
            SceneManager.LoadScene("Prolog"); // ������ -> ���ѷα� ���� ������ ����
        }*/
    }

    private bool CheckTestResultForGame10Score(int key)
    {
        // ������ Ű�� ���� TestResults �˻�
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
