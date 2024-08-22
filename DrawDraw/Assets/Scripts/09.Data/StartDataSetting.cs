using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDataSetting : MonoBehaviour
{
    void Start()
    {

        // ★ [ 게임 데이터 로드 ] ★
        // ( 게임 시작할 때 게임 데이터 로드 해주는 부분 )
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();
        GameData.instance.LoadTestData();




        // ★ [ 데이터 추가 하는 코드 모음집 ] ★ -----------------------------------------------------------------
        



        // ★ [ 데이터 추가 후, 필요한 코드 ] ★ ------------------------------------------------------------------

        // 새로운 데이터 추가 저장 후, 반드시 Load도 같이 진행 해주어야함
        // ( 새로 업데이트된 데이터를 가져와야하기 때문 )

        /*
        GameData.instance.SavePlayerData();
        GameData.instance.SaveTrainingData();
        GameData.instance.SaveTestData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();
        GameData.instance.LoadTestData();
        */


    }

} 
