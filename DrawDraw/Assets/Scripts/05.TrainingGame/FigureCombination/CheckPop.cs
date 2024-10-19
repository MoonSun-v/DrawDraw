using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPop : MonoBehaviour
{
    public GameObject check_popup;
    public GameObject check_popup1;

    public void onCheckPop() // 확인창 띄우기
    {
        check_popup.SetActive(true); // 확인 팝업 창을 화면에 표시
    }
    public void CloseCheckPop()
    {
        check_popup.SetActive(false);
    }
    public void onCheckPop1() // 확인창 띄우기
    {
        check_popup1.SetActive(true); // 확인 팝업 창을 화면에 표시
    }
    public void CloseCheckPop1()
    {
        check_popup1.SetActive(false);
    }
}
