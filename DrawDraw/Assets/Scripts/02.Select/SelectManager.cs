using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Text TestTime;
    public GameObject TestButton;

    int RemainingTestCount;

    // 1. �׽�Ʈ Ƚ�� �ؽ�Ʈ ������Ʈ
    // 2. �׽�Ʈ Ƚ���� ����, ��ư Ŭ�� ��Ȱ��ȭ 
    // 
    void Start()
    {
        int CompletedTestCount = GameData.instance.testdata.TestResults.Count; // ���ݱ��� �׽�Ʈ ���� Ƚ�� 
        int TotalTestCount = GetTotalTestCount(GameData.instance.playerdata.PlayerExp); // ������ ���� �� �׽�Ʈ Ƚ��


        RemainingTestCount = TotalTestCount - CompletedTestCount; // ���� ������ �׽�Ʈ Ƚ�� 
        print("���� ������ �׽�Ʈ Ƚ�� : " + RemainingTestCount);
        if (RemainingTestCount < 0) { RemainingTestCount = 0;  print("�׽�Ʈ Ƚ�� ���� �Դϴ�");  }


        TestTime.text = RemainingTestCount + "ȸ";


        if (RemainingTestCount <= 0) { TestButton.GetComponent<Button>().interactable = false; } 

    }

    // ������ ���� �� �׽�Ʈ Ƚ�� ��ȯ
    private int GetTotalTestCount(int playerExp)
    {
        if (playerExp >= 190) return 6;
        else if (playerExp >= 160) return 5; // level 5
        else if (playerExp >= 120) return 4; // level 4
        else if (playerExp >= 80) return 3;  // level 3 
        else if (playerExp >= 40) return 2;  // level 2 
        return 1;                            // level 1
    }
}
