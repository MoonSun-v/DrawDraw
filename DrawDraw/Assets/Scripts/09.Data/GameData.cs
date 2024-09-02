using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//// Json 사용
//   Json은 string 형태로 저장
//   딕셔너리를 지원하지 않아서, 배열로 주고받거나 유틸리티를 직접 만들어 사용한다. 


//   저장하는 방법
//    1. 저장할 데이터 존재
//    2. 데이터를 Json으로 변환
//    3. Json을 외부에 저장


//   불러오는 방법
//    1. 외부에 저장된 Json을 가져옴
//    2. Json을 데이터 형태로 변환
//    3. 불러온 데이터 사용


// --------------------------------------------------------------------------------------------------------------


//// ★ 플레이어 관련 데이터 ------------------------------------------------------------------------------------
//
// PlayerCharacter : 플레이어 캐릭터 ( Dog 는 0 or Cat 은 1 ) 
// PlayerName      : 플레이어 이름
// PlayerExp       : 플레이어 경험치 
//                   40 -> 80 -> 120 -> 160 -> 190(엔딩) : '스테이지 단계 오픈' 및 '테스트 +1회'
//                   훈련용 게임 스테이지 & 테스트 남은 횟수 연결
// TestNum         : 현재까지 진행한 테스트 횟수

public class PlayerData
{
    public bool PlayerCharacter;
    public string PlayerName;
    public int PlayerExp;

    public int TestNum;
}



//// ★ 훈련용 게임 관련 데이터 ---------------------------------------------------------------------------------
//
// ( 훈련용 게임은 총 19개 스테이지 => 배열의 각 0~18번은 순서대로 각 스테이지를 의미한다. )
// ClearStage  : 현재 클리어한 훈련용 게임 스테이지 
// FailNum     : 현재까지 각 스테이지의 실패한 횟수 
//               각 게임 별 실패 횟수 3번 초과 시 정답 행동 유도 후 다음 게임으로 넘어가야함 

public class TrainingData
{
    public bool[] ClearStage = new bool[19];
    public int[] FailNum = new int[19];
}



//// ★ 테스트 관련 데이터 -------------------------------------------------------------------------------------
//
// ( 테스트는 총 5개 스테이지 => 배열의 각 0~4번은 순서대로 각 테스트를 의미한다. )
// TestResults : 각 TestNum에 대한 게임별 점수와 결과 이미지를 저장하는 딕셔너리
//               key의 int는 TestNum이다.
//               TestResultData 클래스 => 테스트 결과 데이터를 저장하는 형태

[Serializable]
public class TestData
{
    public Dictionary<int, TestResultData> TestResults; // = new Dictionary<int, TestResultData>();
}

[Serializable]
public class TestResultData
{
    public string Game1Img ; // 이미지 경로를 문자열로 저장 
    // public string Game2Img = "";
    // public string Game3Img = "";
    // public string Game4Img = "";
    // public string Game5Img = "";
    // public string Game6Img = ""; 
    // public int Game7Score;
    // public int Game8Score;
    // public int Game9Score;
    // public int Game10Score;
}



// --------------------------------------------------------------------------------------------------------------


public class GameData : MonoBehaviour
{

    public static GameData instance;   // 싱글톤

    string Path;
    string PlayerFileName = "PlayerDataSave";
    string TrainingFileName = "TrainingDataSave";
    string TestFileName = "TestDataSave";

    public PlayerData playerdata = new PlayerData();
    public TrainingData trainingdata = new TrainingData();
    public TestData testdata = new TestData();


    private void Awake()
    {
        // 게임 데이터는 싱글톤으로 관리한다.
        #region 싱글톤

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        #endregion


        Path = Application.persistentDataPath + "/";  // 경로
        print("경로생성 " + Path);


        testdata.TestResults = new Dictionary<int, TestResultData>();   // 초기화
    }





    // --------------------------------------------------------------------------------------------------------
    //// ★ 각 데이터의 Save 와 Load 함수 목록 ★ -------------------------------------------------------------
    //
    // Save : 데이터를 json형태로 변환 후 -> 경로에 저장한다. 
    // Load : 저장된 json을 가져와 -> 데이터 형태로 변환 후 -> 불러온 데이터를 각 데이터에 알맞게 덮어씌운다.
    //        생성되어 있는 파일이 없다면 기본 데이터들로 이루어진 파일을 생성해준다. 



    // [ PlayerData ]-------------------------------------------------------------------------------------------

    public void SavePlayerData()
    {
        string data = JsonUtility.ToJson(playerdata);     
        File.WriteAllText(Path + PlayerFileName, data);   
        print("플레이어 데이터 저장 완료");
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Path + PlayerFileName))
        {                                                             
            string data = File.ReadAllText(Path + PlayerFileName);    
            playerdata = JsonUtility.FromJson<PlayerData>(data);     
            print("플레이어 데이터 불러오기 완료");
        }
        else
        {
            SavePlayerData();                                         
            print("기본 플레이어 데이터 생성 완료");
        }
    }


    // [ TrainingData ]-------------------------------------------------------------------------------------------

    public void SaveTrainingData()
    {
        string data = JsonUtility.ToJson(trainingdata);
        File.WriteAllText(Path + TrainingFileName, data);
        print("훈련 데이터 저장 완료");
    }
    public void LoadTrainingData()
    {
        if (File.Exists(Path + TrainingFileName))
        {
            string data = File.ReadAllText(Path + TrainingFileName);
            trainingdata = JsonUtility.FromJson<TrainingData>(data);
            print("훈련 데이터 불러오기 완료");
        }
        else
        {
            SaveTrainingData();
            print("기본 훈련 데이터 생성 완료");
        }
    }


    // [ TestData ]-------------------------------------------------------------------------------------------
    // 딕셔너리 사용을 위해 DictionaryJsonUtility를 활용한다. 

    public void SaveTestData()
    {
        string data = DictionaryJsonUtility.ToJson(testdata.TestResults, true);
        File.WriteAllText(Path + TestFileName, data);
        print("테스트 데이터 저장 완료");
    }
    public void LoadTestData()
    {
        if (File.Exists(Path + TestFileName))
        {                                                                                             
            string data = File.ReadAllText(Path + TestFileName);
            testdata.TestResults = DictionaryJsonUtility.FromJson<int, TestResultData>(data);     
            print("테스트 데이터 불러오기 완료");
        }
        else
        {
            SaveTestData();                                                                       
            print("기본 테스트 데이터 생성 완료");
        }
    }







    // --------------------------------------------------------------------------------------------------------
    //// ★ 이미지 캡쳐 및 저장 관련 메소드 모음 ★ -----------------------------------------------------------
    //                                               * 이미지 불러오는 메소드는 다른 스크립트에 있음 
    //
    // Base64 인코딩을 사용해 이미지를 문자열로 변환한 후 -> JSON에 포함 하는 방식 사용
    //
    // 1. 화면 일정 부분 캡쳐
    // 2. 이미지 인코딩 (텍스처 -> Base64)
    // 3. 이미지 디코딩 (Base64 -> 텍스처)
    //



    // ★ [특정 영역을 캡처해 Base64 문자열로 변환 ]
    //
    // - cam         : 캡처할 화면을 렌더링할 카메라
    // - captureRect : 캡처할 영역을 정의하는 사각형 (화면의 왼쪽 아래 기준)
    //
    // 1. RenderTexture 설정 : RenderTexture(캡처 영역의 너비, 높이, 깊이 버퍼)
    //                         - 깊이 버퍼 : 비트 깊이 = 이미지 품질 영향 (16 : 가장 저화질)
    //                         카메라의 렌더링 결과를 RenderTexture에 출력
    // 2. cam.Render()       : 카메라가 즉시 씬을 렌더링. 렌더링된 결과는 RenderTexture(rt)에 담김
    // 3. RenderTexture- > Texture2D로 복사
    // 4. 메모리 해제
    // 5. Texture2D를 Base64 문자열로 변환
    //
    public string CaptureScreenArea(Camera cam, Rect captureRect)
    {
        RenderTexture rt = new RenderTexture((int)captureRect.width, (int)captureRect.height, 16);
        cam.targetTexture = rt;  
        
        cam.Render();

        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)captureRect.width, (int)captureRect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        screenShot.Apply();

        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        return TextureToBase64(screenShot);
    }


    // ★ [ Texture2D를 Base64 문자열로 변환 ]
    // 
    // - PNG로 인코딩
    // 
    public string TextureToBase64(Texture2D texture)
    {
        byte[] imageBytes = texture.EncodeToPNG();
        return Convert.ToBase64String(imageBytes);
    }




}
