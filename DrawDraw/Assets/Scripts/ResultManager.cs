using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameResultSO gameResult;
    public Text scoreText; // 프로토타입에서만 사용 

    private bool isClear; // 게임 클리어했는가?

    // Start is called before the first frame update
    void Start()
    {
        // 각 게임별, 결과 세팅

        // 스크래치 
        // 60 % 미만: 게임 실패
        // 60 % 이상 80 % 미만 : 경험치 5
        // 80 % 이상 : 경험치 10
        if (gameResult.previousScene == "ScratchScene")
        {
            if(gameResult.score < 60) // 게임 오버
            {
                isClear = false;
            }
            else // 게임 클리어
            {
                isClear = true;
            }
            
        }
        
        if(isClear)
        {
            scoreText.text = "축하하고 있는 캐릭터";
        }
        else
        {
            scoreText.text = "아쉬워하는 캐릭터";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        // 이전 게임의 씬으로 돌아가기 
        SceneManager.LoadScene(gameResult.previousScene);
    }

    public void End()
    {
        // 맵화면으로 돌아가기
        SceneManager.LoadScene("MapScene");
    }
}
