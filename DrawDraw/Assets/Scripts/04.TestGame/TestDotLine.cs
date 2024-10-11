using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestDotLine : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 MousePosition;

    // 점의 개수 : 30
    private int TotalDot = 30;

    private float DotCount;          // 충돌한 점의 개수
    private int Score;               // 총 점수

    public GameObject CheckPopup;    // 확인 팝업 창

    public Draw draw;


    private void Awake()
    {
        mainCamera = Camera.main;    // "Maincamera" 태그를 가지고 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
    }


    void Update()
    {
        // 마우스 누르고 있는 동안 
        if (Input.GetMouseButton(0))
        {
            MousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);             // 마우스 클릭 위치 값 -> 월드 좌표 위치 값 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                       // 충돌이 있다면 
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            // 해당 오브젝트 콜리더 비활성화 (충돌 1번만 발생시키기 위해)

                DotCount += 1;

            }

        }
    }


    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }


    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }


    // 팝업 : 완성이야 
    public void NextBtn()
    {
        print("충돌한 점의 개수 = " + DotCount);
        Score = (int)((DotCount / TotalDot) * 100);         // 소수점 이하는 내림 
        print("퍼센트 = " + Score + "%");

        SaveResults(Score);

        NextGame();
    }

    // [ 점수값 저장 ]
    void SaveResults(int _score)
    {
        if (_score == 0) { _score += 1; }

        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 4)
        {
            Debug.LogWarning("TestResults에 더 이상 저장할 수 없습니다. 최대 키 값은 4입니다.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        TestResultData currentData = GameData.instance.testdata.TestResults[currentKey];
        currentData.Game8Score = _score;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]의 DotLine 점수 = {_score} 저장 완료");
    }

    void NextGame()
    {
        SceneManager.LoadScene("Test_ShapesClassifyScene");
    }


}
