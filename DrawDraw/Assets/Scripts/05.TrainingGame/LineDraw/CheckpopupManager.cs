using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckpopupManager : MonoBehaviour
{
    public GameObject checkPopup;
    public DrawLine DrawLine;

    public GameObject line1;
    public GameObject line2;
    public GameObject line3;

    public GameObject curveline1;
    public GameObject curveline2;
    public GameObject curveline3;

    public GameObject Shapes1;
    public GameObject Shapes2;
    public GameObject Shapes3;

    public Text ScoreText; // ������ ����� ������ Text Ui

    public GameResultSO gameResult; // ���� ���ȭ�� ���� SO

    public void OnClick_close() // 'â �ݱ�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        checkPopup.transform.gameObject.SetActive(false);
    }
    public void ShowCheckPopup()
    {
        checkPopup.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
    }

    public void OnClick_finish() // Ȯ��â �ϼ� ��ư�� Ŭ�� -> ��� �����ֱ�
    {
        if (line1.activeSelf && curveline1 != null) // ù ��° �׸� �ϼ� -> �� ��° �׸� ����
        {
            // ���� ��Ȱ��ȭ
            line1.SetActive(false);
            line2.SetActive(false);
            line3.SetActive(false);

            if(curveline1 != null)
            {
                //� Ȱ��ȭ
                curveline1.SetActive(true);
                curveline2.SetActive(true);
                curveline3.SetActive(true);
            }
            //�׷��� �� ��� �����
            ClearAllLines();

        }
        else if(curveline1 == null) // ù��° �׸��� ���� ���
        {
            goto_result();
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 == null) // �ر׸� 2��
        {
            goto_result();
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 != null) // �ر׸� 3��
        {
            // ���� ��Ȱ��ȭ
            curveline1.SetActive(false);
            curveline2.SetActive(false);
            curveline3.SetActive(false);

            //� Ȱ��ȭ
            Shapes1.SetActive(true);
            Shapes2.SetActive(true);
            Shapes3.SetActive(true);

            //�׷��� �� ��� �����
            ClearAllLines();

        }
        else if (Shapes1 != null && Shapes1.activeSelf)
        {
            goto_result();

        }
        checkPopup.SetActive(false);

    }

    void ClearAllLines()
    {
        DrawLine.ClearAllLines();
    }

    private void goto_result() // ���â�� '�ϼ��̾�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        // ���� ���� -> ��� ȭ�鿡�� ����Ŭ����/���� ���� ���ؼ�
        gameResult.score = int.Parse(ScoreText.text);;

        // ���� �� �̸� ���� : ��� â���� �ش� �������� ���ƿ��� ���ؼ� 
        gameResult.previousScene = SceneManager.GetActiveScene().name;

        // ��� ȭ������ �Ѿ�� 
        //StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
        SceneManager.LoadScene("ResultScene");
    }

    // �ڷ�ƾ: 1�� �Ŀ� ���� �ε��ϴ� �Լ�
    //IEnumerator ResultSceneDelay()
    //{
    //    Debug.Log("Waiting for 1 second before loading ResultScene");

    //    // 1�� ���
    //    yield return new WaitForSeconds(1);

    //    Debug.Log("Loading ResultScene");

    //    // ��� �� �ε�
    //    SceneManager.LoadScene("ResultScene");
    //}

}

