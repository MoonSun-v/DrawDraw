using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject current_popup;
    //public resultPopupManager result_popup; // PopupManager ��ũ��Ʈ�� ������ ����
    public GameObject check_popup; // PopupManager ��ũ��Ʈ�� ������ ����

    //int Score; // �� �׸��� ���ӿ����� ���� ���� 

    public Text ScoreText; // ������ ����� ������ Text Ui

    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO
    internal object onClick;

    public void OnClick_close() // 'â �ݱ�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        current_popup.transform.gameObject.SetActive(false);
    }
    public void ShowCheckPopup()
    {
        check_popup.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
    }
    public void OnClick_finish() // ���â�� '�ϼ��̾�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        // "�ϼ��̾�" ��ư Ŭ�� �� : ��� ȭ������ �Ѿ�ϴ�.
        //if (gameResult == null)
        //{
        //    Debug.LogError("GameResult�� �Ҵ���� �ʾҽ��ϴ�.");
        //    return;
        //}

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

