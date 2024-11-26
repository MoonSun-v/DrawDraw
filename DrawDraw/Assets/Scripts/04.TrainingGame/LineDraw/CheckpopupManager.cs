using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.ShaderData;

public class CheckpopupManager : MonoBehaviour
{
    public GameObject checkPopup;
    public DrawLine DrawLine;

    public GameObject line1;
    public GameObject line2;
    public GameObject line3;

    public GameObject curveline1;
    public GameObject curveline2;
    public GameObject curveline3;

    public GameObject Shapes1;
    public GameObject Shapes2;
    public GameObject Shapes3;

    public Text ScoreText; // 게임의 결과를 가져올 Text Ui

    public GameResultSO gameResult; // 게임 결과화면 관리 SO

    [SerializeField] public CollisionCounter collisionCounter;


    public void OnClick_close() // '창 닫기' 버튼을 클릭하며 호출 되어질 함수
    {
        checkPopup.transform.gameObject.SetActive(false);
    }
    public void ShowCheckPopup()
    {
        checkPopup.SetActive(true); // 확인 팝업 창을 화면에 표시
    }

    public void OnClick_finish() // 확인창 완성 버튼을 클릭 -> 결과 보여주기
    {
        if (line1.activeSelf && curveline1 != null) // 첫 번째 그림 완성 -> 두 번째 그림 시작
        {
            // 직선 비활성화
            line1.SetActive(false);
            line2.SetActive(false);
            line3.SetActive(false);

            if (curveline1 != null)
            {
                //곡선 활성화
                curveline1.SetActive(true);
                curveline2.SetActive(true);
                curveline3.SetActive(true);
            }
            //그려진 선 모두 지우기
            ClearAllLines();

        }
        else if (curveline1 == null) // 첫번째 그림만 있을 경우
        {
            goto_result();
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 == null) // 밑그림 2개
        {
            goto_result();
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 != null) // 밑그림 3개
        {
            // 직선 비활성화
            curveline1.SetActive(false);
            curveline2.SetActive(false);
            curveline3.SetActive(false);

            //곡선 활성화
            Shapes1.SetActive(true);
            Shapes2.SetActive(true);
            Shapes3.SetActive(true);

            //그려진 선 모두 지우기
            ClearAllLines();

        }
        else if (Shapes1 != null && Shapes1.activeSelf)
        {
            goto_result();

        }
        checkPopup.SetActive(false);

    }

    void ClearAllLines()
    {
        DrawLine.ClearAllLines();
    }

    private void goto_result() // 결과창의 '완성이야' 버튼을 클릭하며 호출 되어질 함수
    {
        ScoreText.text = Score(collisionCounter.collisionCount, collisionCounter.pass);

        // 점수 저장 -> 결과 화면에서 게임클리어/오버 구분 위해서
        gameResult.score = int.Parse(ScoreText.text); ;

        // 현재 씬 이름 저장 : 결과 창에서 해당 게임으로 돌아오기 위해서 
        gameResult.previousScene = SceneManager.GetActiveScene().name;

        // 결과 화면으로 넘어가기 
        //StartCoroutine(ResultSceneDelay()); // StartCoroutine( "메소드이름", 매개변수 );
        SceneManager.LoadScene("ResultScene");
    }

    // 코루틴: 1초 후에 씬을 로드하는 함수
    //IEnumerator ResultSceneDelay()
    //{
    //    Debug.Log("Waiting for 1 second before loading ResultScene");

    //    // 1초 대기
    //    yield return new WaitForSeconds(1);

    //    Debug.Log("Loading ResultScene");

    //    // 결과 씬 로드
    //    SceneManager.LoadScene("ResultScene");
    //}

    private int maxCollisions = 10; // 기준 충돌 횟수 (20번 충돌하면 0점)
    private float maxScore = 100f; // 현재 점수 (최대 100점)
    private string Score(int collisionCount, bool pass)
    {
        if (collisionCount < 5 && pass==true)
        {
            maxScore = 100;
        }

        else if (pass == false  )
        {
            maxScore = 0;
        }

        else
        {
            // 충돌 횟수에 따른 점수 계산
            maxScore = 100 * (float)(maxCollisions - collisionCount) / maxCollisions;
            // 점수가 음수로 내려가는 것을 방지
            if (maxScore < 0)
            {
                maxScore = 0;
            }
        }

        ScoreText.text = maxScore.ToString("F0");
        return ScoreText.text;
    }
}

