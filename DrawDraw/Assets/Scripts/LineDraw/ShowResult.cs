using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public PopupManager popupManager; // PopupManager 스크립트를 참조할 변수

    public void ShowResultPopup()
    {
        //Debug.Log("결과 보기");
        popupManager.Show(); // 결과 팝업 띄우기
    }
}
