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
    public Image targetImage; // �̹����� ������ UI Image ������Ʈ


    // [ �ӽ� ���� �ڵ� ]
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
               
                Texture2D texture = Base64ToTexture(base64Image);   // 1. Base64 ���ڿ��� �ؽ�ó�� ��ȯ

                Sprite sprite = TextureToSprite(texture);           // 2. �ؽ�ó�� ��������Ʈ�� ��ȯ

                targetImage.sprite = sprite;                        // 3. UI Image�� ��������Ʈ ����

            }
            else {  Debug.LogWarning("Base64 �̹��� ���ڿ��� ��� �ֽ��ϴ�.");  }

        }
        else { Debug.LogWarning("�׽�Ʈ ��� �����Ϳ��� ID 1�� �����Ͱ� �����ϴ�.");  }

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
