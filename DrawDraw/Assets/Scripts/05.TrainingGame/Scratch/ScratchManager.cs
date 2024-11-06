using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    // [ �ִϸ����� ���� ]
    public GameObject BlackBase;
    public GameObject BlackAnim;
    public GameObject BlackLine;
    private Animator BaseAnim;
    private Animator CrayonAnim;
    private Animator EraserAnim;

    // [ �˾� ������Ʈ ]
    public GameObject CheckPopup;          // �Ϸ� Ȯ�� �˾� 
    public GameObject SelectDraw;          // ���� ���� �˾� 

    // [ RaycastTarget ��Ȱ��ȭ ���� ����]
    public GameObject ReturnButton;        // ó������ ��ư
    public GameObject EraserButton;        // ���찳 ��ư
    public GameObject Crayon;              // ũ���� �θ� ������Ʈ

    public GameObject Blocker;             // �˾� Ȱ��ȭ ��, �ٸ� ������Ʈ ��ġ �����ֱ�

    // [ �׸��� ���� ������ ���� �Ѱ� ��ġ ]
    public Transform Limit_l;
    public Transform Limit_R;
    public Transform Limit_T;
    public Transform Limit_B;

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public GameObject ScratchDraw;
    public GameObject ScratchBlack;         // �������� 

    // [ ���� ���� ��ư ]
    public GameObject[] ImageButton = new GameObject[4];  

    // [ ��Ÿ ���� ]
    public GameResultSO gameResult;         // ���� ��� ����� SO
    public SpriteRenderer spriteRenderer;   // ��������Ʈ ������
    public Text ScoreText;                  // �ӽ� ���� ǥ�ÿ� �ؽ�Ʈ

    private bool isReturn;
    private bool isEraser;



    // ---------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------


    // [ ī�޶� �ʱ�ȭ ]
    void Awake()
    {
        mainCamera = Camera.main;
    }


    // [ �ִϸ����� �ʱ�ȭ ]
    void Start()
    {
        BaseAnim = BlackBase.GetComponent<Animator>();
        CrayonAnim = Crayon.GetComponent<Animator>();
        EraserAnim = EraserButton.GetComponent<Animator>();
    }



    void Update()
    {
        ReturnButtonState();  // 'ó������' ��ư            ���� ó��
        ScratchDrawState();   // ScratchDraw�� ScratchBlack ���� ó��
        // DrawingAreaLimits();  // �׸��� ���� ����   
    }



    // �� [ ������ �׷��� �ִ��� Ȯ���ϰ�, ������ 'ó������'��ư�� Raycast�� ��Ȱ��ȭ ]
    // 
    private void ReturnButtonState()
    {
        isReturn = scratchdraw.lineRenderers.Count > 0;

        if (!isReturn) { SetButtonRaycastState(ReturnButton, false); }
        else           { SetButtonRaycastState(ReturnButton, true);  }

    }



    // �� [ ScratchDraw�� ScratchBlack ������Ʈ�� Ȱ��ȭ ���� ]
    // 
    private void ScratchDrawState()
    {
        if (ScratchBlack.activeSelf)
        {
            ScratchDraw.SetActive(false);

            scratchblack.enabled = !CheckPopup.activeSelf;   
            SetButtonRaycastState(EraserButton, true);       
        }
        else if (CheckPopup.activeSelf)
        {
            ScratchDraw.SetActive(false);
        }
        else
        {
            ScratchDraw.SetActive(true);
            scratchblack.enabled = false;
            SetButtonRaycastState(EraserButton, false);       
        }
    }



    // �� [ �׸��� ���� ���� ]
    // 
    /*
    private void DrawingAreaLimits()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x ||
            mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if (scratchdraw.iscurrentLineRenderer())
            {
                scratchdraw.FinishLineRenderer();  // ���� �׸��� �� ����
            }
            scratchdraw.enabled = false;           
        }
        else
        {
            scratchdraw.enabled = true;           
        }
    }
    */


    // �� [ �Ϸ� Ȯ�� �˾� ]
    public void CheckPopUp() { CheckPopup.SetActive(true);  OnBlocker(); }
    

    // �� [ �˾�: '�����̾�' ]
    public void PreviousBtn() { CheckPopup.SetActive(false); OffBlocker(); }


    // �� [ �˾�: '�ϼ��̾�' ]
    //
    // 2. ��ũ��ġ �Ϸ� -> ���� ȭ�� �̵�
    // 1. �ر׸� ��ĥ �Ϸ� -> ��ũ��ġ ���� 
    //
    public void NextBtn()
    {
        if (ScratchBlack.activeSelf)     // 2
        {
            float percentage = scratchblack.CheckGrayPercentage();
            if (percentage == -1f)
            {
                Debug.LogError("���� ��� �� ���� �߻�");
            }
            else
            {
                DisplayScoreAndProceed((int)percentage);
            }
        }
        else                             // 1
        {
            StartScratchPhase();
        }
    }



    // �� [ ��� ȭ������ ��ȯ ]
    //
    // ( ��� ȭ�� ��ȯ �� �ʿ��� ������ )
    // - score         : ��ũ��ġ �Ϸ�� ���� ����
    // - previousScene : ���� �� �̸� 
    //
    private void DisplayScoreAndProceed(int score)
    {
        if (score < 100) {  score += 1; } // �ִ� ������ 100��
        ScoreText.text = $"{score} ��";

        gameResult.score = score;
        gameResult.previousScene = SceneManager.GetActiveScene().name; 

        // StartCoroutine(ResultSceneDelay());
        ResultSceneDelay_();
    }



    // �� [ ��ũ��ġ �ܰ� ���� ] 
    //
    // 1. ũ���� ����� -> ���찳 ǥ��
    // 2. ���������� ���� �ִϸ��̼� ����
    // 
    private void StartScratchPhase()
    {
        SetRaycastForCrayonChildren(false);
        CrayonAnim.SetBool("isCrayonBack", true);
        EraserButton.SetActive(true);
        EraserAnim.SetBool("isEraserFront", true); 

        CheckPopup.SetActive(false); 
        BlackBase.SetActive(true);
        BlackAnim.SetActive(true);
        BaseAnim.SetBool("isBlackBase", true); 

        StartCoroutine(SelectDrawDelay());
        // SelectDrawDelay_();
    }



    // �� [ RaycastTarget ���� ���� ]
    //
    // - button : RaycastTarget ���¸� ������ ��ư ������Ʈ
    // - state  : RaycastTarget�� Ȱ��ȭ ����
    // 
    private void SetButtonRaycastState(GameObject button, bool state)
    {
        var image = button.GetComponent<Image>();
        if (image != null)
        {
            image.raycastTarget = state;
        }

        if (button.transform.childCount == 1)
        {
            var childText = button.transform.GetChild(0).GetComponent<Text>();
            if (childText != null)
            {
                childText.raycastTarget = state;
            }
        }
    }



    // �� [ Crayon ��� �ڽ��� RaycastTarget�� ��Ȱ��ȭ ]
    // 
    // - state : RaycastTarget�� Ȱ��ȭ ����
    // 
    private void SetRaycastForCrayonChildren(bool state)
    {
        Transform[] children = Crayon.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child != Crayon.transform)
            {
                var image = child.GetComponent<Image>();
                if (image != null)
                {
                    image.raycastTarget = state;
                }

                if (!state && scratchdraw.previousButton != null)
                {
                    RectTransform prevRt = scratchdraw.previousButton.GetComponent<RectTransform>();
                    prevRt.localPosition = scratchdraw.previousButtonOriginalPosition; // ���� ��ư�� ��ġ ����
                }
            }
        }
    }


    
    // �� 5�� �� ���� ���� �˾� Ȱ��ȭ
    //
    IEnumerator SelectDrawDelay()
    {
        yield return new WaitForSeconds(5);
        SelectDraw.SetActive(true);
        Crayon.SetActive(false);
    }

    /*
    // �� 1�� �� ��� ȭ������ ��ȯ
    //
    IEnumerator ResultSceneDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResultScene");
    }
    */

    void ResultSceneDelay_()
    {
        SceneManager.LoadScene("ResultScene");
    }


    // �� [ ���õ� ���� ���� ]
    //
    // - number : ������ ���� ��ȣ
    //
    public void SelectDrawing(int number)
    {
        if (number >= 0 && number < ImageButton.Length)
        {
            spriteRenderer.sprite = ImageButton[number].GetComponent<Image>().sprite;
        }
        else
        {
            Debug.LogError("�߸��� ���� ��ȣ ����");
        }

        SelectDraw.SetActive(false);
        BlackBase.SetActive(false);
        BlackAnim.SetActive(false);
        BlackLine.SetActive(false);
        ScratchBlack.SetActive(true);
        OffBlocker();
    }



    // �� ���Ŀ Ȱ��ȭ
    private void OnBlocker() { Blocker.SetActive(true);  Debug.Log("Blocker Ȱ��ȭ"); }


    // �� ���Ŀ ��Ȱ��ȭ 
    private void OffBlocker() {  Blocker.SetActive(false);  Debug.Log("Blocker ��Ȱ��ȭ"); }


}
