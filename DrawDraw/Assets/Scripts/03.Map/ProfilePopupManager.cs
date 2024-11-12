using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopupManager : MonoBehaviour
{
    [SerializeField] StageManager _stageManager;
    public Image CharacterImg;
    public Sprite DogSprite;   // 강아지 이미지
    public Sprite CatSprite;   // 고양이 이미지
    public Text Name;

    public Image[] stageImages;     // 20개의 버튼 이미지 배열
    // public Sprite deactivateImage;       // -> 활성화X 상태
    public Sprite activateImage_Dog;     // -> 활성화O 상태
    public Sprite activateImage_Cat;         
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


        int _activateCount = _stageManager.StateStage();

        // 스테이지 도장
        for (int i = 0; i < stageImages.Length; i++)
        {
            Image StageImage = stageImages[i];

            if (GameData.instance.trainingdata.ClearStage[i])
            {
                if (!GameData.instance.playerdata.PlayerCharacter) { StageImage.sprite = completeImages_Dog[i]; }
                else { StageImage.sprite = completeImages_Cat[i]; }
            }
            else if (i < _activateCount) 
            {
                if (!GameData.instance.playerdata.PlayerCharacter) { StageImage.sprite = activateImage_Dog; }
                else                                               { StageImage.sprite = activateImage_Cat; }
            }
            // else {  buttonImage.sprite = deactivateImage; }
        }
    }

}
