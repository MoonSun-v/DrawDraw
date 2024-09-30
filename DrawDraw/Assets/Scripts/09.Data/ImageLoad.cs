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
    public Image[] targetImages;    



    // �� [ �� �׸� ��ư Ŭ�� ��, ȣ�� ]
    // 
    // 1. TestResults[i]�� �̹��� ��������
    // 2. �� targetImage�� �ش� ��������Ʈ �Ҵ�
    // ( �̹��� �����Ͱ� ���� ��� == ���� �׽�Ʈ�� �������� ���� ���� )
    // 
    public void ResultIndexClick(int index)
    {
        if (index < 1 || index > 6) { Debug.LogWarning("��ȿ���� ���� �ε����Դϴ�."); return;  }

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
                // �̹��� �����Ͱ� ���� ��� �ش� targetImage�� ���ų� ����
                // targetImages[i].sprite = null; // �Ǵ� ���� ��������Ʈ�� �����ϰ� �ʹٸ� �ش� �� ����
                // Debug.LogWarning($"TestResults[{i}]�� Game{index}Img �̹����� ��� �ֽ��ϴ�."); 
            }
        }
    }



    // �� [ �ش� index�� i�� ���� Base64 �̹��� ��ȯ ]
    //
    // 1. TestResults���� Ű�� ��ȿ���� Ȯ��
    // 2. index�� ���� ������ GameImg ��ȯ
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
                    Debug.LogWarning("��ȿ���� ���� �ε����Դϴ�.");
                    return null;
            }
        }
        else
        {
            // Debug.LogWarning($"TestResults���� Ű {key}�� �������� �ʽ��ϴ�.");
            return null;
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
