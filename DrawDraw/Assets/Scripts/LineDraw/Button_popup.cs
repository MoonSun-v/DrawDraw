using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject popup; 
    public Text ScoreText; // ������ ����� ������ Text Ui
    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO

    int Score; // �� �׸��� ���ӿ����� ���� ���� 

    public void OnClick_close() // 'â �ݱ�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        popup.transform.gameObject.SetActive(false);
    }

    public void OnClick_next() // '���� ��������' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        //SceneManager.LoadScene("NextScene"); 
        // Debug.Log("���� �������� �Ѿ�ϴ�.");



        // "�ϼ��̾�" ��ư Ŭ�� �� : ��� ȭ������ �Ѿ�ϴ�.

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

