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

    private int dotscore_Circle;          // 원모양 점수
    private int dotscore_Square;          // 사각형 모양 점수 

    private int dotscore_Final;           // 최종 점수

    public GameObject CheckPopup;         // 확인 팝업 창
    public Text ScoreText;                // 임시 점수 표시용 텍스트

    public GameResultSO gameResult;

    public GameObject Dot1;               // 원 모양 밑그림 
    public GameObject Dot2;               // 사각형 모양 밑그림 

    public Draw draw;


    private void Awake()
    {
        mainCamera = Camera.main;    // "Maincamera" 태그를 가지고 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
    }


    void Update()
    {
        // 마우스 누르고 있는 동안 
        if (Input.GetMouseButton(0)) 
        {
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition);             // 마우스 클릭 위치 값 -> 월드 좌표 위치 값 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                       // 충돌이 있다면 
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            // 해당 오브젝트 콜리더 비활성화 (충돌 1번만 발생시키기 위해)

                dotscore.DotCount += 1f;
                
            }

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
        CheckPopup.SetActive(false);   // 팝업 비활성화

    }


    // 팝업 : 완성이야 
    public void NextBtn()
    {

        // 원모양이 끝난거면 사각형 활성화 시켜주기 
        if(Dot1.activeSelf)
        {
            print("충돌한 점의 개수 = " + dotscore.DotCount);
            dotscore_Circle = (int)((dotscore.DotCount / 30) * 100);         // 소수점 이하는 내림 
            print("퍼센트 = " + dotscore_Circle + "%");


            // 프로토타입용 : 임시로 점수 보여주기 (추구 제거 예정)
            dotscore.Score += dotscore_Circle;
            ScoreText.text = dotscore_Circle + "점";


            draw.ClearAllLineRenderers();                                   // 그렸던 선 없애기

            dotscore.DotCount = 0;                                          // 초기화 

            StartCoroutine(NextGameDelay());                                // 다음 도안으로 넘어가기 


        }
        else
        {
            print("충돌한 점의 개수 = " + dotscore.DotCount);
            dotscore_Square = (int)((dotscore.DotCount / 30) * 100);           // 소수점 이하는 내림 
            print("퍼센트 = " + dotscore_Square + "%");


            // 프로토타입용 : 임시로 점수 보여주기 (추구 제거 예정)
            dotscore.Score += dotscore_Square;
            ScoreText.text = dotscore_Square + "점";


            dotscore_Final = (int)((dotscore_Square + dotscore_Circle) / 2 );  // 2개의 게임의 평균 점수 
            print("2개 게임의 평균 점수 = " + dotscore_Final);


            gameResult.score = dotscore_Final;                                 // 점수 저장 
            gameResult.previousScene = SceneManager.GetActiveScene().name;     // 현재 씬 이름 저장


            StartCoroutine(ResultSceneDelay());                                // 결과 화면으로 넘어가기
        }
    }



    IEnumerator NextGameDelay()
    {
        
        yield return new WaitForSeconds(2);    // 2 초 후 실행

        Dot1.SetActive(false);
        Dot2.SetActive(true);

        CheckPopup.SetActive(false);          // 팝업 비활성화

        ScoreText.text = "";
    }


    IEnumerator ResultSceneDelay()
    {
        yield return new WaitForSeconds(2);    // 2 초 후 실행

        SceneManager.LoadScene("ResultScene");
    }
}


public class DotScore
{
    // 점의 총 개수 : 30개

    public float DotCount;  // 충돌한 점의 개수
    
    public int Score;       // 총 점수
}