using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class TestFinish : MonoBehaviour
{

    // �÷��̾� ĳ���� ���� �ҷ�����
    bool isDog;

    public GameObject cat;
    public GameObject dog;

    void Start()
    {
        isDog = !GameData.instance.playerdata.PlayerCharacter;  // �������� true, ����̸� false
        if (isDog) { dog.SetActive(false); }
        else { cat.SetActive(true); }

        StartCoroutine(LoadMapSceneAfterDelay());
    }

    IEnumerator LoadMapSceneAfterDelay()
    {
        // 5�� ���
        yield return new WaitForSeconds(5f);
        /*
        if (GameData.instance.testdata.TestResults.Count > 5)
        {
            // ������ �׽�Ʈ��� -> ���������� �̵�
            // ** ��ũ��Ʈ �߰� �ʿ� **
            print("������ �׽�Ʈ�����Ƿ�, ���������� �̵��մϴ�.");
            SceneManager.LoadScene("EndingScene");

            // ������ ������ ���� �ڵ� �߰� �ʿ� 
            // GameData.instance.trainingdata.ClearStage[19] = true; // ���� �������� ������ �Ϸ�ó�� ���ؼ�..!
        }
        else
        {
            // ������ �׽�Ʈ�� �ƴ϶�� -> ��ȭ������ �̵� 
            SceneManager.LoadScene("MapScene");
        }
        */
        // "MapScene" �̸��� ������ �̵�
        SceneManager.LoadScene("MapScene");
    }
}
