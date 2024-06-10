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

    public GameObject CheckPopup; // 확인 팝업 창
    public Text ScoreText; // 임시 점수 표시용 텍스트
    public GameResultSO gameResult;

    private void Awake()
    {
        // "Maincamera" 태그를 가지고 있는 오브젝트 담색 후 Camera 컴포넌트 정보 전달
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 누르고 있는 동안 
        if (Input.GetMouseButton(0)) 
        {
            // 마우스 클릭 위치 값 -> 월드 좌표 위치 값 
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition); 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit) // 충돌이 있다면 
            {
                // 해당 오브젝트 콜리더 비활성화 (충돌 1번만 발생시키기 위해)
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;

                dotscore.DotCount += 1f;
                // print("충돌한 점의 개수 = " + dotscore.DotCount);
            }

        }
    }

    // 완성 버튼 클릭 시, 총 점수 계산
    /*
    public void DotScore()
    {
        print("충돌한 점의 개수 = " + dotscore.DotCount);
        dotscore.Score = (int) (( dotscore.DotCount / 30 ) * 100) ; // 소수점 이하는 내림 
        print("퍼센트 = " + dotscore.Score + "%");

    }
    */
    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        // OnBlocker();
    }

    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        // 팝업 비활성화
        CheckPopup.SetActive(false);
        // OffBlocker();
    }

    // 팝업 : 완성이야 
    public void NextBtn()
    {
        print("충돌한 점의 개수 = " + dotscore.DotCount);
        dotscore.Score = (int)((dotscore.DotCount / 30) * 100); // 소수점 이하는 내림 
        print("퍼센트 = " + dotscore.Score + "%");

        // 프로토타입 : 임시로 점수 보여주기
        
        ScoreText.text = dotscore.Score + "점";

        gameResult.score = dotscore.Score; // 점수 저장 
        gameResult.previousScene = SceneManager.GetActiveScene().name; // 현재 씬 이름 저장


        // 결과 화면으로 넘어가기 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "메소드이름", 매개변수 );
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 초 후 실행
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }
}

public class DotScore
{
    // 점의 총 개수 : 30개

    // 충돌한 점의 개수
    public float DotCount;

    // 총 점수
    public int Score;
}