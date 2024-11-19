using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestColorClassify : MonoBehaviour
{
    public GameObject[] Shapes;
    public GameObject[] AnswerShapes;

    public GameObject CheckPopup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        CheckPopup.SetActive(false);
        CheckAnswer();
        GoToNextScene();
        // SceneManager.LoadScene("SelectScene");
    }

    private void CheckAnswer()
    {
        int score = 0;  // 점수를 0으로 초기화

        for (int i = 0; i < Shapes.Length; i++)
        {
            if (i >= AnswerShapes.Length)
            {
                Debug.LogWarning("AnswerShapes 배열의 요소가 부족합니다.");
                return;
            }

            BoxCollider2D answerCollider = AnswerShapes[i].GetComponent<BoxCollider2D>();
            if (answerCollider == null)
            {
                Debug.LogWarning("AnswerShapes[" + i + "]에 BoxCollider2D 컴포넌트가 없습니다.");
                return;
            }

            if (IsWithinCollider(Shapes[i], answerCollider))
            {
                Debug.Log("Shape " + i + "이(가) 올바른 박스 콜라이더 안에 있습니다.");
                score++;  // 올바르게 위치한 도형마다 1점 추가
            }
            else
            {
                Debug.Log("Shape " + i + "이(가) 올바른 박스 콜라이더 안에 없습니다.");
            }
        }

        Debug.Log("최종 점수: " + score);  // 최종 점수 출력

        // 점수 저장 
        SaveResults(score);

    }

    // [ 점수값 저장 ] : 기존 만점 4점
    //
    void SaveResults(int _score)
    {
        _score *= 25; // 만점 100으로 세팅 
        if(_score == 0) { _score += 1; }

        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 5)
        {
            Debug.LogWarning("TestResults에 더 이상 저장할 수 없습니다. 최대 키 값은 5입니다.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        TestResultData currentData = GameData.instance.testdata.TestResults[currentKey];
        currentData.Game10Score = _score;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]의 ColorClassify 점수 = {_score} 저장 완료");
    }

    private bool IsWithinCollider(GameObject shape, BoxCollider2D collider)
    {
        Bounds shapeBounds = shape.GetComponent<Collider2D>().bounds;
        return collider.bounds.Intersects(shapeBounds);
    }

    private void GoToNextScene()
    {
        if( GameData.instance.testdata.TestResults.Count > 5 )
        {
            // 마지막 테스트라면 -> 엔딩씬으로 이동
            // ** 스크립트 추가 필요 **
            print("마지막 테스트였으므로, 엔딩씬으로 이동합니다.");
            SceneManager.LoadScene("EndingScene");

            // 엔딩씬 끝낼때 다음 코드 추가 필요 
            // GameData.instance.trainingdata.ClearStage[19] = true; // 맵의 스테이지 아이콘 완료처리 위해서..!
        }
        else
        {
            // 마지막 테스트가 아니라면 -> 선택화면으로 이동 
            SceneManager.LoadScene("MapScene");
        }
    }
}
