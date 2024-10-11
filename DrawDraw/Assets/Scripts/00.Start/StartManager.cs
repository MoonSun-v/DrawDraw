using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    // ���� �׽�Ʈ �����Ͱ� ������ ���ѷα� �ǳʶٱ�
    public void ClickStart()
    {
        // Ű�� 0�� �׽�Ʈ ����� �����ϴ��� Ȯ��
        if (GameData.instance.testdata.TestResults.ContainsKey(0))
        {
            SceneManager.LoadScene("SelectScene");
        }
        else
        {
            Debug.Log("������ ó�� �����մϴ�. ���ѷα׿� �����մϴ�.");
            SceneManager.LoadScene("Profile");
        }

    }

    public void ClickDataReset()
    {

    }
}
