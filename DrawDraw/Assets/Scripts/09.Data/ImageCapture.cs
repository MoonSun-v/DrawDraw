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
    public int testResultId = 1;                         // ������ ����� ID
    public int gameIndex = 1;                            // // ���� �̹��� �ε��� (1~6)
    public Camera targetCamera;                          // ĸó�� ī�޶�
    public Rect captureRect = new Rect(0, 0, 50, 50);    // ĸó�� ����  


    // [ �ӽ� ���� �ڵ� ]
    //
    // 1. ���� ȭ���� ���� �κ��� ĸó�� Base64 �̹��� ���ڿ��� ��ȯ
    // 2. TestData �ν��Ͻ��� ������ ���ο� TestResultData ��ü ����
    // 3. �ش� ID�� TestResultData ����
    //
    void Start()
    {
        CaptureAndSaveImage();
    }

    void CaptureAndSaveImage()
    {
        // targetCamera�� �������� �ʾ��� ��� ���� ī�޶� ���
        // if (targetCamera == null){ targetCamera = Camera.main; }


        // �̹��� ĸó
        string base64Image = GameData.instance.CaptureScreenArea(targetCamera, captureRect);
        TestResultData testResultData;

        // �׽�Ʈ ��� �����Ͱ� ������ ���� ����
        if (!GameData.instance.testdata.TestResults.ContainsKey(testResultId))
        {
            testResultData = new TestResultData();
            GameData.instance.testdata.TestResults[testResultId] = testResultData;
        }
        else
        {
            testResultData = GameData.instance.testdata.TestResults[testResultId];
        }

        // gameIndex�� ���� �� �ʵ忡 �̹��� ����
        switch (gameIndex)
        {
            case 1: testResultData.Game1Img = base64Image; break;
            case 2: testResultData.Game2Img = base64Image; break;
            case 3: testResultData.Game3Img = base64Image; break;
            case 4: testResultData.Game4Img = base64Image; break;
            case 5: testResultData.Game5Img = base64Image; break;
            case 6: testResultData.Game6Img = base64Image; break;
        }

        // ������ ����
        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print("ȭ�� ĸó �� ���� �Ϸ�");
    }
}
