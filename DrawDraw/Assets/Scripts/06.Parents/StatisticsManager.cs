using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{
    public void ResultIndexClick(int index)
    {
        switch (index)
        {
            case 1:
                // TestResults[0]~TestResults[4]�� Game1Img �̹������� ���ʴ�� 5���� Image�� ����.
                break;
            
            default:
                Debug.LogWarning("��ȿ���� ���� �ε����Դϴ�.");
                break;
        }
    }
}
