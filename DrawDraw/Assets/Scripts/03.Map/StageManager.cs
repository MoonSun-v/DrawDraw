using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Image[] stageButtonImages; // 20개의 버튼 이미지 배열 -> 활성화 상태 (기본)
    public Sprite[] deactivateImages; // -> 활성화 되지 않은 상태
    public Sprite[] completeImages_Dog;   // -> 완료한 상태
    public Sprite[] completeImages_Cat; 


    private void Start()
    {
        // [ PlayerExp에 따라 활성화된 버튼 수 계산 ]
        int playerExp = GameData.instance.playerdata.PlayerExp;
        int activateCount;

        if (playerExp >= 190)       activateCount = 20;
        else if (playerExp >= 160)  activateCount = 19;
        else if (playerExp >= 120)  activateCount = 16;
        else if (playerExp >= 80)   activateCount = 12;
        else if (playerExp >= 40)   activateCount = 8;
        else                        activateCount = 4;

        // [ 버튼 상태 설정 ]
        //
        // 1. Complete 상태 : ClearStage가 true인 경우  && ( Dog 는 0 or Cat 은 1 )
        // 2. Activate 상태 : PlayerExp 기준 (기본)
        // 3. Deactivate 상태
        //
        for (int i = 0; i < stageButtonImages.Length; i++)
        {
            Image buttonImage = stageButtonImages[i]; 

            if (GameData.instance.trainingdata.ClearStage[i])  
            {
                if (!GameData.instance.playerdata.PlayerCharacter) { buttonImage.sprite = completeImages_Dog[i]; }
                else                                               { buttonImage.sprite = completeImages_Cat[i];  }
            }
            else if (i < activateCount) { }
            else
            {
                buttonImage.sprite = deactivateImages[i];
                buttonImage.raycastTarget = false; 
            }
        }
    }

}
