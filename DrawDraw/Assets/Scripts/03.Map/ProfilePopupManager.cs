using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopupManager : MonoBehaviour
{
    [SerializeField] StageManager _stageManager;
    public Image CharacterImg;
    public Sprite DogSprite;   // ������ �̹���
    public Sprite CatSprite;   // ����� �̹���
    public Text Name;

    public Image[] stageImages;     // 20���� ��ư �̹��� �迭
    // public Sprite deactivateImage;       // -> Ȱ��ȭX ����
    public Sprite activateImage_Dog;     // -> Ȱ��ȭO ����
    public Sprite activateImage_Cat;         
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


        int _activateCount = _stageManager.StateStage();

        // �������� ����
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
