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

    public GameObject CheckPopup; // Ȯ�� �˾� â
    public Text ScoreText; // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ
    public GameResultSO gameResult;

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
                // print("�浹�� ���� ���� = " + dotscore.DotCount);
            }

        }
    }

    // �ϼ� ��ư Ŭ�� ��, �� ���� ���
    /*
    public void DotScore()
    {
        print("�浹�� ���� ���� = " + dotscore.DotCount);
        dotscore.Score = (int) (( dotscore.DotCount / 30 ) * 100) ; // �Ҽ��� ���ϴ� ���� 
        print("�ۼ�Ʈ = " + dotscore.Score + "%");

    }
    */
    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        // OnBlocker();
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        // �˾� ��Ȱ��ȭ
        CheckPopup.SetActive(false);
        // OffBlocker();
    }

    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        print("�浹�� ���� ���� = " + dotscore.DotCount);
        dotscore.Score = (int)((dotscore.DotCount / 30) * 100); // �Ҽ��� ���ϴ� ���� 
        print("�ۼ�Ʈ = " + dotscore.Score + "%");

        // ������Ÿ�� : �ӽ÷� ���� �����ֱ�
        
        ScoreText.text = dotscore.Score + "��";

        gameResult.score = dotscore.Score; // ���� ���� 
        gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����


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

public class DotScore
{
    // ���� �� ���� : 30��

    // �浹�� ���� ����
    public float DotCount;

    // �� ����
    public int Score;
}