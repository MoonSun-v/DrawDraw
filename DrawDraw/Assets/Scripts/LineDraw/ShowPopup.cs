using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopup : MonoBehaviour
{
    public CheckpopupManager CheckpopupManager; // PopupManager 스크립트를 참조할 변수

    public void ShowCheckPopup()
    {
        CheckpopupManager.check(); // 결과 팝업 띄우기
    }
}
