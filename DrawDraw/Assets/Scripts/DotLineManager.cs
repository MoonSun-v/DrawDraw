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

    private int dotscore_Circle; // ����� ����
    private int dotscore_Square; // �簢�� ��� ���� 

    private int dotscore_Final; // ���� ����

    public GameObject CheckPopup; // Ȯ�� �˾� â
    public Text ScoreText; // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ
    public GameResultSO gameResult;

    public GameObject Dot1; // �� ��� �ر׸� 
    public GameObject Dot2; // �簢�� ��� �ر׸� 

    public Draw draw;

    private void Awake()
    {
        // "Maincamera" �±׸� ������ �ִ� ������Ʈ ��� �� Camera ������Ʈ ���� ����
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ �ִ� ���� 
        if (Input.GetMouseButton(0)) 
        {
            // ���콺 Ŭ�� ��ġ �� -> ���� ��ǥ ��ġ �� 
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition); 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit) // �浹�� �ִٸ� 
            {
                // �ش� ������Ʈ �ݸ��� ��Ȱ��ȭ (�浹 1���� �߻���Ű�� ����)
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;

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
        // �˾� ��Ȱ��ȭ
        CheckPopup.SetActive(false);

    }

    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        // ������� �����Ÿ� �簢�� Ȱ��ȭ �����ֱ� 
        if(Dot1.activeSelf)
        {
            print("�浹�� ���� ���� = " + dotscore.DotCount);

            dotscore_Circle = (int)((dotscore.DotCount / 30) * 100); // �Ҽ��� ���ϴ� ���� 
            print("�ۼ�Ʈ = " + dotscore_Circle + "%");

            // ������Ÿ�� : �ӽ÷� ���� �����ֱ�

            dotscore.Score += dotscore_Circle;

            ScoreText.text = dotscore_Circle + "��";

            // �׷ȴ� �� ���ֱ�
            draw.ClearAllLineRenderers();

            dotscore.DotCount = 0; // �ʱ�ȭ 

            StartCoroutine(NextGameDelay()); // ���� �������� �Ѿ�� 


        }
        else
        {
            print("�浹�� ���� ���� = " + dotscore.DotCount);
            dotscore_Square = (int)((dotscore.DotCount / 30) * 100); // �Ҽ��� ���ϴ� ���� 
            print("�ۼ�Ʈ = " + dotscore_Square + "%");

            // ������Ÿ�� : �ӽ÷� ���� �����ֱ�
            dotscore.Score += dotscore_Square;
            ScoreText.text = dotscore_Square + "��";

            dotscore_Final = (int)((dotscore_Square + dotscore_Circle) / 2 ); // 2���� ������ ��� ���� 
            print("2�� ������ ��� ���� = " + dotscore_Final);

            gameResult.score = dotscore_Final; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����


            // ��� ȭ������ �Ѿ�� 
            StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
        }
        
    }

    IEnumerator NextGameDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        Dot1.SetActive(false);
        Dot2.SetActive(true);

        // �˾� ��Ȱ��ȭ
        CheckPopup.SetActive(false);

        ScoreText.text = "";
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }
}

public class DotScore
{
    // ���� �� ���� : 30��

    // �浹�� ���� ����
    public float DotCount;

    // �� ����
    public int Score;
}