using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//// Json ���
//   Json�� string ���·� ����
//   ��ųʸ��� �������� �ʾƼ�, �迭�� �ְ�ްų� ��ƿ��Ƽ�� ���� ����� ����Ѵ�. 


//   �����ϴ� ���
//    1. ������ ������ ����
//    2. �����͸� Json���� ��ȯ
//    3. Json�� �ܺο� ����


//   �ҷ����� ���
//    1. �ܺο� ����� Json�� ������
//    2. Json�� ������ ���·� ��ȯ
//    3. �ҷ��� ������ ���


// --------------------------------------------------------------------------------------------------------------


//// �� �÷��̾� ���� ������ ------------------------------------------------------------------------------------
//
// PlayerCharacter : �÷��̾� ĳ���� ( Dog �� 0 or Cat �� 1 ) 
// PlayerName      : �÷��̾� �̸�
// PlayerExp       : �÷��̾� ����ġ 
//                   40 -> 80 -> 120 -> 160 -> 190(����) : '�������� �ܰ� ����' �� '�׽�Ʈ +1ȸ'
//                   �Ʒÿ� ���� �������� & �׽�Ʈ ���� Ƚ�� ����
// TestNum         : ������� ������ �׽�Ʈ Ƚ��

public class PlayerData
{
    public bool PlayerCharacter;
    public string PlayerName;
    public int PlayerExp;

    public int TestNum;
}



//// �� �Ʒÿ� ���� ���� ������ ---------------------------------------------------------------------------------
//
// ( �Ʒÿ� ������ �� 19�� �������� => �迭�� �� 0~18���� ������� �� ���������� �ǹ��Ѵ�. )
// ClearStage  : ���� Ŭ������ �Ʒÿ� ���� �������� 
// FailNum     : ������� �� ���������� ������ Ƚ�� 
//               �� ���� �� ���� Ƚ�� 3�� �ʰ� �� ���� �ൿ ���� �� ���� �������� �Ѿ���� 

public class TrainingData
{
    public bool[] ClearStage = new bool[19];
    public int[] FailNum = new int[19];
}



//// �� �׽�Ʈ ���� ������ -------------------------------------------------------------------------------------
//
// ( �׽�Ʈ�� �� 5�� �������� => �迭�� �� 0~4���� ������� �� �׽�Ʈ�� �ǹ��Ѵ�. )
// TestResults : �� TestNum�� ���� ���Ӻ� ������ ��� �̹����� �����ϴ� ��ųʸ�
//               key�� int�� TestNum�̴�.
//               TestResultData Ŭ���� => �׽�Ʈ ��� �����͸� �����ϴ� ����

[Serializable]
public class TestData
{
    public Dictionary<int, TestResultData> TestResults; // = new Dictionary<int, TestResultData>();
}

[Serializable]
public class TestResultData
{
    public string Game1Img ; // �̹��� ��θ� ���ڿ��� ���� 
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

    public static GameData instance;   // �̱���

    string Path;
    string PlayerFileName = "PlayerDataSave";
    string TrainingFileName = "TrainingDataSave";
    string TestFileName = "TestDataSave";

    public PlayerData playerdata = new PlayerData();
    public TrainingData trainingdata = new TrainingData();
    public TestData testdata = new TestData();


    private void Awake()
    {
        // ���� �����ʹ� �̱������� �����Ѵ�.
        #region �̱���

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


        Path = Application.persistentDataPath + "/";  // ���
        print("��λ��� " + Path);


        testdata.TestResults = new Dictionary<int, TestResultData>();   // �ʱ�ȭ
    }





    // --------------------------------------------------------------------------------------------------------
    //// �� �� �������� Save �� Load �Լ� ��� �� -------------------------------------------------------------
    //
    // Save : �����͸� json���·� ��ȯ �� -> ��ο� �����Ѵ�. 
    // Load : ����� json�� ������ -> ������ ���·� ��ȯ �� -> �ҷ��� �����͸� �� �����Ϳ� �˸°� ������.
    //        �����Ǿ� �ִ� ������ ���ٸ� �⺻ �����͵�� �̷���� ������ �������ش�. 



    // [ PlayerData ]-------------------------------------------------------------------------------------------

    public void SavePlayerData()
    {
        string data = JsonUtility.ToJson(playerdata);     
        File.WriteAllText(Path + PlayerFileName, data);   
        print("�÷��̾� ������ ���� �Ϸ�");
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Path + PlayerFileName))
        {                                                             
            string data = File.ReadAllText(Path + PlayerFileName);    
            playerdata = JsonUtility.FromJson<PlayerData>(data);     
            print("�÷��̾� ������ �ҷ����� �Ϸ�");
        }
        else
        {
            SavePlayerData();                                         
            print("�⺻ �÷��̾� ������ ���� �Ϸ�");
        }
    }


    // [ TrainingData ]-------------------------------------------------------------------------------------------

    public void SaveTrainingData()
    {
        string data = JsonUtility.ToJson(trainingdata);
        File.WriteAllText(Path + TrainingFileName, data);
        print("�Ʒ� ������ ���� �Ϸ�");
    }
    public void LoadTrainingData()
    {
        if (File.Exists(Path + TrainingFileName))
        {
            string data = File.ReadAllText(Path + TrainingFileName);
            trainingdata = JsonUtility.FromJson<TrainingData>(data);
            print("�Ʒ� ������ �ҷ����� �Ϸ�");
        }
        else
        {
            SaveTrainingData();
            print("�⺻ �Ʒ� ������ ���� �Ϸ�");
        }
    }


    // [ TestData ]-------------------------------------------------------------------------------------------
    // ��ųʸ� ����� ���� DictionaryJsonUtility�� Ȱ���Ѵ�. 

    public void SaveTestData()
    {
        string data = DictionaryJsonUtility.ToJson(testdata.TestResults, true);
        File.WriteAllText(Path + TestFileName, data);
        print("�׽�Ʈ ������ ���� �Ϸ�");
    }
    public void LoadTestData()
    {
        if (File.Exists(Path + TestFileName))
        {                                                                                             
            string data = File.ReadAllText(Path + TestFileName);
            testdata.TestResults = DictionaryJsonUtility.FromJson<int, TestResultData>(data);     
            print("�׽�Ʈ ������ �ҷ����� �Ϸ�");
        }
        else
        {
            SaveTestData();                                                                       
            print("�⺻ �׽�Ʈ ������ ���� �Ϸ�");
        }
    }







    // --------------------------------------------------------------------------------------------------------
    //// �� �̹��� ĸ�� �� ���� ���� �޼ҵ� ���� �� -----------------------------------------------------------
    //                                               * �̹��� �ҷ����� �޼ҵ�� �ٸ� ��ũ��Ʈ�� ���� 
    //
    // Base64 ���ڵ��� ����� �̹����� ���ڿ��� ��ȯ�� �� -> JSON�� ���� �ϴ� ��� ���
    //
    // 1. ȭ�� ���� �κ� ĸ��
    // 2. �̹��� ���ڵ� (�ؽ�ó -> Base64)
    // 3. �̹��� ���ڵ� (Base64 -> �ؽ�ó)
    //



    // �� [Ư�� ������ ĸó�� Base64 ���ڿ��� ��ȯ ]
    //
    // - cam         : ĸó�� ȭ���� �������� ī�޶�
    // - captureRect : ĸó�� ������ �����ϴ� �簢�� (ȭ���� ���� �Ʒ� ����)
    //
    // 1. RenderTexture ���� : RenderTexture(ĸó ������ �ʺ�, ����, ���� ����)
    //                         - ���� ���� : ��Ʈ ���� = �̹��� ǰ�� ���� (16 : ���� ��ȭ��)
    //                         ī�޶��� ������ ����� RenderTexture�� ���
    // 2. cam.Render()       : ī�޶� ��� ���� ������. �������� ����� RenderTexture(rt)�� ���
    // 3. RenderTexture- > Texture2D�� ����
    // 4. �޸� ����
    // 5. Texture2D�� Base64 ���ڿ��� ��ȯ
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


    // �� [ Texture2D�� Base64 ���ڿ��� ��ȯ ]
    // 
    // - PNG�� ���ڵ�
    // 
    public string TextureToBase64(Texture2D texture)
    {
        byte[] imageBytes = texture.EncodeToPNG();
        return Convert.ToBase64String(imageBytes);
    }




}
