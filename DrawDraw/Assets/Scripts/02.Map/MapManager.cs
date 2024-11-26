using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // [ 배경 변경 관련 변수 ]
    public GameObject Background;
    private Image BackgroundImg;
    public Sprite newSprite;       // 변경할 스프라이트를 참조할 변수


    // [ 프로필 세팅 관련 변수 ]
    public Image CharacterImg;
    public Image Charater_Move_Img;
    public Sprite DogSprite;   // 강아지 이미지
    public Sprite CatSprite;   // 고양이 이미지
    public Sprite DogMoveSprite;
    public Sprite CatMoveSprite;
    public Text Name;
    public Slider ExpBar;
    public Slider CharacterBar;

    // [ 프로필 팝업 관련 변수 ] 
    public GameObject Profile;


    // ★ [ 초기 세팅 ] ★
    // 
    // 1. 현재 시간, 배경 Image 가져와서 특정 시간대에 알맞게 배경 변경 
    // 2. 프로필 정보 세팅 
    // 
    void Start()
    {
        DateTime currentTime = DateTime.Now;                   // 1
        Debug.Log("현재 시간: " + currentTime);

        BackgroundImg = Background.GetComponent<Image>();      
        CheckAndChangeSprite(currentTime);

        SettingProfile();                                      // 2

    }



    // ★ [ 시간대별(오전/오후) 배경 변경 ]
    //
    // 오후 6시부터 다음날 오전 5시 사이인지 확인한 후 Sprite 변경
    //
    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     
        {
            BackgroundImg.sprite = newSprite;                   
        }
    }



    // ★ [ 프로필 정보 세팅 ]
    // 
    // 1. 이미지 세팅 : PlayerCharacter :  false -> 강아지 , true -> 고양이
    // 2. 이름 세팅 
    // 3. 경험치바 세팅 
    // 
    void SettingProfile()
    {
        if(!GameData.instance.playerdata.PlayerCharacter) { CharacterImg.sprite = DogSprite; Charater_Move_Img.sprite = DogMoveSprite; }
        else                                              { CharacterImg.sprite = CatSprite; Charater_Move_Img.sprite = CatMoveSprite; }

        Name.text = GameData.instance.playerdata.PlayerName;

        ExpBar.value = GameData.instance.playerdata.PlayerExp;
        CharacterBar.value = GameData.instance.playerdata.PlayerExp - 25;
    }



    // ★ [ 프로필 팝업 관련 버튼 ]
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


    // [ 시연용 낮 -> 밤 버튼 ]
    public void BackgroundChange()
    {
        BackgroundImg.sprite = newSprite;
    }
}