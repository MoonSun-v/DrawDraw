using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Text TestTime;
    public GameObject TestButton;

    int RemainingTestCount;

    // 1. 테스트 횟수 텍스트 업데이트
    // 2. 테스트 횟수에 따라서, 버튼 클릭 비활성화 
    // 
    void Start()
    {
        int CompletedTestCount = GameData.instance.testdata.TestResults.Count; // 지금까지 테스트 진행 횟수 
        int TotalTestCount = GetTotalTestCount(GameData.instance.playerdata.PlayerExp); // 레벨에 따른 총 테스트 횟수


        RemainingTestCount = TotalTestCount - CompletedTestCount; // 현재 가능한 테스트 횟수 
        print("현재 가능한 테스트 횟수 : " + RemainingTestCount);
        if (RemainingTestCount < 0) { RemainingTestCount = 0;  print("테스트 횟수 오류 입니다");  }


        TestTime.text = RemainingTestCount + "회";


        if (RemainingTestCount <= 0) { TestButton.GetComponent<Button>().interactable = false; } 

    }

    // 레벨에 따른 총 테스트 횟수 반환
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
