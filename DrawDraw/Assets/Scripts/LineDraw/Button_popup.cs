using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject current_popup; 
    public resultPopupManager result_popup; // PopupManager 스크립트를 참조할 변수

    //int Score; // 선 그리기 게임에서의 최종 점수 

    public void OnClick_close() // '창 닫기' 버튼을 클릭하며 호출 되어질 함수
    {
        current_popup.transform.gameObject.SetActive(false);
    }

   
}

