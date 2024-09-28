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
    public int testResultId = 1;  // 각 테스트에 대한 결과 ID
    public int gameIndex = 1;     // 게임 이미지 인덱스 (1~6)


    void Start()
    {
        LoadImage();
    }

    void LoadImage()
    {
        if (GameData.instance.testdata.TestResults.ContainsKey(testResultId))
        {
            TestResultData testResultData = GameData.instance.testdata.TestResults[testResultId];
            string base64Image = "";

            // gameIndex에 맞는 이미지를 불러옴
            switch (gameIndex)
            {
                case 1: base64Image = testResultData.Game1Img; break;
                case 2: base64Image = testResultData.Game2Img; break;
                case 3: base64Image = testResultData.Game3Img; break;
                case 4: base64Image = testResultData.Game4Img; break;
                case 5: base64Image = testResultData.Game5Img; break;
                case 6: base64Image = testResultData.Game6Img; break;
            }

            if (!string.IsNullOrEmpty(base64Image))
            {
                // Base64 문자열을 텍스처로 변환한 후 스프라이트로 변환
                Texture2D texture = Base64ToTexture(base64Image);
                Sprite sprite = TextureToSprite(texture);
                targetImage.sprite = sprite; // UI Image에 스프라이트 적용
            }
            else { Debug.LogWarning("Base64 이미지 문자열이 비어 있습니다."); }
        }
        else { Debug.LogWarning("테스트 결과 데이터에서 해당 ID의 데이터가 없습니다."); }
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
