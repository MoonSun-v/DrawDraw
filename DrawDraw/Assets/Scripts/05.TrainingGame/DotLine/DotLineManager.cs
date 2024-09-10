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

    private int dotscore_Final;           // ���� ����

    public GameObject CheckPopup;         // Ȯ�� �˾� â
    public Text ScoreText;                // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ

    public GameResultSO gameResult;

    // [ �ر׸� ���� ���� ]
    public GameObject[] DotPrefabs;    // �ر׸� �迭 
    private int currentDotIndex = 0;   // ���� �ر׸��� �ε���

    public Draw draw;



    // [ �ر׸� �ʱ� ���� ]
    // �ر׸��� ������ �°� �ʱ�ȭ
    // ù ��° �ر׸��� Ȱ��ȭ, �������� ��Ȱ��ȭ
    // 
    void Awake()
    {
        mainCamera = Camera.main;

        for (int i = 1; i < DotPrefabs.Length; i++)
        {
            DotPrefabs[i].SetActive(false); 
        }
    }


    // [ ���콺 ������ �ִ� ���� ]
    //
    // 1. ���콺 Ŭ�� ��ġ �� -> ���� ��ǥ ��ġ �� ��ȯ
    // 2. �浹�� �ִٸ� -> �ش� ������Ʈ �ݸ��� ��Ȱ��ȭ (�浹 1���� �߻���Ű�� ����)
    // 
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition);            

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                      
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            
                dotscore.DotCount += 1f;
            }
        }
    }


    // -------------------------------------------------------------------------------------------
    //   ��ư Ŭ�� ���� �޼ҵ� ����   ------------------------------------------------------------


    // [ �Ϸ� Ȯ�� �˾� ]
    public void CheckPopUp()          
    {
        CheckPopup.SetActive(true);
    }

    // [ �˾� : �����̾� ]
    public void PreviousBtn()          
    {
        CheckPopup.SetActive(false);   
    }


    // [ �˾� : �ϼ��̾� ] 
    //
    // 1. ������ �ر׸��� �ƴ� ��  :  ���� ��� -> ȭ�� �ʱ�ȭ �۾� (�׷ȴ� �� ���ֱ�, ���� �ʱ�ȭ)
    //                                -> ���� �������� �Ѿ��
    // 2. ������ �ر׸��� ��       :  ���� ��� -> 2�� or 3���� ��� ���� ��� -> gameResult (����, ���̸� ����)
    //                                -> ��� ȭ������ �̵�
    // 
    public void NextBtn()
    {
        // �� ������ �ر׸��� �ƴ� �� 
        // 
        if (currentDotIndex < DotPrefabs.Length - 1)  
        {
            int dotScore = (int)((dotscore.DotCount / 30) * 100);  
            print("�浹�� ���� ���� = " + dotscore.DotCount + " . �ۼ�Ʈ = " + dotScore + "%");

            dotscore.Score += dotScore;
            ScoreText.text = dotScore + "��";

            draw.ClearAllLineRenderers();
            dotscore.DotCount = 0;

            StartCoroutine(NextGameDelay());
        }

        // �� ������ �ر׸��� ��
        else  
        {
            int dotScore = (int)((dotscore.DotCount / 30) * 100);
            print("�浹�� ���� ���� = " + dotscore.DotCount + " . �ۼ�Ʈ = " + dotScore + "%");

            dotscore.Score += dotScore;
            ScoreText.text = dotScore + "��";

            // ------

            dotscore_Final = dotscore.Score / DotPrefabs.Length;  
            print(DotPrefabs.Length + "�� ������ ��� ���� = " + dotscore_Final);

            gameResult.score = dotscore_Final;
            gameResult.previousScene = SceneManager.GetActiveScene().name;

            StartCoroutine(ResultSceneDelay());  
        }
    }



    // ---------------------------------------------------------------------------------------------------
    //   �ڷ�ƾ ���� �Լ� ����   -------------------------------------------------------------------------
    //


    // [ ���� �ر׸����� ���� ]
    // ���� �ر׸� ��Ȱ��ȭ -> ���� �ر׸� Ȱ��ȭ
    //
    IEnumerator NextGameDelay()
    {
        yield return new WaitForSeconds(2);  

        DotPrefabs[currentDotIndex].SetActive(false); 
        currentDotIndex++; 
        DotPrefabs[currentDotIndex].SetActive(true);  

        CheckPopup.SetActive(false);
        ScoreText.text = "";
    }

    IEnumerator ResultSceneDelay()
    {
        yield return new WaitForSeconds(2);   

        SceneManager.LoadScene("ResultScene");
    }
}


public class DotScore
{
    // ���� �� ���� : 30��

    public float DotCount;  // �浹�� ���� ����
    
    public int Score;       // �� ����
}