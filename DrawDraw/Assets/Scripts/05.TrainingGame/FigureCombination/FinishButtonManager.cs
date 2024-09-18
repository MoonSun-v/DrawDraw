using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonManager : MonoBehaviour
{
    // 퍼즐 조각에 사용할 태그 (퍼즐 조각 클론에 미리 태그를 지정해야 함)
    public string puzzlePieceTag = "shape";  // Inspector에서 퍼즐 조각에 대한 태그 설정

    // 밑그림 조각들이 묶여 있는 빈 오브젝트 (부모 오브젝트)
    public GameObject basePieceGroup;

    // 허용 오차 설정
    public float positionTolerance = 0.1f;
    public float rotationTolerance = 5f;

    // 일치한 퍼즐 조각 개수
    private int matchingPieceCount = 0;

    private ColorButtonManager shapeColorChanger;

    void Start()
    {
        CalculateMatches();

        // ShapeColorChanger 스크립트를 가진 오브젝트를 찾음
        GameObject colorChangerObject = GameObject.Find("ColorManager"); // "ColorManager"는 ColorButtonManager 붙어 있는 오브젝트 이름
        if (colorChangerObject != null)
        {
            shapeColorChanger = colorChangerObject.GetComponent<ColorButtonManager>();
        }
    }

    public void OnFinishButtonClick()
    {
        //Debug.Log("완성 버튼 클릭, 색상 변경한 조각 개수 출력하기");
        DisplayColorPieceCounts();
        
        // 확인창 띄우기

    }

    // 버튼 클릭 시 호출되는 함수
    void CalculateMatches()
    {
        /// 태그로 퍼즐 조각 클론들을 자동으로 찾기
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // 퍼즐 조각 개수 및 밑그림 조각 개수 출력
        int puzzlePieceCount = puzzlePieceClones.Length;  // 현재 씬에 존재하는 퍼즐 조각 클론들의 총 개수 계산
        int basePieceCount = basePieceGroup.transform.childCount;  // 밑그림 조각들의 개수 계산 (basePieceGroup의 자식 개수)

        Debug.Log("퍼즐 조각 개수: " + puzzlePieceCount);  // 퍼즐 조각 개수를 디버그 출력
        Debug.Log("밑그림 조각 개수: " + basePieceCount);  // 밑그림 조각 개수를 디버그 출력

        // 일치한 퍼즐 조각 개수 초기화
        matchingPieceCount = 0;  // 퍼즐과 밑그림이 일치한 조각의 개수를 저장할 변수를 0으로 초기화

        // 각 퍼즐 조각 클론과 밑그림 조각 간의 일치도 비교
        foreach (GameObject puzzlePiece in puzzlePieceClones)  // 모든 퍼즐 조각을 순회하며
        {
            // 퍼즐 조각과 밑그림 조각 일치 여부 비교
            CheckMatch(puzzlePiece);  // 각 퍼즐 조각과 밑그림 조각들 사이의 일치 여부를 확인하는 함수 호출
        }

        // 일치한 퍼즐 조각 개수 출력
        Debug.Log("일치한 퍼즐 조각 개수: " + matchingPieceCount);  // 일치한 퍼즐 조각의 최종 개수를 디버그 출력
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

            // 2. 회전 비교
            bool isRotationMatch = Mathf.Abs(Quaternion.Angle(puzzlePiece.transform.rotation, basePiece.transform.rotation)) < rotationTolerance;  // 퍼즐 조각과 밑그림 조각의 회전 각도가 허용 오차 내에 있는지 확인

            // 3. 크기 비교
            bool isScaleMatch = puzzlePiece.transform.localScale == basePiece.transform.localScale;  // 퍼즐 조각과 밑그림 조각의 크기가 같은지 확인
                                                                                                     

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

    // 전체 퍼즐 조각 클론 개수와 색상이 변경된 도형 개수를 출력하는 함수
    void DisplayColorPieceCounts()
    {
        /// 태그로 퍼즐 조각 클론들을 자동으로 찾기
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // 퍼즐 조각 개수 및 밑그림 조각 개수 출력
        int totalPieces = puzzlePieceClones.Length;

        // 변경된 도형 개수를 가져옴 (ShapeColorChanger 스크립트에서)
        int changedPieces = shapeColorChanger != null ? shapeColorChanger.GetChangedShapeCount() : 0;

        // 콘솔에 출력
        Debug.Log($"전체 퍼즐 조각 개수: {totalPieces}");
        Debug.Log($"색상이 변경된 도형 개수: {changedPieces}");
    }
      
}
