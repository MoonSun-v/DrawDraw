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
                // TestResults[0]~TestResults[4]의 Game1Img 이미지들을 차례대로 5가지 Image에 띄운다.
                break;
            
            default:
                Debug.LogWarning("유효하지 않은 인덱스입니다.");
                break;
        }
    }
}
