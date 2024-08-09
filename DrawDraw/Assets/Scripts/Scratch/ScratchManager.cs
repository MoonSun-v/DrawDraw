using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject BlackBase;
    public GameObject BlackAnim;
    public GameObject BlackLine;
    private Animator BaseAnim;
    private Animator CrayonAnim;
    private Animator EraserAnim;

    public Transform Limit_l;
    public Transform Limit_R;
    public Transform Limit_T;
    public Transform Limit_B;

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public GameObject ScratchDraw;

    public SpriteRenderer spriteRenderer;   // ��������Ʈ ������

    public GameObject ScratchBlack;         // �������� 


    // RaycastTarget ��Ȱ��ȭ ��ư
    public GameObject ReturnButton;        // ó������
    public GameObject EraserButton;        // ���찳 
    public GameObject Crayon;              // ũ���� �θ� 

    public GameObject Blocker;             // �˾� Ȱ��ȭ ��, �ٸ� ������Ʈ ��ġ �����ֱ� 
    public GameObject CheckPopup;          // �˾� 
    public GameObject SelectDraw;          // ���� ���� �˾� 

    public GameObject[] ImageButton = new GameObject[4];    // ��ư 4�� 

    public Text ScoreText;                 // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ
    
    private bool isReturn;
    private bool isEraser;

    public GameResultSO gameResult;



    void Awake()
    {
        mainCamera = Camera.main;
    }


    void Start()
    {
        BaseAnim = BlackBase.GetComponent<Animator>();
        CrayonAnim = Crayon.GetComponent<Animator>();
        EraserAnim = EraserButton.GetComponent<Animator>();
    }



    void Update()
    {
        
        isReturn = scratchdraw.lineRenderers.Count > 0;                    // ����Ʈ�� ��Ұ� �ִ°�?

        // ����Ʈ�� ��Ұ� ������
        if (!isReturn)
        {
            ReturnButton.GetComponent<Image>().raycastTarget = false;      // 'ó������' ��ư RaycastTarget ��Ȱ��ȭ


            if (ReturnButton.transform.childCount == 1)                    // �ڽ� �ؽ�Ʈ ������Ʈ�� �ִ��� Ȯ���ϰ� �ڽ��� raycastTarget ��Ȱ��ȭ
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
            ReturnButton.GetComponent<Image>().raycastTarget = true;      // 'ó������' ��ư RaycastTarget Ȱ��ȭ
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

            EraserButton.GetComponent<Image>().raycastTarget = true;    // ���찳 RaycastTarget Ȱ��ȭ

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
            scratchblack.enabled = false;

            EraserButton.GetComponent<Image>().raycastTarget = false;
        }




        // �׸��� ���� ���� -----------------------------------------------------------------------------------------------------------------------


        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // �Է� ���콺�� x, y ��ǥ�� ���� ������ ����� Draw ��Ȱ��ȭ 
        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x || mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if(scratchdraw.iscurrentLineRenderer())
            {
                scratchdraw.FinishLineRenderer();    // ���� �׸��� �� ����
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

        // [ ��ũ��ġ �Ϸ� -> ���� ȭ�� �̵� ]
        if(ScratchBlack.activeSelf)
        {
            
            float percentage = scratchblack.CheckGrayPercentage();    // ���� ���
            if (percentage == -1f)
            {
                print("�����Դϴ�~");
            }
            else
            {
                print("ȸ�� �κ��� �������� �ۼ�Ʈ: " + percentage + "%");

                // ������Ÿ�� : �ӽ÷� ���� �����ֱ�
                int Score = (int)percentage;
                if(Score<100)
                {
                    Score += 1;
                }
                ScoreText.text = Score + "��";

                gameResult.score = Score;                                         // ���� ���� 
                gameResult.previousScene = SceneManager.GetActiveScene().name;    // ���� �� �̸� ����

                StartCoroutine(ResultSceneDelay());             // ��� ȭ������ �Ѿ�� 
            }

        }

        // [ �ر׸� ��ĥ �Ϸ� -> ��ũ��ġ ���� ]
        else
        {
            // ũ���� RaycastTarget ��Ȱ��ȭ �� �������� �ֱ�


            // �θ� ������Ʈ�� �ڽ� ������Ʈ���� ������
            Transform[] children = Crayon.GetComponentsInChildren<Transform>();

            // ��� �ڽ� ������Ʈ���� RaycastTarget ��Ȱ��ȭ
            foreach (Transform child in children)
            {
                if (child != Crayon.transform)                      // �ڱ� �ڽ� ����
                {
                    Image image = child.GetComponent<Image>();
                    if (image != null)
                    {
                        image.raycastTarget = false;
                    }


                    // ũ���� ����ֱ� : ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ������ �ִϸ��̼� ���� 
                    if (scratchdraw.previousButton != null)
                    {
                        RectTransform prevRt = scratchdraw.previousButton.GetComponent<RectTransform>();
                        prevRt.localPosition = scratchdraw.previousButtonOriginalPosition;
                    }
                    CrayonAnim.SetBool("isCrayonBack", true);

                    /*
                    Vector3 CrayonPosition = child.localPosition;
                    CrayonPosition.x = 1300;
                    child.localPosition = CrayonPosition;
                    print("ũ���� ���ϴ�.");
                    */
                }
            }

            // ���찳 ��������
            EraserAnim.SetBool("isEraserFront", true);
            /*
            Transform EraserTransform = EraserButton.GetComponent<Transform>();
            Vector3 EraserPosition = EraserTransform.position;
            EraserPosition.x = 1950;
            EraserTransform.position = EraserPosition;
            //Vector3 EraserPosition = EraserTransform.position + new Vector3(-120, 0f, 0f);
            //EraserTransform.position = EraserPosition;

            print("���찳 ���ɴϴ�.");
            */

            
            CheckPopup.SetActive(false);         // �˾� ��Ȱ��ȭ

            //OnBlocker();

            BlackBase.SetActive(true);           // ���������� ���� �ִϸ��̼� : ���� �ٽ� ��Ȱ��ȭ �ʿ� 
            BlackAnim.SetActive(true); 

            BaseAnim.SetBool("isBlackBase",true);

            
            StartCoroutine(SelectDrawDelay());   // ���� ���� 4�� ���� : �ִϸ��̼� ��� �� ����� �ϹǷ� �ڷ�ƾ ����

        }
        
    }


    IEnumerator SelectDrawDelay()
    {
        yield return new WaitForSeconds(5);   // 5 �� �� ����

        SelectDraw.SetActive(true);          // ���� ���� ����

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
        BlackAnim.SetActive(false);
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
