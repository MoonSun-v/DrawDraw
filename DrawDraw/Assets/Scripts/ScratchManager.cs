using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject BlackBase;
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
        if(ScratchBlack.activeSelf)
        {
            // ���� ���
            scratchblack.CheckGrayPercentage();

            // ��� ȭ������ �Ѿ�� 
        }
        else
        {
            // �˾� ��Ȱ��ȭ
            CheckPopup.SetActive(false);

            // ���������� ���� �ִϸ��̼�
            OnBlocker();

            BlackBase.SetActive(true); // ���� �ٽ� ��Ȱ��ȭ �ʿ� 
            BaseAnim.SetBool("isBlackBase",true);

            // ���� ���� 4�� ���� : �ִϸ��̼� ��� �� ����� �ϹǷ� �ڷ�ƾ ����
            StartCoroutine(SelectDrawDelay());


            // ������ ������ ���� Ȱ��ȭ


            // ScratchBlack.SetActive(true); // ���� �ڵ� 
        }
        
    }

    IEnumerator SelectDrawDelay()
    {
        // 5 �� �� ����
        yield return new WaitForSeconds(5);

        // ���� ���� ����
        SelectDraw.SetActive(true);

        // ���� ��ư ���� ��, ���õ� ���� Ȱ��ȭ : �ڵ� �̵� �ʿ� !!
        ScratchBlack.SetActive(true);
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
