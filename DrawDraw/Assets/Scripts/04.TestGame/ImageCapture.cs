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
    public int testResultId = 1;                         // 예시로 저장할 결과의 ID
    public Camera targetCamera;                          // 캡처할 카메라 (필요에 따라 인스펙터에서 할당)
    public Rect captureRect = new Rect(0, 0, 50, 50);    // 캡처할 영역   (인스펙터에서 설정 가능)


    // [ 임시 구현 코드 ]
    //
    // 1. 게임 화면의 일정 부분을 캡처해 Base64 이미지 문자열로 변환
    // 2. TestData 인스턴스에 저장할 새로운 TestResultData 객체 생성
    // 3. 해당 ID에 TestResultData 저장
    //
    void Start()
    {
        // targetCamera가 지정되지 않았을 경우 메인 카메라를 사용
        // if (targetCamera == null){ targetCamera = Camera.main; }

        string base64Image = GameData.instance.CaptureScreenArea(targetCamera, captureRect);
        print(base64Image);

        TestResultData testResultData = new TestResultData();
        testResultData.Game1Img = base64Image;

        GameData.instance.testdata.TestResults[testResultId] = testResultData;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

       print("화면 캡처 및 저장 완료");
        
    }

}
