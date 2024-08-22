using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    
    // gameResult : ���� ��� �������� ��ũ���ͺ������Ʈ
    // StageNum   : �÷����� �������� ������ ���ڷ� ǥ���ϴ� ���� 
    //              �������� ������� 0��~18���� ���ڸ� ������. 
    public GameResultSO gameResult;
    private int StageNum = 30; 

    // ( �ӽ� ������ )
    public Text scoreText;  // ������Ÿ�Կ����� ��� 
    private bool isClear;   // ���� Ŭ�����ߴ°�? 



    // �� [ �� �Ʒ� ���Ӻ�, ��� ���� ] ��  -------------------------------------------------------
    //
    void Start()
    {

        // 1. [ ���� ���� �׸��� ]
        // 50 % �̸�           : ����ġ X  ,���� ����
        // 50 % �̻� 60 % �̸� : ����ġ 5  ,���� ����
        // 60 % �̻�           : ����ġ 10 ,���� ����
        if (gameResult.previousScene == "DotLineScene")
        {
            StageNum = 0;

            if      (gameResult.score < 50)  { isClear = false; /*FailSetting(StageNum);*/ }    
            else if (gameResult.score < 60)  { isClear = true;  /*SuccessSetting(StageNum);*/ }  
            else                             { isClear = true;  /*ClearSetting(StageNum);*/ }    
        }


        // 2. [ �� ���� �׸��� ]
        // 6�� �̻��� �浹 : ���� ����
        else if (gameResult.previousScene == "LineScene")
        {
            if (gameResult.score >= 6) { isClear = false; } 
            else                       { isClear = true; }  
        }


        // 3. [ ��ũ��ġ ]
        // 60 % �̸�           : ����ġX   ,���� ����
        // 60 % �̻� 80 % �̸� : ����ġ 5  ,���� ����
        // 80 % �̻�           : ����ġ 10 ,���� ����
        else if (gameResult.previousScene == "ScratchScene")
        {
            if (gameResult.score < 60) { isClear = false; } 
            else                       { isClear = true; }   

        }


        // 4. [ �������� ]
        // ��ĥ�� ������ ���� )  -2�� �̻�   : ����ġX   ,���� ����
        //                       -1��        : ����ġ 5  ,���� ����
        //                       ��� ����   : ����ġ 10 ,���� ����


        // 5. [ ĥ�� ]
        // ���õ� ���� ��ġ�ϴ°� )  No  : ����ġX   ,���� ����
        //                             Yes : ����ġ 10 ,���� ����



        // 6. [ ���� ]
        // ��� ������ �˸°� �������°� )  No  : ����ġX   ,���� ����
        //                                  Yes : ����ġ 10 ,���� ����



        // ĳ���� ���� ����
        if (isClear) { scoreText.text = "�����ϰ� �ִ� ĳ����"; }
        else         { scoreText.text = "�ƽ����ϴ� ĳ����"; }

        
        /*
        GameData.instance.SavePlayerData();
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();
        */
     }




    // �� [ �Ʒð��� ��� ���� �����ϴ� �޼ҵ� ] �� -------------------------------------------------
    //
    // 1. FailSetting()    : ����ġ X,  ���� ����
    // 2. SuccessSetting() : ����ġ 5,  ���� ���� 
    // 3. ClearSetting()   : ����ġ 10, ���� ����  
    // => �� �Լ��� ������ ���������� ���ڸ� �޾ƿ´�.=(int stagenum) 

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





    // �� [ �� �̵� ��ư ] -----------------------------------------------------------------------------
    //
    // Restart() : ���� ������ ������ ���ư���
    // End()     : ��ȭ������ ���ư���

    public void Restart(){ SceneManager.LoadScene(gameResult.previousScene); }
    public void End(){ SceneManager.LoadScene("MapScene"); }
}
