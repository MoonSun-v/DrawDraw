using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchDraw: MonoBehaviour
{
    public Camera m_camera;

    //// 그림도구 관련 변수 
    public GameObject brush; // 브러시 프리팹 
    private LineRenderer lineRenderer;
    private Color lineColor;

    LineRenderer currentLineRenderer; // 현재 선 그리는 데 사용되는 LineRenderer 컴포넌트 저장

    Vector2 lastPos; // 마지막으로 그려진 점의 위치를 저장 

    [SerializeField, Range(0.0f, 2.0f)]
    private float width; // 선 굵기 조절 

    public ScratchManager Scratch;

    private void Start()
    {
        lineRenderer = brush.GetComponent<LineRenderer>();

        // 기본 색상은 그레이
        lineColor = Color.gray;
        SetLineColor();
    }

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
            // currentLineRenderer = null; // 현재 그리는 선 종료
            FinishLineRenderer();
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
        if (!currentLineRenderer) return;

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

    public bool iscurrentLineRenderer()
    {
        bool isLineRanderer;
        if(currentLineRenderer)
        {
            isLineRanderer = true;
        }
        else
        {
            isLineRanderer = false;
        }
        return isLineRanderer;
    }

    public void FinishLineRenderer()
    {
        currentLineRenderer = null; // 현재 그리는 선 종료
    }

    public void SetLineColor()
    {
        // LineRenderer의 색상을 설정합니다.
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }


    //// 그림 도구 선택 → 브러시 프리팹의 색상 변경
    
    public void ColorRedButton()
    {
        lineColor = Color.red;
        SetLineColor();
    }

    public void ColorOrangeButton()
    {
        lineColor = new Color(1f, 0.5f, 0f);
        SetLineColor();
    }

    public void ColorYellowButton()
    {
        lineColor = Color.yellow;
        SetLineColor();
    }

    public void ColorGreenButton()
    {
        lineColor = new Color(0f, 0.392f, 0f);
        SetLineColor();
    }

    public void ColorSkyBlueButton()
    {
        lineColor = new Color(0.529f, 0.808f, 0.922f); 
        SetLineColor();
    }

    public void ColorBlueButton()
    {
        lineColor = Color.blue;
        SetLineColor();
    }

    public void ColorPurpleButton()
    {
        lineColor = new Color(0.859f, 0.439f, 0.576f);
        SetLineColor();
    }
}
