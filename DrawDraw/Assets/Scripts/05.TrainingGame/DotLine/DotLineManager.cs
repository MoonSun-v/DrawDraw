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

    private int dotscore_Final;           // 최종 점수

    public GameObject CheckPopup;         // 확인 팝업 창
    public Text ScoreText;                // 임시 점수 표시용 텍스트

    public GameResultSO gameResult;

    // [ 밑그림 관련 변수 ]
    public GameObject[] DotPrefabs;    // 밑그림 배열 
    private int currentDotIndex = 0;   // 현재 밑그림의 인덱스

    public Draw draw;



    // [ 밑그림 초기 설정 ]
    // 밑그림의 개수에 맞게 초기화
    // 첫 번째 밑그림만 활성화, 나머지는 비활성화
    // 
    void Awake()
    {
        mainCamera = Camera.main;

        for (int i = 1; i < DotPrefabs.Length; i++)
        {
            DotPrefabs[i].SetActive(false); 
        }
    }


    // [ 마우스 누르고 있는 동안 ]
    //
    // 1. 마우스 클릭 위치 값 -> 월드 좌표 위치 값 변환
    // 2. 충돌이 있다면 -> 해당 오브젝트 콜리더 비활성화 (충돌 1번만 발생시키기 위해)
    // 
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition);            

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                      
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            
                dotscore.DotCount += 1f;
            }
        }
    }


    // -------------------------------------------------------------------------------------------
    //   버튼 클릭 관련 메소드 모음   ------------------------------------------------------------


    // [ 완료 확인 팝업 ]
    public void CheckPopUp()          
    {
        CheckPopup.SetActive(true);
    }

    // [ 팝업 : 아직이야 ]
    public void PreviousBtn()          
    {
        CheckPopup.SetActive(false);   
    }


    // [ 팝업 : 완성이야 ] 
    //
    // 1. 마지막 밑그림이 아닐 때  :  점수 계산 -> 화면 초기화 작업 (그렸던 선 없애기, 점수 초기화)
    //                                -> 다음 도안으로 넘어가기
    // 2. 마지막 밑그림일 때       :  점수 계산 -> 2개 or 3개의 평균 점수 계산 -> gameResult (점수, 씬이름 저장)
    //                                -> 결과 화면으로 이동
    // 
    public void NextBtn()
    {
        // ★ 마지막 밑그림이 아닐 때 
        // 
        if (currentDotIndex < DotPrefabs.Length - 1)  
        {
            int dotScore = (int)((dotscore.DotCount / 30) * 100);  
            print("충돌한 점의 개수 = " + dotscore.DotCount + " . 퍼센트 = " + dotScore + "%");

            dotscore.Score += dotScore;
            ScoreText.text = dotScore + "점";

            draw.ClearAllLineRenderers();
            dotscore.DotCount = 0;

            StartCoroutine(NextGameDelay());
        }

        // ★ 마지막 밑그림일 때
        else  
        {
            int dotScore = (int)((dotscore.DotCount / 30) * 100);
            print("충돌한 점의 개수 = " + dotscore.DotCount + " . 퍼센트 = " + dotScore + "%");

            dotscore.Score += dotScore;
            ScoreText.text = dotScore + "점";

            // ------

            dotscore_Final = dotscore.Score / DotPrefabs.Length;  
            print(DotPrefabs.Length + "개 게임의 평균 점수 = " + dotscore_Final);

            gameResult.score = dotscore_Final;
            gameResult.previousScene = SceneManager.GetActiveScene().name;

            StartCoroutine(ResultSceneDelay());  
        }
    }



    // ---------------------------------------------------------------------------------------------------
    //   코루틴 관련 함수 모음   -------------------------------------------------------------------------
    //


    // [ 다음 밑그림으로 변경 ]
    // 현재 밑그림 비활성화 -> 다음 밑그림 활성화
    //
    IEnumerator NextGameDelay()
    {
        yield return new WaitForSeconds(2);  

        DotPrefabs[currentDotIndex].SetActive(false); 
        currentDotIndex++; 
        DotPrefabs[currentDotIndex].SetActive(true);  

        CheckPopup.SetActive(false);
        ScoreText.text = "";
    }

    IEnumerator ResultSceneDelay()
    {
        yield return new WaitForSeconds(2);   

        SceneManager.LoadScene("ResultScene");
    }
}


public class DotScore
{
    // 점의 총 개수 : 30개

    public float DotCount;  // 충돌한 점의 개수
    
    public int Score;       // 총 점수
}