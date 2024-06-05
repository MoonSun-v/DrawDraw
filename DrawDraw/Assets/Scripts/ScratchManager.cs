using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    public Transform Limit_l;
    public Transform Limit_R;
    public Transform Limit_T;
    public Transform Limit_B;

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public GameObject ScratchDraw;

    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    public GameObject ScratchBlack; // 검은도안 

    // RaycastTarget 비활성화 버튼
    public GameObject ReturnButton; // 처음부터
    public GameObject EraserButton; // 지우개 

    public GameObject CheckPopup; // 팝업 

    private bool isReturn;
    private bool isEraser;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        
    }


    void Update()
    {
        // 리스트에 요소가 있는가?
        isReturn = scratchdraw.lineRenderers.Count > 0;
        
        // 리스트에 요소가 없으면
        if (!isReturn)
        {
            // '처음부터' 버튼 RaycastTarget 비활성화
            ReturnButton.GetComponent<Image>().raycastTarget = false;

            // 자식 텍스트 오브젝트가 있는지 확인하고 자식의 raycastTarget 비활성화
            if (ReturnButton.transform.childCount == 1)
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
            // '처음부터' 버튼 RaycastTarget 활성화
            ReturnButton.GetComponent<Image>().raycastTarget = true;
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

            // 지우개 RaycastTarget 활성화
            EraserButton.GetComponent<Image>().raycastTarget = true;

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
            
            EraserButton.GetComponent<Image>().raycastTarget = false;
        }


        // 그리기 영역 제한
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 Draw 비활성화 
        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x || mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if(scratchdraw.iscurrentLineRenderer())
            {
                scratchdraw.FinishLineRenderer(); // 현재 그리는 선 종료
            }

            scratchdraw.enabled = false;
        }
        else
        {
            scratchdraw.enabled = true;
        }

    }

    // 도안 4개 띄우고
    // 검은색 스크래치 시작 
    public void StartBlack()
    {
        if (!ScratchBlack.activeSelf)
        {

        }
    }
    
    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);

    }

    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        // 팝업 비활성화
        CheckPopup.SetActive(false);
    }

    // 팝업 : 완성이야 
    public void NextBtn()
    {
        if(ScratchBlack.activeSelf)
        {
            // 점수 계산
            scratchblack.CheckGrayPercentage();

            // 결과 화면으로 넘어가기 
        }
        else
        {
            // 팝업 비활성화
            CheckPopup.SetActive(false);

            // 검은색 도안 활성화
            ScratchBlack.SetActive(true);
        }
        
    }


}
