using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour
{
    public GameObject linePrefab; // 선을 그리기 위한 프리팹
    public float lineWidth = 0.15f; // 선의 너비

    public GameObject TestDrawManager;

    private LineRenderer currentLineRenderer; // 현재 그려지는 선의 라인 렌더러
    private Vector2 previousPosition; // 이전 위치
    private Vector2 previousPosition2; // 이전전 위치

    private List<GameObject> lines = new List<GameObject>();

    private bool isDrawing = false;

    public void Update()
    {
        if (TestDrawManager == null)
        {
            Debug.LogError("TestDrawManager is not assigned!");
            return;
        }

        // 그리기 영역 안에 있어야 그리기 가능
        if (TestDrawManager.GetComponent<TestDrawManager>().DrawActivate)
        {
            // 마우스 클릭 또는 터치가 시작되면 새로운 선을 그린다.
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CreateNewLine();
                isDrawing = true; // 선 그리기 시작
            }
            // 선을 그리는 중일 때 행동
            else if (isDrawing && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
            {
                Vector2 currentPosition = GetInputPosition();

                if (Vector2.Distance(currentPosition, previousPosition) > 0.1f) // 선을 그리기 위한 최소한의 거리
                {
                    AddPointToLine(currentPosition);
                }
            }
            // 마우스 클릭 또는 터치가 끝나면 그리기 종료
            else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                currentLineRenderer = null; // 현재 그리는 선 종료
                isDrawing = false; // 선 그리기 종료
            }
        }
        else
        {
            previousPosition = previousPosition2; // 이전 위치 초기화
        }
    }

    void CreateNewLine()
    {
        // 새로운 선을 그릴 GameObject 생성
        GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLineRenderer = newLine.GetComponent<LineRenderer>();
        currentLineRenderer.sortingOrder = 1;

        // 생성된 선을 리스트에 추가
        lines.Add(newLine);

        // 라인 렌더러 설정
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.positionCount = 0; // 선의 위치를 초기화합니다.

        // 현재 위치를 이전 위치로 설정
        previousPosition = GetInputPosition();
        AddPointToLine(previousPosition);
    }

    void AddPointToLine(Vector2 newPoint)
    {
        // 새로운 점을 선에 추가
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, newPoint);

        // 현재 위치를 이전 위치로 설정
        previousPosition2 = previousPosition;
        previousPosition = newPoint;

    }

    public void ClearAllLines()
    {
        // 리스트에 저장된 모든 선(GameObject)을 삭제
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        // 리스트를 초기화
        lines.Clear();
    }

    // 마우스 클릭 또는 터치의 위치를 세계 좌표로 변환하여 반환
    Vector2 GetInputPosition()
    {
        Vector2 inputPosition = Vector2.zero;
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }

        return Camera.main.ScreenToWorldPoint(inputPosition);
    }
}
