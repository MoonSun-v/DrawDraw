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
    public Sprite DogSprite;   // ������ �̹���
    public Sprite CatSprite;   // ����� �̹���
    public Text Name;          


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
    // 
    void SettingProfile()
    {
        if(!GameData.instance.playerdata.PlayerCharacter) { CharacterImg.sprite = DogSprite; }
        else                                              { CharacterImg.sprite = CatSprite; }

        Name.text = GameData.instance.playerdata.PlayerName;
    }



    // �� [ ������ �˾� ���� ��ư ]
    // 
    public void OnProfilePopup()
    {
        Profile.SetActive(true);
    }
    public void OffProfilePopup()
    {
        Profile.SetActive(false);
    }

}