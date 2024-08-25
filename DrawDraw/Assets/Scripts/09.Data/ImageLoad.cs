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
    public Image targetImage; // 이미지를 적용할 UI Image 컴포넌트


    // [ 임시 구현 코드 ]
    //
    void Start()
    {
        if (GameData.instance.testdata.TestResults.ContainsKey(1))
        {
            TestResultData testResultData = GameData.instance.testdata.TestResults[1];

            string base64Image = testResultData.Game1Img;
            print(base64Image);

            if (!string.IsNullOrEmpty(base64Image))
            {
               
                Texture2D texture = Base64ToTexture(base64Image);   // 1. Base64 문자열을 텍스처로 변환

                Sprite sprite = TextureToSprite(texture);           // 2. 텍스처를 스프라이트로 변환

                targetImage.sprite = sprite;                        // 3. UI Image에 스프라이트 적용

            }
            else {  Debug.LogWarning("Base64 이미지 문자열이 비어 있습니다.");  }

        }
        else { Debug.LogWarning("테스트 결과 데이터에서 ID 1의 데이터가 없습니다.");  }

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
