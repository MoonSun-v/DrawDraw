using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject popup;
    public void OnClick_close() // '창 닫기' 버튼을 클릭하며 호출 되어질 함수
    {
        popup.transform.gameObject.SetActive(false);
    }

    public void OnClick_next() // '다음 게임으로' 버튼을 클릭하며 호출 되어질 함수
    {
        //SceneManager.LoadScene("NextScene"); 
        Debug.Log("다음 게임으로 넘어갑니다.");
    }
}

