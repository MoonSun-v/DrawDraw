using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchDraw: MonoBehaviour
{
    public Camera m_camera;

    // [ 그림도구 관련 변수 ]
    public GameObject brush;                      // 브러시 프리팹 
    private Color lineColor;
    private LineRenderer lineRenderer;
    private LineRenderer currentLineRenderer;     // 현재 선 그리기용 LineRenderer

    private Vector2 lastPos;                      // 마지막으로 그려진 점의 위치를 저장 

    // [ 그리기 관련 변수 ]
    [SerializeField, Range(0.0f, 2.0f)]
    private float width = 0.66f;                  // 선 굵기 조절 
    
    public ScratchManager Scratch;

    // [ 색상 선택 관련 변수 ]
    public GameObject previousButton;                 // 이전에 클릭된 버튼을 추적하기 위한 변수
    public Vector3 previousButtonOriginalPosition;    // 이전 버튼의 원래 위치를 저장
    private const int CrayonMove = 90;                // 버튼 이동 거리

    // [ 그려진 선 추적 및 관리 ]
    public List<GameObject> lineRenderers = new List<GameObject>();     // 생성된 LineRenderer를 추적하기 위한 리스트
    public GameObject ScratchBlack;
    public bool isStartDraw;
    public bool isSelectColor;



    private void Start()
    {
        lineRenderer = brush.GetComponent<LineRenderer>();

    }


    private void Update()
    {
        if (previousButton != null)
        {
            Drawing();
        }  
    }


    // ★ [ 그리기 작업 수행 ] ★
    // 
    void Drawing()
    {
        if (Input.GetMouseButtonDown(0))     // 눌렀을 때 한번만 (누르고 있어도 한번..!)
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))    // 누르고 있는 경우
        {
            PointToMousePos();
        }
        else if (Input.GetMouseButtonUp(0))  
        {
            FinishLineRenderer();
        }

    }


    // ★ [ 브러시 생성, 초기화 ]
    // 
    // - 선 굵기 항상 동일하게 설정
    // - 마우스 위치를 이용해 브러시 LineRenderer의 시작과 끝 지점 설정
    //   ( 라인 렌더러를 시작하려면 2개 점이 있어야 하니까 )
    // - 생성된 LineRenderer 객체를 리스트에 추가
    // 
    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        currentLineRenderer.startWidth = currentLineRenderer.endWidth = width;     

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);        
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

        lineRenderers.Add(brushInstance);    
    }



    // ★ [ 마우스 위치 따라 선 그리기 ]
    //
    // ( 마우스 위치가 변경되었을 때만 새로운 점 추가 )
    // 
    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(lastPos, mousePos) > 0.1f)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }



    // ★ [ 선에 새로운 점 추가 ]
    //
    // 선의 positionCount 증가시키고 새로운 점 위치 설정 
    // 
    void AddAPoint(Vector2 pointPos)
    {
        if (!currentLineRenderer)
        {
            print("currentLineRenderer가 null 입니다.");
            return;
        }

        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }


    // ★ [ 현재 선 그리기 종료 ] 
    //
    // isStartDraw : 설명 문구 업데이트 위한 작업
    //
    public void FinishLineRenderer()
    {
        currentLineRenderer = null;    

        if (!isStartDraw)              
        {
            isStartDraw = true;
            print("isStartDraw가 true가 되었습니다");
        }
    }


    // ★ [ LineRenderer 색상 설정 ]
    //
    // isSelectColor : 설명 문구 업데이트 위한 작업
    //
    public void SetLineColor()
    {
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        if(!isSelectColor)             
        {
            isSelectColor = true;
            print("isSelectColor가 true가 되었습니다");
        }
    }


    // ★ [ 생성된 모든 LineRenderer 객체를 삭제 ] : '처음부터' 버튼 클릭
    // 
    public void ClearAllLineRenderers()
    {
        if (!ScratchBlack.activeSelf)        
        {
            foreach (GameObject lineRendererObject in lineRenderers)
            {
                Destroy(lineRendererObject);
            }
            lineRenderers.Clear();           
        }
        
    }


    public bool iscurrentLineRenderer()
    {
        return currentLineRenderer != null;
    }


    // ★ [ 브러시 프리팹 색상 변경 ]
    //
    // 1. 이전 색상 버튼 원위치
    // 2. 색상 적용
    // 3. 현재 색상 버튼의 원래 위치 저장
    // 4. 이전 버튼을 현재 버튼으로 업데이트
    //
    public void SelectColorButton(GameObject crayon, Color color)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = color;
        SetLineColor();

        RectTransform rt = crayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = crayon;
    }


    #region 브러시 색상 변경 메서드 모음

    public void ColorRedButton(GameObject RedCrayon)
    {
        SelectColorButton(RedCrayon, Color.red);
    }

    public void ColorOrangeButton(GameObject OrangeCrayon)
    {
        SelectColorButton(OrangeCrayon, new Color(1f, 0.5f, 0f));
    }

    public void ColorYellowButton(GameObject YellowCrayon)
    {
        SelectColorButton(YellowCrayon, Color.yellow);
    }

    public void ColorGreenButton(GameObject GreenCrayon)
    {
        SelectColorButton(GreenCrayon, new Color(0f, 0.392f, 0f));
    }

    public void ColorSkyBlueButton(GameObject SkyBlueCrayon)
    {
        SelectColorButton(SkyBlueCrayon, new Color(0.529f, 0.808f, 0.922f));
    }

    public void ColorBlueButton(GameObject BlueCrayon)
    {
        SelectColorButton(BlueCrayon, Color.blue);
    }

    public void ColorPurpleButton(GameObject PurpleCrayon)
    {
        SelectColorButton(PurpleCrayon, new Color(0.859f, 0.439f, 0.576f));
    }

    #endregion

}