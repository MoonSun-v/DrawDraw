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
    //
    public GameResultSO gameResult;
    private int StageNum = 30; 


    // ( 임시 변수들 )
    public Text scoreText;  // 프로토타입에서만 사용 
    private bool isClear;   // 게임 클리어했는가? 



    // ★ [ 각 훈련 게임별 스테이지 이름 ]
    List<string> dotLineScenes = new List<string> { "DotLineScene1", "DotLineScene2", "DotLineScene3" }; 
    List<string> LineScenes = new List<string> { "1LineScene", "2LineScene", "3LineScene", "4LineScene", "5LineScene", "6LineScene" };
    List<string> ScratchScenes = new List<string> { "ScratchScene1", "ScratchScene2" };
    List<string> FigureCombiScenes = new List<string> { "1Pinwheel", "1Sun", "2Rocket", "2Ship", "3Person", "3TheTrain" };
    List<string> TangramScenes = new List<string> { "TangramScene_Lv1", "TangramScene_Lv2", "TangramScene_Lv3" };
    List<string> PuzzleScenes = new List<string> { "PuzzleScene_1", "PuzzleScene_2" };


    // -----------------------------------------------------------------------------------------------------
    // ★ [ 각 훈련 게임별, 결과 세팅 ] ★  ----------------------------------------------------------------
    //
    void Start()
    {

        // 1. [ 점선 따라 그리기 ]
        // 50 % 미만           : 경험치 X  ,게임 실패
        // 50 % 이상 60 % 미만 : 경험치 5  ,게임 성공
        // 60 % 이상           : 경험치 10 ,게임 성공
        
        if (dotLineScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "DotLineScene1") { StageNum = 0; }       
            else if (gameResult.previousScene == "DotLineScene2") { StageNum = 2; }
            else if (gameResult.previousScene == "DotLineScene3") { StageNum = 4; }

            #endregion

            if (gameResult.score < 50)       { isClear = false; FailSetting(StageNum); }    
            else if (gameResult.score < 60)  { isClear = true;  SuccessSetting(StageNum); }  
            else                             { isClear = true;  ClearSetting(StageNum); }    
        }



        // 2. [ 선 따라 그리기 ]
        // 6번 이상의 충돌 : 게임 오버

        else if (LineScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "1LineScene") { StageNum = 1; }
            else if (gameResult.previousScene == "2LineScene") { StageNum = 3; }
            else if (gameResult.previousScene == "3LineScene") { StageNum = 5; }
            else if (gameResult.previousScene == "4LineScene") { StageNum = 6; }
            else if (gameResult.previousScene == "5LineScene") { StageNum = 10; }
            else if (gameResult.previousScene == "6LineScene") { StageNum = 16; }

            #endregion

            if (gameResult.score >= 6) { isClear = false; FailSetting(StageNum); } 
            else                       { isClear = true; ClearSetting(StageNum); }
        }


        // 3. [ 스크래치 ]
        // 60 % 미만           : 경험치X   ,게임 실패
        // 60 % 이상 80 % 미만 : 경험치 5  ,게임 성공
        // 80 % 이상           : 경험치 10 ,게임 성공

        else if (ScratchScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "ScratchScene1")      { StageNum = 7; }
            else if (gameResult.previousScene == "ScratchScene2") { StageNum = 12; }

            #endregion

            if (gameResult.score < 60)      { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else                            { isClear = true; ClearSetting(StageNum); }

        }


        // 4. [ 도형조합 ]
        // 색칠된 도형의 개수 )  -2개 이상   : 경험치X   ,게임 실패
        //                       -1개        : 경험치 5  ,게임 성공
        //                       모든 도형   : 경험치 10 ,게임 성공

        // => 점수화 되어있는 듯? 일단 < 60, 80 > 기준으로 세팅해놓음 

        else if (FigureCombiScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당
            
            if (gameResult.previousScene == "1Pinwheel" || gameResult.previousScene == "1Sun") { StageNum = 8; }
            else if (gameResult.previousScene == "2Rocket" || gameResult.previousScene == "2Ship") { StageNum = 9; }
            else if (gameResult.previousScene == "3Person" || gameResult.previousScene == "3TheTrain") { StageNum = 13; }

            #endregion

            if (gameResult.score < 60)      { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else                            { isClear = true; ClearSetting(StageNum); }

        }


        // 5. [ 칠교 ]
        // 제시된 모양과 일치하는가 )  No  : 경험치X   ,게임 실패
        //                             Yes : 경험치 10 ,게임 성공

        else if (TangramScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "TangramScene_Lv1")    { StageNum = 11; }
            else if (gameResult.previousScene == "TangramScene_Lv2") { StageNum = 15; }
            else if (gameResult.previousScene == "TangramScene_Lv3") { StageNum = 18; }

            #endregion

            if (gameResult.score == 0)        { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score == 100) { isClear = true; ClearSetting(StageNum); }
            else { Debug.LogWarning("gameResult.score 값이 잘못 할당되었습니다. 0 또는 100을 할당해주세요"); }

        }


        // 6. [ 퍼즐 ]
        // 모든 퍼즐이 알맞게 맞춰졌는가 )  No  : 경험치X   ,게임 실패
        //                                  Yes : 경험치 10 ,게임 성공

        else if (PuzzleScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "PuzzleScene_1") { StageNum = 14; }
            else if (gameResult.previousScene == "PuzzleScene_1") { StageNum = 17; }

            #endregion

            if (gameResult.score == 0)        { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score == 100) { isClear = true; ClearSetting(StageNum); }
            else { Debug.LogWarning("gameResult.score 값이 잘못 할당되었습니다. 0 또는 100을 할당해주세요"); }

        }


        // 캐릭터 상태 세팅
        if (isClear) { scoreText.text = "축하하고 있는 캐릭터"; }
        else         { scoreText.text = "아쉬워하는 캐릭터"; }

        
        
        GameData.instance.SavePlayerData();
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();
        
     }




    // ★ [ 훈련게임 결과 정보 저장하는 메소드 ] ★ -------------------------------------------------
    //
    // 1. FailSetting()    : 경험치 X,  게임 실패 -> 3회 이상 실패 시, 힌트 이벤트 작동 
    // 2. SuccessSetting() : 경험치 5,  게임 성공 
    // 3. ClearSetting()   : 경험치 10, 게임 성공  
    // => 각 함수는 지정된 스테이지의 숫자를 받아온다.=(int stagenum) 

    void FailSetting(int stagenum)
    {
        GameData.instance.trainingdata.FailNum[stagenum] += 1;
        // print($"{stagenum}번 스테이지의 실패 횟수 저장 완료");
    }

    void SuccessSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 5;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;

        // print($"{stagenum}번 스테이지의 결과값 저장 완료");
    }

    void ClearSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 10;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;

        // print($"{stagenum}번 스테이지의 결과값 저장 완료");
    }





    // ★ [ 씬 이동 버튼 ] -----------------------------------------------------------------------------
    //
    // Restart() : 이전 게임의 씬으로 돌아가기
    // End()     : 맵화면으로 돌아가기

    public void Restart(){ SceneManager.LoadScene(gameResult.previousScene); }
    public void End(){ SceneManager.LoadScene("MapScene"); }
}
