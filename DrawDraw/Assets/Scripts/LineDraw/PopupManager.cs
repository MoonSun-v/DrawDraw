using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui

    private CollisionCounter count; // CollisionCounter 스크립트를 참조할 변수
    private  MonoBehaviour LineDrawManager; // LineDrawManager 스크립트를 참조할 변수

    private void Awake()
    {
        transform.gameObject.SetActive(false); // 게임이 시작되면 결과 팝업 창을 보이지 않도록 한다.   
    }

    public void Show()
    {
        int score = count.GetCollisionCount(); // CollisionCounter부터 충돌 횟수를 불러온다.
        Debug.Log(score);
        //Text_GameResult.text = "충돌 횟수 : " + score.ToString(); // 팝업의 점수 창에 현재 점수를 표시한다.

        transform.gameObject.SetActive(true); // 결과 팝업 창을 화면에 표시
        //DrawArea.SetDrawActivate(false);
    }

}
