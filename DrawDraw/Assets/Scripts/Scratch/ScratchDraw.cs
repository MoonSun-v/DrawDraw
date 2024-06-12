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

    public GameObject previousButton; // 이전에 클릭된 버튼을 추적하기 위한 변수
    public Vector3 previousButtonOriginalPosition; // 이전 버튼의 원래 위치를 저장
    private int CrayonMove = 90;

    public List<GameObject> lineRenderers = new List<GameObject>(); // 생성된 LineRenderer를 추적하기 위한 리스트

    public GameObject ScratchBlack;

    public bool isSelectColor;
    public bool isStartDraw;

    private void Start()
    {
        lineRenderer = brush.GetComponent<LineRenderer>();

        // 기본 색상은 그레이
        // lineColor = Color.gray;
        // SetLineColor();
    }

    private void Update()
    {
        // 아직 색상 선택을 하지 않았으면 그리기 차단
        if (previousButton == null)
        {
            return;
        }

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

        // 생성된 LineRenderer 객체를 리스트에 추가
        lineRenderers.Add(brushInstance);
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
        return currentLineRenderer != null;
    }

    public void FinishLineRenderer()
    {
        currentLineRenderer = null; // 현재 그리는 선 종료

        if (!isStartDraw) // 첫 색칠이 시작되면 
        {
            isStartDraw = true;
            print("isStartDraw가 true가 되었습니다");
        }
    }

    public void SetLineColor()
    {
        // LineRenderer의 색상을 설정합니다.
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        if(!isSelectColor)
        {
            isSelectColor = true;
            print("isSelectColor가 true가 되었습니다");
        }
    }


    // 처음부터 버튼 클릭 시 생성된 모든 LineRenderer 객체를 삭제하는 메서드
    public void ClearAllLineRenderers()
    {
        // ScratchBlack이 비활성화일 때만 삭제 가능
        if (!ScratchBlack.activeSelf)
        {
            foreach (GameObject lineRendererObject in lineRenderers)
            {
                Destroy(lineRendererObject);
            }
            lineRenderers.Clear(); // 리스트 초기화
        }
        
    }

    //// 그림 도구 선택 → 브러시 프리팹의 색상 변경

    public void ColorRedButton(GameObject RedCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = Color.red;
        SetLineColor();

        RectTransform rt = RedCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = RedCrayon;
    }

    public void ColorOrangeButton(GameObject OrangeCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = new Color(1f, 0.5f, 0f);
        SetLineColor();

        RectTransform rt = OrangeCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = OrangeCrayon;
    }

    public void ColorYellowButton(GameObject YellowCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = Color.yellow;
        SetLineColor();

        RectTransform rt = YellowCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = YellowCrayon;
    }

    public void ColorGreenButton(GameObject GreenCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = new Color(0f, 0.392f, 0f);
        SetLineColor();

        RectTransform rt = GreenCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = GreenCrayon;
    }

    public void ColorSkyBlueButton(GameObject SkyBlueCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = new Color(0.529f, 0.808f, 0.922f); 
        SetLineColor();

        RectTransform rt = SkyBlueCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = SkyBlueCrayon;
    }

    public void ColorBlueButton(GameObject BlueCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = Color.blue;
        SetLineColor();

        RectTransform rt = BlueCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = BlueCrayon;
    }

    public void ColorPurpleButton(GameObject PurpleCrayon)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼 처리
        lineColor = new Color(0.859f, 0.439f, 0.576f);
        SetLineColor();

        RectTransform rt = PurpleCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = PurpleCrayon;
    }
}
