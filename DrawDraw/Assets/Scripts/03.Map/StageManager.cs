using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Image[] stageButtonImages; // 20���� ��ư �̹��� �迭 -> Ȱ��ȭ ���� (�⺻)
    public Sprite[] deactivateImages; // -> Ȱ��ȭ ���� ���� ����
    public Sprite[] completeImages;   // -> �Ϸ��� ����


    private void Start()
    {
        // [ PlayerExp�� ���� Ȱ��ȭ�� ��ư �� ��� ]
        int playerExp = GameData.instance.playerdata.PlayerExp;
        int activateCount;

        if (playerExp >= 190)       activateCount = 20;
        else if (playerExp >= 160)  activateCount = 19;
        else if (playerExp >= 120)  activateCount = 16;
        else if (playerExp >= 80)   activateCount = 12;
        else if (playerExp >= 40)   activateCount = 8;
        else                        activateCount = 4;

        // [ ��ư ���� ���� ]
        //
        // 1. Complete ���� : ClearStage�� true�� ��� 
        // 2. Activate ���� : PlayerExp ���� (�⺻)
        // 3. Deactivate ����
        //
        for (int i = 0; i < stageButtonImages.Length; i++)
        {
            Image buttonImage = stageButtonImages[i]; 

            if (GameData.instance.trainingdata.ClearStage[i])  
            {
                buttonImage.sprite = completeImages[i]; 
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
