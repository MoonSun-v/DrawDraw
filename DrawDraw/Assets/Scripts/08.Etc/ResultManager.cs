using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    
    // gameResult : 게임 결과 가져오는 스크립터블오브젝트
    // StageNum   : 플레이한 스테이지 정보를 숫자로 표기하는 변수 
    //              스테이지 순서대로 0번~18번의 숫자를 가진다. 
    public GameResultSO gameResult;
    private int StageNum = 30; 

    // ( 임시 변수들 )
    public Text scoreText;  // 프로토타입에서만 사용 
    private bool isClear;   // 게임 클리어했는가? 



    // ★ [ 각 훈련 게임별, 결과 세팅 ] ★  -------------------------------------------------------
    //
    void Start()
    {

        // 1. [ 점선 따라 그리기 ]
        // 50 % 미만           : 경험치 X  ,게임 실패
        // 50 % 이상 60 % 미만 : 경험치 5  ,게임 성공
        // 60 % 이상           : 경험치 10 ,게임 성공
        if (gameResult.previousScene == "DotLineScene")
        {
            StageNum = 0;

            if      (gameResult.score < 50)  { isClear = false; /*FailSetting(StageNum);*/ }    
            else if (gameResult.score < 60)  { isClear = true;  /*SuccessSetting(StageNum);*/ }  
            else                             { isClear = true;  /*ClearSetting(StageNum);*/ }    
        }


        // 2. [ 선 따라 그리기 ]
        // 6번 이상의 충돌 : 게임 오버
        else if (gameResult.previousScene == "LineScene")
        {
            if (gameResult.score >= 6) { isClear = false; } 
            else                       { isClear = true; }  
        }


        // 3. [ 스크래치 ]
        // 60 % 미만           : 경험치X   ,게임 실패
        // 60 % 이상 80 % 미만 : 경험치 5  ,게임 성공
        // 80 % 이상           : 경험치 10 ,게임 성공
        else if (gameResult.previousScene == "ScratchScene")
        {
            if (gameResult.score < 60) { isClear = false; } 
            else                       { isClear = true; }   

        }


        // 4. [ 도형조합 ]
        // 색칠된 도형의 개수 )  -2개 이상   : 경험치X   ,게임 실패
        //                       -1개        : 경험치 5  ,게임 성공
        //                       모든 도형   : 경험치 10 ,게임 성공


        // 5. [ 칠교 ]
        // 제시된 모양과 일치하는가 )  No  : 경험치X   ,게임 실패
        //                             Yes : 경험치 10 ,게임 성공



        // 6. [ 퍼즐 ]
        // 모든 퍼즐이 알맞게 맞춰졌는가 )  No  : 경험치X   ,게임 실패
        //                                  Yes : 경험치 10 ,게임 성공



        // 캐릭터 상태 세팅
        if (isClear) { scoreText.text = "축하하고 있는 캐릭터"; }
        else         { scoreText.text = "아쉬워하는 캐릭터"; }

        
        /*
        GameData.instance.SavePlayerData();
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();
        */
     }




    // ★ [ 훈련게임 결과 정보 저장하는 메소드 ] ★ -------------------------------------------------
    //
    // 1. FailSetting()    : 경험치 X,  게임 실패
    // 2. SuccessSetting() : 경험치 5,  게임 성공 
    // 3. ClearSetting()   : 경험치 10, 게임 성공  
    // => 각 함수는 지정된 스테이지의 숫자를 받아온다.=(int stagenum) 

    void FailSetting(int stagenum)
    {
        GameData.instance.trainingdata.FailNum[stagenum] += 1;
    }

    void SuccessSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 5;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;
    }

    void ClearSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 10;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;
    }





    // ★ [ 씬 이동 버튼 ] -----------------------------------------------------------------------------
    //
    // Restart() : 이전 게임의 씬으로 돌아가기
    // End()     : 맵화면으로 돌아가기

    public void Restart(){ SceneManager.LoadScene(gameResult.previousScene); }
    public void End(){ SceneManager.LoadScene("MapScene"); }
}
