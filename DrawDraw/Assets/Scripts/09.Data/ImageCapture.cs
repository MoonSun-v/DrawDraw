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
    public int testResultId = 1;                         // ���÷� ������ ����� ID
    public Camera targetCamera;                          // ĸó�� ī�޶� (�ʿ信 ���� �ν����Ϳ��� �Ҵ�)
    public Rect captureRect = new Rect(0, 0, 50, 50);    // ĸó�� ����   (�ν����Ϳ��� ���� ����)


    // [ �ӽ� ���� �ڵ� ]
    //
    // 1. ���� ȭ���� ���� �κ��� ĸó�� Base64 �̹��� ���ڿ��� ��ȯ
    // 2. TestData �ν��Ͻ��� ������ ���ο� TestResultData ��ü ����
    // 3. �ش� ID�� TestResultData ����
    //
    void Start()
    {
        // targetCamera�� �������� �ʾ��� ��� ���� ī�޶� ���
        // if (targetCamera == null){ targetCamera = Camera.main; }

        string base64Image = GameData.instance.CaptureScreenArea(targetCamera, captureRect);
        print(base64Image);

        TestResultData testResultData = new TestResultData();
        testResultData.Game1Img = base64Image;

        GameData.instance.testdata.TestResults[testResultId] = testResultData;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

       print("ȭ�� ĸó �� ���� �Ϸ�");
        
    }

}
