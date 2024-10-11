using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataReset : MonoBehaviour
{
    string path;


    // [ �ÿ��� : ������ ���� ��ư Ŭ�� ��, �۵� ]
    // 
    public void DeleteAllData()
    {
        path = Application.persistentDataPath + "/";

        DeletePlayerData();
        DeleteTrainingData();
        DeleteTestData();

        Debug.Log("���� �÷��̾�,�Ʒ�,�׽�Ʈ ������ ����");

    }


    // �÷��̾� ������ ����
    public void DeletePlayerData()
    {
        string playerFilePath = path + "PlayerDataSave";
        if (File.Exists(playerFilePath))
        {
            File.Delete(playerFilePath);
            // Debug.Log("�÷��̾� ������ ����");
        }
        else
        {
            // Debug.Log("�÷��̾� �����Ͱ� �������� �ʽ��ϴ�.");
        }
    }

    // �Ʒ� ������ ����
    public void DeleteTrainingData()
    {
        string trainingFilePath = path + "TrainingDataSave";
        if (File.Exists(trainingFilePath))
        {
            File.Delete(trainingFilePath);
            // Debug.Log("�Ʒ� ������ ����");
        }
        else
        {
            // Debug.Log("�Ʒ� �����Ͱ� �������� �ʽ��ϴ�.");
        }
    }

    // �׽�Ʈ ������ ����
    public void DeleteTestData()
    {
        string testFilePath = path + "TestDataSave";
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
            // Debug.Log("�׽�Ʈ ������ ����");
        }
        else
        {
            // Debug.Log("�׽�Ʈ �����Ͱ� �������� �ʽ��ϴ�.");
        }
    }
}
