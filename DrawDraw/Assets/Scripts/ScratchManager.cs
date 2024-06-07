using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject BlackBase;
    public GameObject BlackLine;
    private Animator BaseAnim;

    public Transform Limit_l;
    public Transform Limit_R;
    public Transform Limit_T;
    public Transform Limit_B;

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public GameObject ScratchDraw;

    public SpriteRenderer spriteRenderer; // ��������Ʈ ������

    public GameObject ScratchBlack; // �������� 

    // RaycastTarget ��Ȱ��ȭ ��ư
    public GameObject ReturnButton; // ó������
    public GameObject EraserButton; // ���찳 

    public GameObject Blocker; // �˾� Ȱ��ȭ ��, �ٸ� ������Ʈ ��ġ �����ֱ� 
    public GameObject CheckPopup; // �˾� 
    public GameObject SelectDraw; // ���� ���� �˾� 

    public GameObject[] ImageButton = new GameObject[4]; // ��ư 4�� 

    public Text ScoreText; // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ

    private bool isReturn;
    private bool isEraser;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        BaseAnim = BlackBase.GetComponent<Animator>();
    }


    void Update()
    {
        // ����Ʈ�� ��Ұ� �ִ°�?
        isReturn = scratchdraw.lineRenderers.Count > 0;
        
        // ����Ʈ�� ��Ұ� ������
        if (!isReturn)
        {
            // 'ó������' ��ư RaycastTarget ��Ȱ��ȭ
            ReturnButton.GetComponent<Image>().raycastTarget = false;

            // �ڽ� �ؽ�Ʈ ������Ʈ�� �ִ��� Ȯ���ϰ� �ڽ��� raycastTarget ��Ȱ��ȭ
            if (ReturnButton.transform.childCount == 1)
            {
                var childText = ReturnButton.transform.GetChild(0).GetComponent<Text>();
                if (childText != null)
                {
                    childText.raycastTarget = false;
                }
            }

            // GUI �ϼ� ��, �ð�ȭ�� �����ֱ� 

        }
        else
        {
            // 'ó������' ��ư RaycastTarget Ȱ��ȭ
            ReturnButton.GetComponent<Image>().raycastTarget = true;
        }


        // ������ ������ Ȱ��ȭ �Ǿ��ٸ�
        if(ScratchBlack.activeSelf)
        {
            ScratchDraw.SetActive(false);

            if (CheckPopup.activeSelf)
            {
                scratchblack.enabled = false;
            }
            else
            {
                scratchblack.enabled = true;
            }

            // ���찳 RaycastTarget Ȱ��ȭ
            EraserButton.GetComponent<Image>().raycastTarget = true;

            // GUI �ϼ� ��, �ð�ȭ�� �����ֱ�

        }
        // �˾� Ȱ��ȭ �Ǿ�������
        else if(CheckPopup.activeSelf)
        {
            ScratchDraw.SetActive(false);

        }
        else
        {
            ScratchDraw.SetActive(true);
            
            EraserButton.GetComponent<Image>().raycastTarget = false;
        }


        // �׸��� ���� ����
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // �Է� ���콺�� x, y ��ǥ�� ���� ������ ����� Draw ��Ȱ��ȭ 
        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x || mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if(scratchdraw.iscurrentLineRenderer())
            {
                scratchdraw.FinishLineRenderer(); // ���� �׸��� �� ����
            }

            scratchdraw.enabled = false;
        }
        else
        {
            scratchdraw.enabled = true;
        }

    }

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        OnBlocker();
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        // �˾� ��Ȱ��ȭ
        CheckPopup.SetActive(false);
        OffBlocker();
    }

    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        if(ScratchBlack.activeSelf)
        {
            // ��� ȭ������ �Ѿ�� 
            StartCoroutine(ResultSceneDelay());
        }
        else
        {
            // �˾� ��Ȱ��ȭ
            CheckPopup.SetActive(false);

            //OnBlocker();

            // ���������� ���� �ִϸ��̼�
            BlackBase.SetActive(true); 
            BlackLine.SetActive(true); // ���� �ٽ� ��Ȱ��ȭ �ʿ� 

            BaseAnim.SetBool("isBlackBase",true);

            // ���� ���� 4�� ���� : �ִϸ��̼� ��� �� ����� �ϹǷ� �ڷ�ƾ ����
            StartCoroutine(SelectDrawDelay());

        }
        
    }

    IEnumerator SelectDrawDelay()
    {
        // 5 �� �� ����
        yield return new WaitForSeconds(5);

        // ���� ���� ����
        SelectDraw.SetActive(true);

    }

    IEnumerator ResultSceneDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }

    // ���� ���� ��, �˸��� ���� ����ֱ�
    public void SelectDrawing(int number)
    {
        if(number == 0)
        {
            // ��ư�� ��������Ʈ�� ������Ʈ�� ��������Ʈ�� ����
            spriteRenderer.sprite = ImageButton[0].GetComponent<Image>().sprite;
        }
        else if(number == 1)
        {
            spriteRenderer.sprite = ImageButton[1].GetComponent<Image>().sprite;
        }
        else if (number == 2)
        {
            spriteRenderer.sprite = ImageButton[2].GetComponent<Image>().sprite;
        }
        else if (number == 3)
        {
            spriteRenderer.sprite = ImageButton[3].GetComponent<Image>().sprite;
        }
        else
        {
            print("Number Error!!");
        }

        SelectDraw.SetActive(false);
        BlackBase.SetActive(false);
        BlackLine.SetActive(false);

        ScratchBlack.SetActive(true);

        OffBlocker();

    }

    private void OnBlocker()
    {
        Blocker.SetActive(true);
        print("Blocker Ȱ��ȭ!");
    }

    private void OffBlocker()
    {
        Blocker.SetActive(false);
        print("Blocker ��Ȱ��ȭ");
    }
}
