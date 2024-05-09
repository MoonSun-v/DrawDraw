using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer; // 현재 선 그리는 데 사용되는 LineRenderer 컴포넌트 저장

    Vector2 lastPos; // 마지막으로 그려진 점의 위치를 저장 

    [SerializeField, Range(0.0f, 2.0f)]
    private float width; // 선 굵기 조절 

    private void Update()
    {
        Drawing();
    }

    void Drawing()
    {

        if (Input.GetMouseButtonDown(0)) // 눌렀을 때 한번만 (누르고 있어도 한번..!)
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0)) // 누르고 있는 경우
        {
            PointToMousePos();
        }
        else if (Input.GetMouseButtonUp(0)) // 떼었을 때
        {
            currentLineRenderer = null; // 현재 그리는 선 종료
        }
    }

    // 브러시를 생성, 초기화
    void CreateBrush()
    {
        // 마우스 위치를 이용해 브러시의 LineRenderer의 시작과 끝 지점 설정

        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = currentLineRenderer.endWidth = width; // 선 굵기 항상 동일하게 

        // 라인 렌더러를 시작하려면 2개 점이 있어야 하니까
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    // 선에 새로운 점 추가 : 선의 positionCount 증가시키고 새로운 점 위치 설정 
    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    // 마우스 위치 따라 선 그리기 (마우스 위치가 변경되었을 때만 새로운 점을 추가)
    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }
}
