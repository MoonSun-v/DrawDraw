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
    public int testResultId = 1;                         // 저장할 결과의 ID
    public int gameIndex = 1;                            // // 게임 이미지 인덱스 (1~6)
    public Camera targetCamera;                          // 캡처할 카메라
    public Rect captureRect = new Rect(0, 0, 50, 50);    // 캡처할 영역  


    // [ 임시 구현 코드 ]
    //
    // 1. 게임 화면의 일정 부분을 캡처해 Base64 이미지 문자열로 변환
    // 2. TestData 인스턴스에 저장할 새로운 TestResultData 객체 생성
    // 3. 해당 ID에 TestResultData 저장
    //
    void Start()
    {
        CaptureAndSaveImage();
    }

    void CaptureAndSaveImage()
    {
        // targetCamera가 지정되지 않았을 경우 메인 카메라를 사용
        // if (targetCamera == null){ targetCamera = Camera.main; }


        // 이미지 캡처
        string base64Image = GameData.instance.CaptureScreenArea(targetCamera, captureRect);
        TestResultData testResultData;

        // 테스트 결과 데이터가 없으면 새로 생성
        if (!GameData.instance.testdata.TestResults.ContainsKey(testResultId))
        {
            testResultData = new TestResultData();
            GameData.instance.testdata.TestResults[testResultId] = testResultData;
        }
        else
        {
            testResultData = GameData.instance.testdata.TestResults[testResultId];
        }

        // gameIndex에 따라 각 필드에 이미지 저장
        switch (gameIndex)
        {
            case 1: testResultData.Game1Img = base64Image; break;
            case 2: testResultData.Game2Img = base64Image; break;
            case 3: testResultData.Game3Img = base64Image; break;
            case 4: testResultData.Game4Img = base64Image; break;
            case 5: testResultData.Game5Img = base64Image; break;
            case 6: testResultData.Game6Img = base64Image; break;
        }

        // 데이터 저장
        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print("화면 캡처 및 저장 완료");
    }
}
