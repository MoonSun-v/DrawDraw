using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Image[] stageButtonImages; // 20���� ��ư �̹��� �迭 -> Ȱ��ȭ ���� (�⺻)
    public Sprite[] activateImages;
    public Sprite[] deactivateImages; // -> Ȱ��ȭ ���� ���� ����
    public Sprite[] completeImages_Dog;   // -> �Ϸ��� ����
    public Sprite[] completeImages_Cat; 


    private void Start()
    {
        // [ PlayerExp�� ���� Ȱ��ȭ�� ��ư �� ��� ]
        int _activateCount = StateStage();

        // [ ��ư ���� ���� ]
        //
        // 1. Complete ���� : ClearStage�� true�� ���  && ( Dog �� 0 or Cat �� 1 )
        //                  : ������ ���������� ������ �÷��� ����
        // 2. Activate ���� : PlayerExp ���� (�⺻)
        // 3. Deactivate ����
        //
        for (int i = 0; i < stageButtonImages.Length; i++)
        {
            Image buttonImage = stageButtonImages[i]; 

            if (GameData.instance.trainingdata.ClearStage[i])  
            {
                if (!GameData.instance.playerdata.PlayerCharacter) { buttonImage.sprite = completeImages_Dog[i]; }
                else                                               { buttonImage.sprite = completeImages_Cat[i];  }
            }
            else if (i < _activateCount) { }
            else
            {
                buttonImage.sprite = deactivateImages[i];
                buttonImage.raycastTarget = false; 
            }
        }
    }


    // [ PlayerExp�� ���� Ȱ��ȭ�� ��ư �� ��� ]
    //
    public int StateStage()
    {
        int playerExp = GameData.instance.playerdata.PlayerExp;
        int activateCount;

        if (playerExp >= 190) activateCount = 20;
        else if (playerExp >= 160) activateCount = 19;
        else if (playerExp >= 120) activateCount = 16;
        else if (playerExp >= 80) activateCount = 12;
        else if (playerExp >= 40) activateCount = 8;
        else activateCount = 4;

        return activateCount;
    }

    // ----------------------------------------------------------------------------------------------------------------------
    // [ �ÿ��� : �������� ��� ���� ] 
    // ��� ���������� Activate ���·� ����� : �̹��� ���� �� ��ư Ŭ�� Ȱ��ȭ 
    //                                          (������ �������� ����) 
    public void UnlockedStage()
    {
        for (int i = 0; i < stageButtonImages.Length-1; i++)
        {
            Image buttonImage = stageButtonImages[i];
            buttonImage.sprite = activateImages[i];
            buttonImage.raycastTarget = true; 
        }
    }

}
