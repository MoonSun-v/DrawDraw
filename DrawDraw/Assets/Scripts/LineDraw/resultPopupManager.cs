using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultPopupManager : MonoBehaviour
{
    public Text Text_GameResult; // ������ ����� ǥ������ Text Ui
    public Text ScoreText; // ������ ����� ������ Text Ui\

    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO

    public void Show()
    {
        Text_GameResult.text = ScoreText.text+"�� ������ϴ�."; // �˾��� ���� â�� ���� ������ ǥ���Ѵ�.
        transform.gameObject.SetActive(true); // ��� �˾� â�� ȭ�鿡 ǥ��
    }

    public void OnClick_finish() // ���â�� '�ϼ��̾�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        // "�ϼ��̾�" ��ư Ŭ�� �� : ��� ȭ������ �Ѿ�ϴ�.
        if (gameResult == null)
        {
            Debug.LogError("GameResult�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // ���� ���� -> ��� ȭ�鿡�� ����Ŭ����/���� ���� ���ؼ�
        gameResult.score = int.Parse(ScoreText.text);

        // ���� �� �̸� ���� : ��� â���� �ش� �������� ���ƿ��� ���ؼ� 
        gameResult.previousScene = SceneManager.GetActiveScene().name;

        // ��� ȭ������ �Ѿ�� 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
    }

    IEnumerator ResultSceneDelay()
    {
       // 2 �� �� ����
       yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }

}
