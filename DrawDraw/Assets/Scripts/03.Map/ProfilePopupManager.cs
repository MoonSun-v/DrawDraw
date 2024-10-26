using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopupManager : MonoBehaviour
{
    public Image CharacterImg;
    public Sprite DogSprite;   // 강아지 이미지
    public Sprite CatSprite;   // 고양이 이미지
    public Text Name;

    public Image[] stageButtonImages;     // 20개의 버튼 이미지 배열
    public Sprite deactivateImages_Dog;   // -> 완료 하지 않은 상태
    public Sprite deactivateImages_Cat;   
    public Sprite[] completeImages_Dog;   // -> 완료한 상태
    public Sprite[] completeImages_Cat;


    void Start()
    {
        // 캐릭터 이미지 : PlayerCharacter :  false -> 강아지 , true -> 고양이
        if (!GameData.instance.playerdata.PlayerCharacter) { CharacterImg.sprite = DogSprite; }
        else                                               { CharacterImg.sprite = CatSprite; }

        // 레벨과 이름 
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


        // 스테이지 도장
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
