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
    public Image[] targetImages;    



    // ★ [ 각 항목 버튼 클릭 시, 호출 ]
    // 
    // 1. TestResults[i]의 이미지 가져오기
    // 2. 각 targetImage에 해당 스프라이트 할당
    // ( 이미지 데이터가 없는 경우 == 아직 테스트를 진행하지 않은 상태 )
    // 
    public void ResultIndexClick(int index)
    {
        if (index < 1 || index > 6) { Debug.LogWarning("유효하지 않은 인덱스입니다."); return;  }

        for (int i = 0; i < 5; i++)
        {
            string base64Image = GetBase64ImageFromResults(index, i);

            if (!string.IsNullOrEmpty(base64Image))
            {
                Texture2D texture = Base64ToTexture(base64Image);
                Sprite sprite = TextureToSprite(texture);
                targetImages[i].sprite = sprite; 
            }
            else
            {
                // 이미지 데이터가 없을 경우 해당 targetImage를 비우거나 유지
                // targetImages[i].sprite = null; // 또는 기존 스프라이트를 유지하고 싶다면 해당 줄 생략
                // Debug.LogWarning($"TestResults[{i}]의 Game{index}Img 이미지가 비어 있습니다."); 
            }
        }
    }



    // ★ [ 해당 index와 i에 따라 Base64 이미지 반환 ]
    //
    // 1. TestResults에서 키가 유효한지 확인
    // 2. index에 따라 적절한 GameImg 반환
    //
    private string GetBase64ImageFromResults(int index, int imageIndex)
    {
        int key = imageIndex; 

        if (GameData.instance.testdata.TestResults.ContainsKey(key))
        {
            TestResultData testResultData = GameData.instance.testdata.TestResults[key];

            switch (index)
            {
                case 1:
                    return testResultData.Game1Img;
                case 2:
                    return testResultData.Game2Img;
                case 3:
                    return testResultData.Game3Img;
                case 4:
                    return testResultData.Game4Img;
                case 5:
                    return testResultData.Game5Img;
                case 6:
                    return testResultData.Game6Img;
                default:
                    Debug.LogWarning("유효하지 않은 인덱스입니다.");
                    return null;
            }
        }
        else
        {
            // Debug.LogWarning($"TestResults에서 키 {key}가 존재하지 않습니다.");
            return null;
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
