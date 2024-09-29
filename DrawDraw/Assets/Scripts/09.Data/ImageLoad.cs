using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// --------------------------------------------------------------------------------------------------------
//// �� ����� �̹��� �ҷ��� Image ������Ʈ�� ����� ��ũ��Ʈ �� 
// --------------------------------------------------------------------------------------------------------

public class ImageLoad : MonoBehaviour
{
    public Image targetImage;     // �̹����� ������ UI Image ������Ʈ
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
            Debug.LogWarning("Base64 �̹��� ���ڿ��� ��� �ֽ��ϴ�.");
        }
    }

    // �� [ Base64 ���ڿ��� Texture2D�� ��ȯ ]
    // 
    // 1. Base64 ���ڿ��� ����Ʈ �迭�� ��ȯ
    // 2. �ӽ� ũ�� 2x2
    // 3. ����Ʈ �迭�κ��� �̹��� ������ �ε��� �ؽ�ó ����
    // 4. ������ �ؽ�ó ��ȯ
    // 
    public Texture2D Base64ToTexture(string base64String)
    {
        byte[] imageBytes = Convert.FromBase64String(base64String); 
        Texture2D texture = new Texture2D(2, 2); 
        texture.LoadImage(imageBytes);
        return texture;
    }


    // �� [ Texture2D�� Sprite�� ��ȯ ]
    // UI Image ������Ʈ�� Sprite�� ����ϹǷ�, Texture2D�� Sprite�� ��ȯ�� �ʿ䰡 ���� 
    //
    public Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
