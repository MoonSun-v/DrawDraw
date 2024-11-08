using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// --------------------------------------------------------------------------------------------------------
//// �� ����ȭ�� ĸó�ؼ� json�� �����ϴ� ��ũ��Ʈ �� 
// --------------------------------------------------------------------------------------------------------

public class ImageCapture : MonoBehaviour
{
    public Camera targetCamera;           // ĸó�� ī�޶�
    private Rect captureRect = new Rect(0, 0, 100, 100);    // ĸó ����

    public int sceneIndex;                // �� �ε��� (1���� 6���� ���� ����)


    // [ ����ȭ�� ĸó ���� ]
    //
    // 1. ���� ȭ���� ���� �κ��� ĸó�� Base64 �̹��� ���ڿ��� ��ȯ
    // 2. �̹��� ������ ������ Key�� ã��
    //    - (Ű ���� 5�� �ʰ��ϸ� �̹��� ĸó �ߴ�)
    //    - TestResultData�� ������ ���� ����
    //    - �ش� Key�� TestResultData�� sceneIndex�� ���� �̹��� ����
    // 3. �ش� ID�� TestResultData ����
    //
    void Start()
    {
        string base64Image = GameData.instance.CaptureScreenArea(targetCamera, captureRect);


        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 5)
        {
            Debug.LogWarning("TestResults�� �� �̻� �̹����� ������ �� �����ϴ�. �ִ� Ű ���� 5�Դϴ�.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        SaveImageToScene(currentKey, sceneIndex, base64Image);

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]�� {sceneIndex}�� �̹��� ���� �Ϸ�");
    }


    

    // �� [ �̹��� ���� ]
    //
    private void SaveImageToScene(int key, int sceneIndex, string base64Image)
    {
        TestResultData currentData = GameData.instance.testdata.TestResults[key];

        switch (sceneIndex)
        {
            case 1:
                currentData.Game1Img = base64Image;
                break;
            case 2:
                currentData.Game2Img = base64Image;
                break;
            case 3:
                currentData.Game3Img = base64Image;
                break;
            case 4:
                currentData.Game4Img = base64Image;
                break;
            case 5:
                currentData.Game5Img = base64Image;
                break;
            case 6:
                currentData.Game6Img = base64Image;
                break;
            default:
                Debug.LogWarning("��ȿ���� ���� �� �ε����Դϴ�.");
                break;
        }
    }
}
