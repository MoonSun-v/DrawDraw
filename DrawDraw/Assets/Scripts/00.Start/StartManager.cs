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

        if (GameData.instance.testdata.TestResults.ContainsKey(0))
        {
            TestResultData resultData = GameData.instance.testdata.TestResults[0];

            if (resultData.Game10Score != 0)
            {
                Debug.Log("���� �����Ͱ� �����մϴ�");
                SceneManager.LoadScene("SelectScene");
            }
            else
            {
                Debug.Log("���� �׽�Ʈ ������ ������� �ʽ��ϴ�. ���ѷα׿� �����մϴ�.");
                SceneManager.LoadScene("Profile");
            }
        }
        else
        {
            Debug.Log("���� �׽�Ʈ ������ �����ϴ�. ���ѷα׿� �����մϴ�.");
            SceneManager.LoadScene("Profile");
        }

    }

    
}
