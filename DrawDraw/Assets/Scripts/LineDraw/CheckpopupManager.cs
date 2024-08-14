using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CheckpopupManager : MonoBehaviour
{
    public GameObject checkPopup;
    public resultPopupManager result_popup; // PopupManager 스크립트를 참조할 변수
    public DrawLine DrawLine;

    public GameObject line1;
    public GameObject line2;

    public GameObject curveline1;
    public GameObject curveline2;
    public GameObject curveline3;

    public void check()
    {
        transform.gameObject.SetActive(true); // 확인 팝업 창을 화면에 표시
    }

    public void OnClick_result() // 확인창 완성 버튼을 클릭 -> 결과 보여주기
    {
        checkPopup.SetActive(false);
        if (line1.activeSelf)
        {
            // 직선 비활성화
            line1.SetActive(false);
            line2.SetActive(false);

            //곡선 활성화
            curveline1.SetActive(true);
            curveline2.SetActive(true);
            curveline3.SetActive(true);

            //그려진 선 모두 지우기
            ClearAllLines();

        }
        else if (curveline1.activeSelf)
        {
            result_popup.Show(); // 결과 팝업 띄우기
        }

    }

    void ClearAllLines()
    {
        DrawLine.ClearAllLines();
    }

}

