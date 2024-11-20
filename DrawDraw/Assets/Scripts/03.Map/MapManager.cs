using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // [ ��� ���� ���� ���� ]
    public GameObject Background;
    private Image BackgroundImg;
    public Sprite newSprite;       // ������ ��������Ʈ�� ������ ����


    // [ ������ ���� ���� ���� ]
    public Image CharacterImg;
    public Image Charater_Move_Img;
    public Sprite DogSprite;   // ������ �̹���
    public Sprite CatSprite;   // ����� �̹���
    public Sprite DogMoveSprite;
    public Sprite CatMoveSprite;
    public Text Name;
    public Slider ExpBar;
    public Slider CharacterBar;

    // [ ������ �˾� ���� ���� ] 
    public GameObject Profile;


    // �� [ �ʱ� ���� ] ��
    // 
    // 1. ���� �ð�, ��� Image �����ͼ� Ư�� �ð��뿡 �˸°� ��� ���� 
    // 2. ������ ���� ���� 
    // 
    void Start()
    {
        DateTime currentTime = DateTime.Now;                   // 1
        Debug.Log("���� �ð�: " + currentTime);

        BackgroundImg = Background.GetComponent<Image>();      
        CheckAndChangeSprite(currentTime);

        SettingProfile();                                      // 2

    }



    // �� [ �ð��뺰(����/����) ��� ���� ]
    //
    // ���� 6�ú��� ������ ���� 5�� �������� Ȯ���� �� Sprite ����
    //
    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     
        {
            BackgroundImg.sprite = newSprite;                   
        }
    }



    // �� [ ������ ���� ���� ]
    // 
    // 1. �̹��� ���� : PlayerCharacter :  false -> ������ , true -> �����
    // 2. �̸� ���� 
    // 3. ����ġ�� ���� 
    // 
    void SettingProfile()
    {
        if(!GameData.instance.playerdata.PlayerCharacter) { CharacterImg.sprite = DogSprite; Charater_Move_Img.sprite = DogMoveSprite; }
        else                                              { CharacterImg.sprite = CatSprite; Charater_Move_Img.sprite = CatMoveSprite; }

        Name.text = GameData.instance.playerdata.PlayerName;

        ExpBar.value = GameData.instance.playerdata.PlayerExp;
        CharacterBar.value = GameData.instance.playerdata.PlayerExp - 25;
    }



    // �� [ ������ �˾� ���� ��ư ]
    // 
    public void OnProfilePopup()
    {
        Profile.SetActive(true);
        // StageManager.SetActive(true);
    }
    public void OffProfilePopup()
    {
        Profile.SetActive(false);
    }


    // [ �ÿ��� �� -> �� ��ư ]
    public void BackgroundChange()
    {
        BackgroundImg.sprite = newSprite;
    }
}