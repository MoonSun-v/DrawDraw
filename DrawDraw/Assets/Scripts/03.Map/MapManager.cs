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


    // [ 프로필 팝업 관련 변수] 
    public GameObject Profile;



    // - 현재 시간 가져오기
    // - 배경의 Image 컴포넌트 가져오기
    // - 시간 확인 및 스프라이트 변경 
    void Start()
    {
        DateTime currentTime = DateTime.Now;                   
        Debug.Log("현재 시간: " + currentTime);

        BackgroundImg = Background.GetComponent<Image>();      

        CheckAndChangeSprite(currentTime);                     
    }



    // [ 시간대별(오전/오후) 배경 변경 메소드 ]
    // 오후 6시부터 다음날 오전 5시 사이인지 확인한 후 Sprite 변경
    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     
        {
            BackgroundImg.sprite = newSprite;                   
        }
    }



    // [ 프로필 팝업 관련 버튼 메소드 ]
    public void OnProfilePopup()
    {
        Profile.SetActive(true);
    }
    public void OffProfilePopup()
    {
        Profile.SetActive(false);
    }

}