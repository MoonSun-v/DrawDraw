using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// --------------------------------------------------------------------------------------------------------
//// ★ 저장된 이미지 불러와 Image 컴포넌트에 씌우는 스크립트 ★ 
// --------------------------------------------------------------------------------------------------------

public class ImageLoad : MonoBehaviour
{
    public Image targetImage;     // 이미지를 적용할 UI Image 컴포넌트
    public int sceneIndex;


    void Start()
    {
        TestResultData testResultData = GameData.instance.testdata.TestResults[0];
        string base64Image = "";

        switch (sceneIndex)
        {
            case 1:
                base64Image = testResultData.Game1Img;
                break;
            case 2:
                base64Image = testResultData.Game2Img;
                break;
            case 3:
                base64Image = testResultData.Game3Img;
                break;
            case 4:
                base64Image = testResultData.Game4Img;
                break;
            case 5:
                base64Image = testResultData.Game5Img;
                break;
            case 6:
                base64Image = testResultData.Game6Img;
                break;
        }

        if (!string.IsNullOrEmpty(base64Image))
        {
            Texture2D texture = Base64ToTexture(base64Image);
            Sprite sprite = TextureToSprite(texture);
            targetImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Base64 이미지 문자열이 비어 있습니다.");
        }
    }

    // ★ [ Base64 문자열을 Texture2D로 변환 ]
    // 
    // 1. Base64 문자열을 바이트 배열로 변환
    // 2. 임시 크기 2x2
    // 3. 바이트 배열로부터 이미지 데이터 로드해 텍스처 생성
    // 4. 생성된 텍스처 반환
    // 
    public Texture2D Base64ToTexture(string base64String)
    {
        byte[] imageBytes = Convert.FromBase64String(base64String); 
        Texture2D texture = new Texture2D(2, 2); 
        texture.LoadImage(imageBytes);
        return texture;
    }


    // ★ [ Texture2D를 Sprite로 변환 ]
    // UI Image 컴포넌트는 Sprite를 사용하므로, Texture2D를 Sprite로 변환할 필요가 있음 
    //
    public Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
