using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDataSetting : MonoBehaviour
{
    void Start()
    {

        // �� [ ���� ������ �ε� ] ��
        // ( ���� ������ �� ���� ������ �ε� ���ִ� �κ� )
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();
        GameData.instance.LoadTestData();




        // �� [ ������ �߰� �ϴ� �ڵ� ������ ] �� -----------------------------------------------------------------
        



        // �� [ ������ �߰� ��, �ʿ��� �ڵ� ] �� ------------------------------------------------------------------

        // ���ο� ������ �߰� ���� ��, �ݵ�� Load�� ���� ���� ���־����
        // ( ���� ������Ʈ�� �����͸� �����;��ϱ� ���� )

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
