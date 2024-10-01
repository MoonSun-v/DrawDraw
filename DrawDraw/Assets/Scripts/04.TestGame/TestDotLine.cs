using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestDotLine : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 MousePosition;

    // ���� ���� : 30
    private int TotalDot = 30;

    private float DotCount;          // �浹�� ���� ����
    private int Score;               // �� ����

    public GameObject CheckPopup;    // Ȯ�� �˾� â

    public Draw draw;


    private void Awake()
    {
        mainCamera = Camera.main;    // "Maincamera" �±׸� ������ �ִ� ������Ʈ Ž�� �� Camera ������Ʈ ���� ����
    }


    void Update()
    {
        // ���콺 ������ �ִ� ���� 
        if (Input.GetMouseButton(0))
        {
            MousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);             // ���콺 Ŭ�� ��ġ �� -> ���� ��ǥ ��ġ �� 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                       // �浹�� �ִٸ� 
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            // �ش� ������Ʈ �ݸ��� ��Ȱ��ȭ (�浹 1���� �߻���Ű�� ����)

                DotCount += 1;

            }

        }
    }


    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }


    // �˾� : �����̾�
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }


    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        print("�浹�� ���� ���� = " + DotCount);
        Score = (int)((DotCount / TotalDot) * 100);         // �Ҽ��� ���ϴ� ���� 
        print("�ۼ�Ʈ = " + Score + "%");

        // StartCoroutine(NextGameDelay());                 
        NextGame();
    }

    void NextGame()
    {
        SceneManager.LoadScene("Test_ShapesClassifyScene");
    }

    /*
    IEnumerator NextGameDelay()
    {

        yield return new WaitForSeconds(2);    // 2 �� �� ����

        print("���� ���� �׸��� �׽�Ʈ �Ϸ�. ���� �׽�Ʈ�� �̵� �ʿ��մϴ� ");
        SceneManager.LoadScene("Test_ShapesClassifyScene");

    }
    */

}
