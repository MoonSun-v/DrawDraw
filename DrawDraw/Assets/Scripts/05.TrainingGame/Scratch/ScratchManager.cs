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

    public SpriteRenderer spriteRenderer;   // 스프라이트 렌더러

    public GameObject ScratchBlack;         // 검은도안 


    // RaycastTarget 비활성화 버튼
    public GameObject ReturnButton;        // 처음부터
    public GameObject EraserButton;        // 지우개 
    public GameObject Crayon;              // 크레용 부모 

    public GameObject Blocker;             // 팝업 활성화 시, 다른 오브젝트 터치 막아주기 
    public GameObject CheckPopup;          // 팝업 
    public GameObject SelectDraw;          // 도안 선택 팝업 

    public GameObject[] ImageButton = new GameObject[4];    // 버튼 4개 

    public Text ScoreText;                 // 임시 점수 표시용 텍스트
    
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
        
        isReturn = scratchdraw.lineRenderers.Count > 0;                    // 리스트에 요소가 있는가?

        // 리스트에 요소가 없으면
        if (!isReturn)
        {
            ReturnButton.GetComponent<Image>().raycastTarget = false;      // '처음부터' 버튼 RaycastTarget 비활성화


            if (ReturnButton.transform.childCount == 1)                    // 자식 텍스트 오브젝트가 있는지 확인하고 자식의 raycastTarget 비활성화
            {
                var childText = ReturnButton.transform.GetChild(0).GetComponent<Text>();
                if (childText != null)
                {
                    childText.raycastTarget = false;
                }
            }

            // GUI 완성 시, 시각화도 시켜주기 

        }
        else
        {
            ReturnButton.GetComponent<Image>().raycastTarget = true;      // '처음부터' 버튼 RaycastTarget 활성화
        }



        // 검은색 도안이 활성화 되었다면 
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

            EraserButton.GetComponent<Image>().raycastTarget = true;    // 지우개 RaycastTarget 활성화

            // GUI 완성 시, 시각화도 시켜주기

        }
        // 팝업 활성화 되어있으면
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




        // 그리기 영역 제한 -----------------------------------------------------------------------------------------------------------------------


        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 Draw 비활성화 
        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x || mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if(scratchdraw.iscurrentLineRenderer())
            {
                scratchdraw.FinishLineRenderer();    // 현재 그리는 선 종료
            }

            scratchdraw.enabled = false;
        }
        else
        {
            scratchdraw.enabled = true;
        }

    }



    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        OnBlocker();
    }


    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        // 팝업 비활성화
        CheckPopup.SetActive(false);
        OffBlocker();
    }


    // 팝업 : 완성이야 
    public void NextBtn()
    {

        // [ 스크래치 완료 -> 점수 화면 이동 ]
        if(ScratchBlack.activeSelf)
        {
            
            float percentage = scratchblack.CheckGrayPercentage();    // 점수 계산
            if (percentage == -1f)
            {
                print("에러입니다~");
            }
            else
            {
                print("회색 부분이 투명해진 퍼센트: " + percentage + "%");

                // 프로토타입 : 임시로 점수 보여주기
                int Score = (int)percentage;
                if(Score<100)
                {
                    Score += 1;
                }
                ScoreText.text = Score + "점";

                gameResult.score = Score;                                         // 점수 저장 
                gameResult.previousScene = SceneManager.GetActiveScene().name;    // 현재 씬 이름 저장

                StartCoroutine(ResultSceneDelay());             // 결과 화면으로 넘어가기 
            }

        }

        // [ 밑그림 색칠 완료 -> 스크래치 시작 ]
        else
        {
            // 크레용 RaycastTarget 비활성화 및 안쪽으로 넣기


            // 부모 오브젝트의 자식 오브젝트들을 가져옴
            Transform[] children = Crayon.GetComponentsInChildren<Transform>();

            // 모든 자식 오브젝트들의 RaycastTarget 비활성화
            foreach (Transform child in children)
            {
                if (child != Crayon.transform)                      // 자기 자신 제외
                {
                    Image image = child.GetComponent<Image>();
                    if (image != null)
                    {
                        image.raycastTarget = false;
                    }


                    // 크레용 집어넣기 : 이전 버튼이 있다면 원래 위치로 되돌리기고 애니메이션 실행 
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
                    print("크레용 들어갑니다.");
                    */
                }
            }

            // 지우개 내보내기
            EraserAnim.SetBool("isEraserFront", true);
            /*
            Transform EraserTransform = EraserButton.GetComponent<Transform>();
            Vector3 EraserPosition = EraserTransform.position;
            EraserPosition.x = 1950;
            EraserTransform.position = EraserPosition;
            //Vector3 EraserPosition = EraserTransform.position + new Vector3(-120, 0f, 0f);
            //EraserTransform.position = EraserPosition;

            print("지우개 나옵니다.");
            */

            
            CheckPopup.SetActive(false);         // 팝업 비활성화

            //OnBlocker();

            BlackBase.SetActive(true);           // 검정색으로 덮는 애니메이션 : 추후 다시 비활성화 필요 
            BlackAnim.SetActive(true); 

            BaseAnim.SetBool("isBlackBase",true);

            
            StartCoroutine(SelectDrawDelay());   // 랜덤 도안 4개 띄우기 : 애니메이션 재생 후 띄워야 하므로 코루틴 적용

        }
        
    }


    IEnumerator SelectDrawDelay()
    {
        yield return new WaitForSeconds(5);   // 5 초 후 실행

        SelectDraw.SetActive(true);          // 랜덤 도안 띄우기

    }


    IEnumerator ResultSceneDelay()
    {
        // 2 초 후 실행
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }


    // 도안 선택 시, 알맞은 도안 띄워주기
    public void SelectDrawing(int number)
    {
        if(number == 0)
        {
            // 버튼의 스프라이트를 오브젝트의 스프라이트로 변경

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
        print("Blocker 활성화!");
    }

    private void OffBlocker()
    {
        Blocker.SetActive(false);
        print("Blocker 비활성화");
    }
}
