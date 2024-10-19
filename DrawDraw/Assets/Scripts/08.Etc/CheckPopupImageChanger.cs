using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPopupImageChanger : MonoBehaviour
{
    // Image 컴포넌트를 참조
    public Image imageComponent;

    private bool userPreference = false; // 사용자 정보를 기반으로 결정 ("dog" 또는 "cat")


    // 고양이와 강아지 이미지 스프라이트
    public Sprite catImage;
    public Sprite dogImage;

    private void Start()
    {
        userPreference = GameData.instance.playerdata.PlayerCharacter;
        Debug.Log(userPreference);
        ChangeImage(userPreference);
    }

    // 조건에 따라 이미지를 변경하는 함수
    public void ChangeImage(bool isCat)
    {
        // PlayerCharacter: false->강아지 , true->고양이 
        if (userPreference == false && dogImage != null)
        {

            // false이면 강아지 이미지로 변경
            imageComponent.sprite = dogImage;
        }
        else if (userPreference == true && catImage != null)
        {
            // true이면 고양이 이미지로 변경
            imageComponent.sprite = catImage;
        }
    }
}