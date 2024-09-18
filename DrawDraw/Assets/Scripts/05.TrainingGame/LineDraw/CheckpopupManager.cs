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
    public GameObject line3;

    public GameObject curveline1;
    public GameObject curveline2;
    public GameObject curveline3;

    public GameObject Shapes1;
    public GameObject Shapes2;
    public GameObject Shapes3;

    public void OnClick_result() // 확인창 완성 버튼을 클릭 -> 결과 보여주기
    {

        if (line1.activeSelf && curveline1 != null) // 첫 번째 그림 완성 -> 두 번째 그림 시작
        {
            // 직선 비활성화
            line1.SetActive(false);
            line2.SetActive(false);
            line3.SetActive(false);

            if(curveline1 != null)
            {
                //곡선 활성화
                curveline1.SetActive(true);
                curveline2.SetActive(true);
                curveline3.SetActive(true);
            }
            //그려진 선 모두 지우기
            ClearAllLines();

        }
        else if(curveline1 == null) // 첫번째 그림만 있을 경우
        {
            result_popup.Show(); // 결과 팝업 띄우기
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 == null) // 밑그림 2개
        {
            result_popup.Show(); // 결과 팝업 띄우기
  
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 != null) // 밑그림 3개
        {
            // 직선 비활성화
            curveline1.SetActive(false);
            curveline2.SetActive(false);
            curveline3.SetActive(false);

            //곡선 활성화
            Shapes1.SetActive(true);
            Shapes2.SetActive(true);
            Shapes3.SetActive(true);

            //그려진 선 모두 지우기
            ClearAllLines();

        }
        else if (Shapes1 != null && Shapes1.activeSelf)
        {
            result_popup.Show(); // 결과 팝업 띄우기

        }
        checkPopup.SetActive(false);

    }

    void ClearAllLines()
    {
        DrawLine.ClearAllLines();
    }

}

