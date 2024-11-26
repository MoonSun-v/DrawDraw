using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// --------------------------------------------------------------------------------------------------------
//// ★ 게임화면 캡처해서 json에 저장하는 스크립트 ★ 
// --------------------------------------------------------------------------------------------------------

public class ImageCapture : MonoBehaviour
{
    public Camera targetCamera;           // 캡처할 카메라
    private Rect captureRect = new Rect(0, 0, 100, 100);    // 캡처 영역

    public int sceneIndex;                // 씬 인덱스 (1부터 6까지 수동 설정)


    // [ 게임화면 캡처 진행 ]
    //
    // 1. 게임 화면의 일정 부분을 캡처해 Base64 이미지 문자열로 변환
    // 2. 이미지 저장할 적절한 Key값 찾기
    //    - (키 값이 5를 초과하면 이미지 캡처 중단)
    //    - TestResultData가 없으면 새로 생성
    //    - 해당 Key의 TestResultData에 sceneIndex에 따라 이미지 저장
    // 3. 해당 ID에 TestResultData 저장
    //
    void Start()
    {
        string base64Image = GameData.instance.CaptureScreenArea(targetCamera, captureRect);


        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 5)
        {
            Debug.LogWarning("TestResults에 더 이상 이미지를 저장할 수 없습니다. 최대 키 값은 5입니다.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        SaveImageToScene(currentKey, sceneIndex, base64Image);

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]의 {sceneIndex}번 이미지 저장 완료");
    }


    

    // ★ [ 이미지 저장 ]
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
                Debug.LogWarning("유효하지 않은 씬 인덱스입니다.");
                break;
        }
    }
}
