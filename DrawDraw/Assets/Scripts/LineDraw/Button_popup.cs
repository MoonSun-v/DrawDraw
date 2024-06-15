using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject current_popup; 
    public Text ScoreText; // ������ ����� ������ Text Ui
    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO
    public resultPopupManager result_popup; // PopupManager ��ũ��Ʈ�� ������ ����

    int Score; // �� �׸��� ���ӿ����� ���� ���� 

    public void OnClick_close() // 'â �ݱ�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        current_popup.transform.gameObject.SetActive(false);
    }

    public void OnClick_next() // ���â�� '�ϼ��̾�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        // "�ϼ��̾�" ��ư Ŭ�� �� : ��� ȭ������ �Ѿ�ϴ�.

        // ���� ���� -> ��� ȭ�鿡�� ����Ŭ����/���� ���� ���ؼ�
        gameResult.score = int.Parse(ScoreText.text);

        // ���� �� �̸� ���� : ��� â���� �ش� �������� ���ƿ��� ���ؼ� 
        gameResult.previousScene = SceneManager.GetActiveScene().name; 


        // ��� ȭ������ �Ѿ�� 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
    }

    public void OnClick_result() // Ȯ��â �ϼ� ��ư�� Ŭ�� -> ��� �����ֱ�
    {
        current_popup.transform.gameObject.SetActive(false);
        result_popup.Show(); // ��� �˾� ����
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }
}

