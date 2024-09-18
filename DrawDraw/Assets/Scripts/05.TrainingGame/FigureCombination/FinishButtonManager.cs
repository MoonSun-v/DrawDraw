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
    public GameObject result_popup;

    public Text Text_GameResult; // 게임의 결과를 표시해줄 Text Ui
    public Text ScoreText; // 게임의 결과를 가져올 Text Ui

    public GameResultSO gameResult; // 게임 결과화면 관리 SO
    internal object onClick;

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
        gameResult.score = int.Parse(ScoreText.text);


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

    public void OnResult()
    {
        //Debug.Log("완성 버튼 클릭, 색상 변경한 조각 개수 출력하기");
        DisplayColorPieceCounts();
        Text_GameResult.text = "올바른 위치에 색칠까지 한 조각 개수 : "+ ScoreText.text;

        check_popup.SetActive(false); // 확인 팝업 창을 화면에 표시
        result_popup.SetActive(true); // 확인 팝업 창을 화면에 표시 
        //Debug.Log("완성 버튼 클릭, 색상 변경한 조각 개수 출력하기");
        //DisplayColorPieceCounts();
    }

    // 전체 퍼즐 조각 클론 개수와 색상이 변경된 도형 개수를 출력하는 함수
    void DisplayColorPieceCounts()
    {
        /// 태그로 퍼즐 조각 클론들을 자동으로 찾기
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // 퍼즐 조각 개수 및 밑그림 조각 개수 출력
        int totalPieces = puzzlePieceClones.Length;

        // 변경된 도형 개수를 가져옴 (ShapeColorChanger 스크립트에서)
        int changedPieces = shapeColorChanger != null ? shapeColorChanger.GetChangedShapeCount() : 0;

        ScoreText.text = changedPieces.ToString();  

        // 콘솔에 출력
        Debug.Log($"전체 퍼즐 조각 개수: {totalPieces}");
        Debug.Log($"색상이 변경된 도형 개수: {changedPieces}");
    }

    public void GotoResultScene()
    {
        Debug.Log("완성 버튼 클릭, 결과 씬으로 이동");

    }

}
