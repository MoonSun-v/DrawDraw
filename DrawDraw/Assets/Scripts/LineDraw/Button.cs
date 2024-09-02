using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject current_popup;
    //public resultPopupManager result_popup; // PopupManager 스크립트를 참조할 변수
    public GameObject check_popup; // PopupManager 스크립트를 참조할 변수

    //int Score; // 선 그리기 게임에서의 최종 점수 

    public Text ScoreText; // 게임의 결과를 가져올 Text Ui

    public GameResultSO gameResult; // 게임 결과화면 관리 SO
    internal object onClick;

    public void OnClick_close() // '창 닫기' 버튼을 클릭하며 호출 되어질 함수
    {
        current_popup.transform.gameObject.SetActive(false);
    }
    public void ShowCheckPopup()
    {
        check_popup.SetActive(true); // 확인 팝업 창을 화면에 표시
    }
    public void OnClick_finish() // 결과창의 '완성이야' 버튼을 클릭하며 호출 되어질 함수
    {
        // "완성이야" 버튼 클릭 시 : 결과 화면으로 넘어갑니다.
        //if (gameResult == null)
        //{
        //    Debug.LogError("GameResult가 할당되지 않았습니다.");
        //    return;
        //}

        // 점수 저장 -> 결과 화면에서 게임클리어/오버 구분 위해서
        gameResult.score = int.Parse(ScoreText.text);


        // 현재 씬 이름 저장 : 결과 창에서 해당 게임으로 돌아오기 위해서 
        gameResult.previousScene = SceneManager.GetActiveScene().name;

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

