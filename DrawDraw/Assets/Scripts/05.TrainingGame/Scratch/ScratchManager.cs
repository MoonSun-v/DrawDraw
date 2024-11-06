using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    // [ 애니메이터 참조 ]
    public GameObject BlackBase;
    public GameObject BlackAnim;
    public GameObject BlackLine;
    private Animator BaseAnim;
    private Animator CrayonAnim;
    private Animator EraserAnim;

    // [ 팝업 오브젝트 ]
    public GameObject CheckPopup;          // 완료 확인 팝업 
    public GameObject SelectDraw;          // 도안 선택 팝업 

    // [ RaycastTarget 비활성화 위한 변수]
    public GameObject ReturnButton;        // 처음부터 버튼
    public GameObject EraserButton;        // 지우개 버튼
    public GameObject Crayon;              // 크레용 부모 오브젝트

    public GameObject Blocker;             // 팝업 활성화 시, 다른 오브젝트 터치 막아주기

    // [ 그리기 영역 제한을 위한 한계 위치 ]
    public Transform Limit_l;
    public Transform Limit_R;
    public Transform Limit_T;
    public Transform Limit_B;

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public GameObject ScratchDraw;
    public GameObject ScratchBlack;         // 검은도안 

    // [ 도안 선택 버튼 ]
    public GameObject[] ImageButton = new GameObject[4];  

    // [ 기타 참조 ]
    public GameResultSO gameResult;         // 게임 결과 저장용 SO
    public SpriteRenderer spriteRenderer;   // 스프라이트 렌더러
    public Text ScoreText;                  // 임시 점수 표시용 텍스트

    private bool isReturn;
    private bool isEraser;



    // ---------------------------------------------------------------------------------------------
    // ---------------------------------------------------------------------------------------------


    // [ 카메라 초기화 ]
    void Awake()
    {
        mainCamera = Camera.main;
    }


    // [ 애니메이터 초기화 ]
    void Start()
    {
        BaseAnim = BlackBase.GetComponent<Animator>();
        CrayonAnim = Crayon.GetComponent<Animator>();
        EraserAnim = EraserButton.GetComponent<Animator>();
    }



    void Update()
    {
        ReturnButtonState();  // '처음부터' 버튼            상태 처리
        ScratchDrawState();   // ScratchDraw와 ScratchBlack 상태 처리
        // DrawingAreaLimits();  // 그리기 영역 제한   
    }



    // ★ [ 라인이 그려져 있는지 확인하고, 없으면 '처음부터'버튼의 Raycast를 비활성화 ]
    // 
    private void ReturnButtonState()
    {
        isReturn = scratchdraw.lineRenderers.Count > 0;

        if (!isReturn) { SetButtonRaycastState(ReturnButton, false); }
        else           { SetButtonRaycastState(ReturnButton, true);  }

    }



    // ★ [ ScratchDraw와 ScratchBlack 오브젝트의 활성화 관리 ]
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



    // ★ [ 그리기 영역 제한 ]
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
                scratchdraw.FinishLineRenderer();  // 현재 그리는 선 종료
            }
            scratchdraw.enabled = false;           
        }
        else
        {
            scratchdraw.enabled = true;           
        }
    }
    */


    // ★ [ 완료 확인 팝업 ]
    public void CheckPopUp() { CheckPopup.SetActive(true);  OnBlocker(); }
    

    // ★ [ 팝업: '아직이야' ]
    public void PreviousBtn() { CheckPopup.SetActive(false); OffBlocker(); }


    // ★ [ 팝업: '완성이야' ]
    //
    // 2. 스크래치 완료 -> 점수 화면 이동
    // 1. 밑그림 색칠 완료 -> 스크래치 시작 
    //
    public void NextBtn()
    {
        if (ScratchBlack.activeSelf)     // 2
        {
            float percentage = scratchblack.CheckGrayPercentage();
            if (percentage == -1f)
            {
                Debug.LogError("점수 계산 중 오류 발생");
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



    // ★ [ 결과 화면으로 전환 ]
    //
    // ( 결과 화면 전환 시 필요한 데이터 )
    // - score         : 스크래치 완료로 계산된 점수
    // - previousScene : 현재 씬 이름 
    //
    private void DisplayScoreAndProceed(int score)
    {
        if (score < 100) {  score += 1; } // 최대 점수는 100점
        ScoreText.text = $"{score} 점";

        gameResult.score = score;
        gameResult.previousScene = SceneManager.GetActiveScene().name; 

        // StartCoroutine(ResultSceneDelay());
        ResultSceneDelay_();
    }



    // ★ [ 스크래치 단계 시작 ] 
    //
    // 1. 크레용 숨기고 -> 지우개 표시
    // 2. 검정색으로 덮는 애니메이션 시작
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



    // ★ [ RaycastTarget 상태 설정 ]
    //
    // - button : RaycastTarget 상태를 설정할 버튼 오브젝트
    // - state  : RaycastTarget의 활성화 여부
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



    // ★ [ Crayon 모든 자식의 RaycastTarget을 비활성화 ]
    // 
    // - state : RaycastTarget의 활성화 여부
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
                    prevRt.localPosition = scratchdraw.previousButtonOriginalPosition; // 이전 버튼의 위치 복원
                }
            }
        }
    }


    
    // ★ 5초 후 도안 선택 팝업 활성화
    //
    IEnumerator SelectDrawDelay()
    {
        yield return new WaitForSeconds(5);
        SelectDraw.SetActive(true);
        Crayon.SetActive(false);
    }

    /*
    // ★ 1초 후 결과 화면으로 전환
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


    // ★ [ 선택된 도안 적용 ]
    //
    // - number : 선택한 도안 번호
    //
    public void SelectDrawing(int number)
    {
        if (number >= 0 && number < ImageButton.Length)
        {
            spriteRenderer.sprite = ImageButton[number].GetComponent<Image>().sprite;
        }
        else
        {
            Debug.LogError("잘못된 도안 번호 선택");
        }

        SelectDraw.SetActive(false);
        BlackBase.SetActive(false);
        BlackAnim.SetActive(false);
        BlackLine.SetActive(false);
        ScratchBlack.SetActive(true);
        OffBlocker();
    }



    // ★ 블로커 활성화
    private void OnBlocker() { Blocker.SetActive(true);  Debug.Log("Blocker 활성화"); }


    // ★ 블로커 비활성화 
    private void OffBlocker() {  Blocker.SetActive(false);  Debug.Log("Blocker 비활성화"); }


}
