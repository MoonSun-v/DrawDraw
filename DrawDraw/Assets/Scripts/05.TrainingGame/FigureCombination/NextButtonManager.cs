using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonManager : MonoBehaviour
{
    public CanvasGroup shapeButtonGroup;  // 도형 버튼 그룹
    public CanvasGroup colorButtonGroup;  // 색칠 버튼 그룹

    public GameObject[] prefabsToDisable; // 비활성화할 프리팹의 배열

    public GameObject completeButtonObject;  // Inspector에서 "완성" 버튼 오브젝트를 할당

    public GameObject check_popup; // PopupManager 스크립트를 참조할 변수

    // 퍼즐 조각에 사용할 태그 (퍼즐 조각 클론에 미리 태그를 지정해야 함)
    public string puzzlePieceTag = "shape";  // Inspector에서 퍼즐 조각에 대한 태그 설정

    // 밑그림 조각들이 묶여 있는 빈 오브젝트 (부모 오브젝트)
    public GameObject basePieceGroup;

    // 허용 오차 설정
    private float positionTolerance = 0.1f;
    private float rotationTolerance = 5f;
    private float localScaleTolerance = 0.2f;

    // 일치한 퍼즐 조각 개수
    private int matchingPieceCount = 0;

    void Start()
    {
        // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
        //SetCanvasGroupActive(shapeButtonGroup, true);
        //SetCanvasGroupActive(colorButtonGroup, false);
    }

    public void OnNextButtonClick()
    {
        CalculateMatches();

        // 처음 시작할 때 도형 버튼은 보이고, 색칠 버튼은 보이지 않도록 설정
        SetCanvasGroupActive(shapeButtonGroup, false);
        SetCanvasGroupActive(colorButtonGroup, true);

        // 각 프리팹의 모든 인스턴스에 있는 모든 스크립트를 비활성화합니다.
        foreach (GameObject prefab in prefabsToDisable)
        {
            DisableScriptsOnPrefabInstances(prefab);
        }

        //isButtonClicked = true;  // 버튼이 눌렸음을 표시
        //CalculateMatches();
        gameObject.SetActive(false);  // "다음" 버튼 오브젝트 비활성화
        completeButtonObject.SetActive(true);  // "완성" 버튼 오브젝트 활성화

        check_popup.SetActive(false);
    }

    private void DisableScriptsOnPrefabInstances(GameObject prefab)
    {
        // 특정 프리팹의 모든 인스턴스를 찾습니다.
        GameObject[] allInstances = GameObject.FindGameObjectsWithTag(prefab.tag);
        foreach (GameObject instance in allInstances)
        {
            // 해당 인스턴스에 붙어 있는 모든 MonoBehaviour 스크립트를 비활성화합니다.
            MonoBehaviour[] scripts = instance.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
                //Debug.Log($"{instance.name}의 {script.GetType().Name} 스크립트가 비활성화되었습니다.");
            }
        }
    }

    void SetCanvasGroupActive(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }

    // 일치 도형 개수 계산

    public float maxScore = 100f;    // 기본 점수 (100점 만점)
    public Text ScoreText; // 게임의 결과를 가져올 Text Ui

    void CalculateMatches()
    {
        /// 태그로 퍼즐 조각 클론들을 자동으로 찾기
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // 퍼즐 조각 개수 및 밑그림 조각 개수 출력
        int puzzlePieceCount = puzzlePieceClones.Length;  // 현재 씬에 존재하는 퍼즐 조각 클론들의 총 개수 계산
        int basePieceCount = basePieceGroup.transform.childCount;  // 밑그림 조각들의 개수 계산 (basePieceGroup의 자식 개수)

        //Debug.Log("퍼즐 조각 개수: " + puzzlePieceCount);  // 퍼즐 조각 개수를 디버그 출력
        //Debug.Log("밑그림 조각 개수: " + basePieceCount);  // 밑그림 조각 개수를 디버그 출력

        // 일치한 퍼즐 조각 개수 초기화
        matchingPieceCount = 0;  // 퍼즐과 밑그림이 일치한 조각의 개수를 저장할 변수를 0으로 초기화

        // 각 퍼즐 조각 클론과 밑그림 조각 간의 일치도 비교
        foreach (GameObject puzzlePiece in puzzlePieceClones)  // 모든 퍼즐 조각을 순회하며
        {
            // 퍼즐 조각과 밑그림 조각 일치 여부 비교
            CheckMatch(puzzlePiece);  // 각 퍼즐 조각과 밑그림 조각들 사이의 일치 여부를 확인하는 함수 호출
        }

        // 일치한 퍼즐 조각 개수 출력
        //Debug.Log("일치한 퍼즐 조각 개수: " + matchingPieceCount);  // 일치한 퍼즐 조각의 최종 개수를 디버그 출력

        float score;
        // 도형이 없을 경우 0점 반환
        if (basePieceCount == 0 || puzzlePieceCount == 0)
        {
            //Debug.Log("퍼즐이나 밑그림 조각이 없으므로 0점입니다.");
            score = 0;
        }
        else
        {
            // 일치한 퍼즐 조각 개수를 100점 만점으로 환산
            score = (matchingPieceCount / (float)basePieceCount) * 50f;
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
        Debug.Log("도형 맞추기 점수(50점 만점): " + ScoreText.text);
    }

    // 퍼즐 조각과 여러 밑그림 조각 간의 일치 여부 비교
    void CheckMatch(GameObject puzzlePiece)
    {
        // 일치 여부를 저장할 변수
        bool hasMatched = false;  // 퍼즐 조각과 일치하는 밑그림 조각을 찾았는지 여부를 저장할 변수를 false로 초기화

        // 부모 오브젝트(basePieceGroup)의 자식 오브젝트들을 순회하며 비교
        foreach (Transform basePieceTransform in basePieceGroup.transform)  // 밑그림 조각들의 부모 오브젝트의 자식들(밑그림 조각들)을 순회
        {
            GameObject basePiece = basePieceTransform.gameObject;  // 현재 밑그림 조각을 가져옴

            // 1. 위치 비교
            bool isPositionMatch = Vector3.Distance(puzzlePiece.transform.position, basePiece.transform.position) < positionTolerance;  // 퍼즐 조각과 밑그림 조각의 위치가 허용 오차 내에 있는지 확인
            //Debug.Log(basePiece+" isPositionMatch :" + isPositionMatch);
            
            // 2. 회전 비교
            bool isRotationMatch = Mathf.Abs(Quaternion.Angle(puzzlePiece.transform.rotation, basePiece.transform.rotation)) < rotationTolerance;  // 퍼즐 조각과 밑그림 조각의 회전 각도가 허용 오차 내에 있는지 확인
            //Debug.Log(basePiece + " isRotationMatch :" + isRotationMatch);
            
            // 3. 크기 비교
            //bool isScaleMatch = puzzlePiece.transform.localScale == basePiece.transform.localScale;  // 퍼즐 조각과 밑그림 조각의 크기가 같은지 확인
            // 두 퍼즐 조각과 기준 조각의 localScale 비교
            bool isScaleMatch = Mathf.Abs(puzzlePiece.transform.localScale.x - basePiece.transform.localScale.x) < localScaleTolerance &&
                                Mathf.Abs(puzzlePiece.transform.localScale.y - basePiece.transform.localScale.y) < localScaleTolerance &&
                                Mathf.Abs(puzzlePiece.transform.localScale.z - basePiece.transform.localScale.z) < localScaleTolerance;     
          
            // 4. 모든 조건이 일치하면 매치로 간주
            if (isPositionMatch && isRotationMatch && isScaleMatch)  // 위치, 회전, 크기 비교가 모두 일치할 경우
            {
                hasMatched = true;  // 매칭된 것으로 간주하고, 일치 플래그를 true로 설정
                break;  // 일치하는 밑그림 조각을 찾았으므로 더 이상 비교하지 않고 반복문 탈출
            }
        }

        // 퍼즐 조각이 일치한 경우 개수 증가
        if (hasMatched)  // 일치한 퍼즐 조각이 존재하는 경우
        {
            matchingPieceCount++;  // 일치한 퍼즐 조각의 개수를 1 증가시킴
        }
    }

}