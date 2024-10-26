using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopupManager : MonoBehaviour
{
    public Image CharacterImg;
    public Sprite DogSprite;   // ������ �̹���
    public Sprite CatSprite;   // ����� �̹���
    public Text Name;

    public Image[] stageButtonImages;     // 20���� ��ư �̹��� �迭
    public Sprite deactivateImages_Dog;   // -> �Ϸ� ���� ���� ����
    public Sprite deactivateImages_Cat;   
    public Sprite[] completeImages_Dog;   // -> �Ϸ��� ����
    public Sprite[] completeImages_Cat;


    void Start()
    {
        // ĳ���� �̹��� : PlayerCharacter :  false -> ������ , true -> �����
        if (!GameData.instance.playerdata.PlayerCharacter) { CharacterImg.sprite = DogSprite; }
        else                                               { CharacterImg.sprite = CatSprite; }

        // ������ �̸� 
        string name = GameData.instance.playerdata.PlayerName;
        int exp = GameData.instance.playerdata.PlayerExp;
        int level;
        if (exp >= 190) level = 6;
        else if (exp >= 160) level = 5;
        else if (exp >= 120) level = 4;
        else if (exp >= 80) level = 3;
        else if (exp >= 40) level = 2;
        else level = 1;

        Name.text = "LV." + level + " " + name;


        // �������� ����
        for (int i = 0; i < stageButtonImages.Length; i++)
        {
            Image buttonImage = stageButtonImages[i];

            if (GameData.instance.trainingdata.ClearStage[i])
            {
                if (!GameData.instance.playerdata.PlayerCharacter) { buttonImage.sprite = completeImages_Dog[i]; }
                else                                               { buttonImage.sprite = completeImages_Cat[i]; }
            }
            else
            {
                if (!GameData.instance.playerdata.PlayerCharacter) { buttonImage.sprite = deactivateImages_Dog; }
                else                                               { buttonImage.sprite = deactivateImages_Cat; }
            }
        }


    }

}
