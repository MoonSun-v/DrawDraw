using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishButtonManager : MonoBehaviour
{
    // 퍼즐 조각에 사용할 태그 (퍼즐 조각 클론에 미리 태그를 지정해야 함)
    public string puzzlePieceTag = "shape";  // Inspector에서 퍼즐 조각에 대한 태그 설정

    private ColorButtonManager shapeColorChanger;

    public GameObject check_popup; // PopupManager 스크립트를 참조할 변수

    //public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui
    public Text ScoreText; // 게임의 결과를 가져올 Text Ui

    public GameResultSO gameResult; // 게임 결과화면 관리 SO
    internal object onClick;

    // 기본 점수 (100점 만점)
    public float maxScore = 100f;

    void Start()
    {
        // ShapeColorChanger 스크립트를 가진 오브젝트를 찾음
        GameObject colorChangerObject = GameObject.Find("ColorManager"); // "ColorManager"는 ColorButtonManager 붙어 있는 오브젝트 이름
        if (colorChangerObject != null)
        {
            shapeColorChanger = colorChangerObject.GetComponent<ColorButtonManager>();
        }
    }

    public void onCheckPop() // 확인창 띄우기
    {
        check_popup.SetActive(true); // 확인 팝업 창을 화면에 표시
    }

    public void OnFinishButtonClick()
    {
        // 점수 저장 -> 결과 화면에서 게임클리어/오버 구분 위해서
        gameResult.score = int.Parse(DisplayColorPieceCounts());

        // 현재 씬 이름 저장 : 결과 창에서 해당 게임으로 돌아오기 위해서 
        gameResult.previousScene = SceneManager.GetActiveScene().name;

        // 결과 화면으로 넘어가기 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "메소드이름", 매개변수 );
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 초 후 실행
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }

    // 전체 퍼즐 조각 클론 개수와 색상이 변경된 도형 개수를 출력하는 함수
    string DisplayColorPieceCounts()
    {
        /// 태그로 퍼즐 조각 클론들을 자동으로 찾기
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // 퍼즐 조각 개수 및 밑그림 조각 개수 출력
        int totalPieces = puzzlePieceClones.Length;

        // 변경된 도형 개수를 가져옴 (ShapeColorChanger 스크립트에서)
        int changedPieces = shapeColorChanger != null ? shapeColorChanger.GetChangedShapeCount() : 0; 

        // 콘솔에 출력
        //Debug.Log($"전체 퍼즐 조각 개수: {totalPieces}");
        //Debug.Log($"색상이 변경된 도형 개수: {changedPieces}");

        // 점수 계산: 변경된 퍼즐 조각 수를 전체 퍼즐 조각 수로 나눈 뒤 100점을 기준으로 점수를 부여
        float score = 0f;
        if (totalPieces > 0)
        {
            score = (changedPieces / (float)totalPieces) * 50f; //100점 기준
        }

        // 현재 ScoreText.text 값이 숫자로 변환 가능한지 확인
        float scoreValue;

        // ScoreText.text 값을 float으로 변환 시도
        if (float.TryParse(ScoreText.text, out scoreValue))
        {
            // 변환에 성공한 경우 새로운 score를 더함
            scoreValue += score;
        }
        else
        {
            // 변환 실패 시 오류 메시지 출력
            Debug.LogError("ScoreText의 값이 숫자로 변환될 수 없습니다: " + ScoreText.text);
        }

        ScoreText.text = scoreValue.ToString("F0");

        // 최종 점수 출력
        Debug.Log("도형 색칠하기 점수(50점 만점): " + score);
        Debug.Log("최종 점수: " + ScoreText.text);

        return ScoreText.text;
    }
}
