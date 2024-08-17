using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DotLineManager : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 MousePosition;

    DotScore dotscore = new DotScore();

    private int dotscore_Circle;          // ����� ����
    private int dotscore_Square;          // �簢�� ��� ���� 

    private int dotscore_Final;           // ���� ����

    public GameObject CheckPopup;         // Ȯ�� �˾� â
    public Text ScoreText;                // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ

    public GameResultSO gameResult;

    public GameObject Dot1;               // �� ��� �ر׸� 
    public GameObject Dot2;               // �簢�� ��� �ر׸� 

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
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition);             // ���콺 Ŭ�� ��ġ �� -> ���� ��ǥ ��ġ �� 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                       // �浹�� �ִٸ� 
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            // �ش� ������Ʈ �ݸ��� ��Ȱ��ȭ (�浹 1���� �߻���Ű�� ����)

                dotscore.DotCount += 1f;
                
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
        CheckPopup.SetActive(false);   // �˾� ��Ȱ��ȭ

    }


    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {

        // ������� �����Ÿ� �簢�� Ȱ��ȭ �����ֱ� 
        if(Dot1.activeSelf)
        {
            print("�浹�� ���� ���� = " + dotscore.DotCount);
            dotscore_Circle = (int)((dotscore.DotCount / 30) * 100);         // �Ҽ��� ���ϴ� ���� 
            print("�ۼ�Ʈ = " + dotscore_Circle + "%");


            // ������Ÿ�Կ� : �ӽ÷� ���� �����ֱ� (�߱� ���� ����)
            dotscore.Score += dotscore_Circle;
            ScoreText.text = dotscore_Circle + "��";


            draw.ClearAllLineRenderers();                                   // �׷ȴ� �� ���ֱ�

            dotscore.DotCount = 0;                                          // �ʱ�ȭ 

            StartCoroutine(NextGameDelay());                                // ���� �������� �Ѿ�� 


        }
        else
        {
            print("�浹�� ���� ���� = " + dotscore.DotCount);
            dotscore_Square = (int)((dotscore.DotCount / 30) * 100);           // �Ҽ��� ���ϴ� ���� 
            print("�ۼ�Ʈ = " + dotscore_Square + "%");


            // ������Ÿ�Կ� : �ӽ÷� ���� �����ֱ� (�߱� ���� ����)
            dotscore.Score += dotscore_Square;
            ScoreText.text = dotscore_Square + "��";


            dotscore_Final = (int)((dotscore_Square + dotscore_Circle) / 2 );  // 2���� ������ ��� ���� 
            print("2�� ������ ��� ���� = " + dotscore_Final);


            gameResult.score = dotscore_Final;                                 // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name;     // ���� �� �̸� ����


            StartCoroutine(ResultSceneDelay());                                // ��� ȭ������ �Ѿ��
        }
    }



    IEnumerator NextGameDelay()
    {
        
        yield return new WaitForSeconds(2);    // 2 �� �� ����

        Dot1.SetActive(false);
        Dot2.SetActive(true);

        CheckPopup.SetActive(false);          // �˾� ��Ȱ��ȭ

        ScoreText.text = "";
    }


    IEnumerator ResultSceneDelay()
    {
        yield return new WaitForSeconds(2);    // 2 �� �� ����

        SceneManager.LoadScene("ResultScene");
    }
}


public class DotScore
{
    // ���� �� ���� : 30��

    public float DotCount;  // �浹�� ���� ����
    
    public int Score;       // �� ����
}