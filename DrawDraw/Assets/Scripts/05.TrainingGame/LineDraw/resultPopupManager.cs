using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultPopupManager : MonoBehaviour
{
    public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui
    public Text ScoreText; // 게임의 결과를 가져올 Text Ui

    //private GameResultSO gameResult; // 게임 결과화면 관리 SO
    //private EdgeCollider2D edgeCollider;

    

    public void Show()
    {
        Text_GameResult.text = ScoreText.text + "점"; // 팝업의 점수 창에 현재 점수를 표시한다.

        //if (CollisionCounter.Instance.IsSafe())
        //{
            
        //}
        //else
        //{
        //    Text_GameResult.text = "밑그림밖에서 그려졌습니다.";
        //}

        transform.gameObject.SetActive(true); // 결과 팝업 창을 화면에 표시
    }
}
