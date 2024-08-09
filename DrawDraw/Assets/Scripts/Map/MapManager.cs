using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Sprite newSprite;   // 변경할 스프라이트를 참조할 변수
    // private SpriteRenderer spriteRenderer;

    private Image Background;


    void Start()
    {
        
        DateTime currentTime = DateTime.Now;     // 현재 시간 가져오기

        Debug.Log("현재 시간: " + currentTime);  // 시간 출력

        // 현재 오브젝트의 SpriteRenderer 컴포넌트를 가져옴
        // spriteRenderer = GetComponent<SpriteRenderer>();

        Background = GetComponent<Image>();      // 현재 오브젝트의 Image 컴포넌트를 가져옴

        CheckAndChangeSprite(currentTime);       // 시간 확인 및 스프라이트 변경
    }


    void Update()
    {
        //// 매 프레임마다 현재 시간 업데이트 및 출력
        // DateTime currentTime = DateTime.Now;
        // Debug.Log("현재 시간: " + currentTime);
    }


    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     // 오후 6시부터 다음날 오전 5시 사이인지 확인
        {
            Background.sprite = newSprite;                      // 스프라이트 변경
        }
    }
}
