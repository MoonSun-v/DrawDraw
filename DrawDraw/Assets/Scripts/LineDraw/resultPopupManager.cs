using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultPopupManager : MonoBehaviour
{
    public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui
    public Text ScoreText; // 게임의 결과를 가져올 Text Ui\

    public GameResultSO gameResult; // 게임 결과화면 관리 SO

    public void Show()
    {
        Text_GameResult.text = ScoreText.text+"번 벗어났습니다."; // 팝업의 점수 창에 현재 점수를 표시한다.
        transform.gameObject.SetActive(true); // 결과 팝업 창을 화면에 표시
    }

    public void OnClick_finish() // 결과창의 '완성이야' 버튼을 클릭하며 호출 되어질 함수
    {
        // "완성이야" 버튼 클릭 시 : 결과 화면으로 넘어갑니다.
        if (gameResult == null)
        {
            Debug.LogError("GameResult가 할당되지 않았습니다.");
            return;
        }

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
