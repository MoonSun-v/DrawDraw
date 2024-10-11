using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataReset : MonoBehaviour
{
    string path;


    // [ 시연용 : 데이터 삭제 버튼 클릭 시, 작동 ]
    // 
    public void DeleteAllData()
    {
        path = Application.persistentDataPath + "/";

        DeletePlayerData();
        DeleteTrainingData();
        DeleteTestData();

        Debug.Log("기존 플레이어,훈련,테스트 데이터 삭제");

    }


    // 플레이어 데이터 삭제
    public void DeletePlayerData()
    {
        string playerFilePath = path + "PlayerDataSave";
        if (File.Exists(playerFilePath))
        {
            File.Delete(playerFilePath);
            // Debug.Log("플레이어 데이터 삭제");
        }
        else
        {
            // Debug.Log("플레이어 데이터가 존재하지 않습니다.");
        }
    }

    // 훈련 데이터 삭제
    public void DeleteTrainingData()
    {
        string trainingFilePath = path + "TrainingDataSave";
        if (File.Exists(trainingFilePath))
        {
            File.Delete(trainingFilePath);
            // Debug.Log("훈련 데이터 삭제");
        }
        else
        {
            // Debug.Log("훈련 데이터가 존재하지 않습니다.");
        }
    }

    // 테스트 데이터 삭제
    public void DeleteTestData()
    {
        string testFilePath = path + "TestDataSave";
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
            // Debug.Log("테스트 데이터 삭제");
        }
        else
        {
            // Debug.Log("테스트 데이터가 존재하지 않습니다.");
        }
    }
}
