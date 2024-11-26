using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMatcher : MonoBehaviour
{
    // 퍼즐 조각에 사용할 태그 (퍼즐 조각 클론에 미리 태그를 지정해야 함)
    public string puzzlePieceTag = "shape";  // Inspector에서 퍼즐 조각에 대한 태그 설정

    // 밑그림 조각들이 묶여 있는 빈 오브젝트 (부모 오브젝트)
    public GameObject basePieceGroup;

    // 허용 오차 설정
    public float positionTolerance = 0.1f;
    public float rotationTolerance = 5f;

    // UI 버튼
    public Button calculateButton;  // Inspector에서 버튼을 할당

    void Start()
    {
        // 버튼 클릭 이벤트에 함수 연결
        //calculateButton.onClick.AddListener(CalculateMatches);
    }

    // 버튼 클릭 시 호출되는 함수
    void CalculateMatches()
    {
        // 태그로 퍼즐 조각 클론들을 자동으로 찾기
        GameObject[] puzzlePieceClones = GameObject.FindGameObjectsWithTag(puzzlePieceTag);

        // 각 퍼즐 조각 클론과 밑그림 조각 간의 일치도 비교
        foreach (GameObject puzzlePiece in puzzlePieceClones)
        {
            // 퍼즐 조각과 밑그림 조각 일치 여부 비교
            CheckMatch(puzzlePiece);
        }
    }

    // 퍼즐 조각과 여러 밑그림 조각 간의 일치 여부 비교
    void CheckMatch(GameObject puzzlePiece)
    {
        GameObject bestMatch = null;
        float bestMatchPercentage = 0f;

        // 부모 오브젝트(basePieceGroup)의 자식 오브젝트들을 순회하며 비교
        foreach (Transform basePieceTransform in basePieceGroup.transform)
        {
            GameObject basePiece = basePieceTransform.gameObject;

            // 1. 위치 비교
            bool isPositionMatch = Vector3.Distance(puzzlePiece.transform.position, basePiece.transform.position) < positionTolerance;

            // 2. 회전 비교
            bool isRotationMatch = Mathf.Abs(Quaternion.Angle(puzzlePiece.transform.rotation, basePiece.transform.rotation)) < rotationTolerance;

            // 3. 크기 비교
            bool isScaleMatch = puzzlePiece.transform.localScale == basePiece.transform.localScale;

            // 4. 일치도(정확도) 계산
            float matchPercentage = 0f;
            if (isPositionMatch) matchPercentage += 33.3f;
            if (isRotationMatch) matchPercentage += 33.3f;
            if (isScaleMatch) matchPercentage += 33.3f;

            // 현재 밑그림 조각이 가장 잘 맞는지 확인
            if (matchPercentage > bestMatchPercentage)
            {
                bestMatch = basePiece;
                bestMatchPercentage = matchPercentage;
            }
        }

        // 가장 잘 맞는 밑그림 조각에 대한 결과 출력
        if (bestMatch != null)
        {
            Debug.Log("퍼즐 조각 " + puzzlePiece.name + "가 가장 잘 맞는 밑그림 조각: " + bestMatch.name);
            Debug.Log("일치도: " + bestMatchPercentage + "%");
        }
        else
        {
            Debug.Log("퍼즐 조각 " + puzzlePiece.name + "과 일치하는 밑그림 조각이 없습니다.");
        }
    }
}