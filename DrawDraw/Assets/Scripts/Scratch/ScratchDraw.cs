using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchDraw: MonoBehaviour
{
    public Camera m_camera;

    //// �׸����� ���� ���� 
    public GameObject brush;              // �귯�� ������ 
    private LineRenderer lineRenderer;
    private Color lineColor;

    LineRenderer currentLineRenderer;     // ���� �� �׸��� �� ���Ǵ� LineRenderer ������Ʈ ����

    Vector2 lastPos;                      // ���������� �׷��� ���� ��ġ�� ���� 

    [SerializeField, Range(0.0f, 2.0f)]
    private float width;                  // �� ���� ���� 

    public ScratchManager Scratch;

    public GameObject previousButton;                 // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition;    // ���� ��ư�� ���� ��ġ�� ����
    private int CrayonMove = 90;

    public List<GameObject> lineRenderers = new List<GameObject>();     // ������ LineRenderer�� �����ϱ� ���� ����Ʈ

    public GameObject ScratchBlack;

    public bool isSelectColor;
    public bool isStartDraw;



    private void Start()
    {
        lineRenderer = brush.GetComponent<LineRenderer>();

        // �⺻ ������ �׷���
        // lineColor = Color.gray;
        // SetLineColor();
    }


    private void Update()
    {
        Drawing();
    }


    void Drawing()
    {
        if (previousButton == null)    // ���� ���� ������ ���� �ʾ����� �׸��� ����
        {
            return;
        }
        else if (Input.GetMouseButtonDown(0))     // ������ �� �ѹ��� (������ �־ �ѹ�..!)
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))    // ������ �ִ� ���
        {
            PointToMousePos();
        }
        else if (Input.GetMouseButtonUp(0))  // ������ ��
        {
            // currentLineRenderer = null;   // ���� �׸��� �� ����
            FinishLineRenderer();
        }

    }


    // �귯�ø� ����, �ʱ�ȭ
    void CreateBrush()
    {
        // ���콺 ��ġ�� �̿��� �귯���� LineRenderer�� ���۰� �� ���� ����

        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = currentLineRenderer.endWidth = width;      // �� ���� �׻� �����ϰ� 

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);        // ���� �������� �����Ϸ��� 2�� ���� �־�� �ϴϱ�

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

        
        lineRenderers.Add(brushInstance);    // ������ LineRenderer ��ü�� ����Ʈ�� �߰�
    }



    // ���� ���ο� �� �߰� : ���� positionCount ������Ű�� ���ο� �� ��ġ ���� 
    void AddAPoint(Vector2 pointPos)
    {
        if (!currentLineRenderer) return;

        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }



    // ���콺 ��ġ ���� �� �׸��� (���콺 ��ġ�� ����Ǿ��� ���� ���ο� ���� �߰�)
    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        if ((lastPos - mousePos).magnitude > 0.1f)
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
        currentLineRenderer = null;    // ���� �׸��� �� ����

        if (!isStartDraw)              // ù ��ĥ�� ���۵Ǹ� 
        {
            isStartDraw = true;
            print("isStartDraw�� true�� �Ǿ����ϴ�");
        }
    }


    // LineRenderer ���� ����
    public void SetLineColor()
    {
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        if(!isSelectColor)
        {
            isSelectColor = true;
            print("isSelectColor�� true�� �Ǿ����ϴ�");
        }
    }


    // ó������ ��ư Ŭ�� �� ������ ��� LineRenderer ��ü�� �����ϴ� �޼���
    public void ClearAllLineRenderers()
    {
        if (!ScratchBlack.activeSelf)        // ScratchBlack�� ��Ȱ��ȭ�� ���� ���� ����
        {
            foreach (GameObject lineRendererObject in lineRenderers)
            {
                Destroy(lineRendererObject);
            }
            lineRenderers.Clear();           // ����Ʈ �ʱ�ȭ
        }
        
    }



    // �׸� ���� ���� �� �귯�� �������� ���� ���� ----------------------------------------------------------------------------

    #region �귯�� ���� ���� �޼��� ����

    public void ColorRedButton(GameObject RedCrayon)
    {
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = Color.red;
        SetLineColor();

        RectTransform rt = RedCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = RedCrayon;
    }

    public void ColorOrangeButton(GameObject OrangeCrayon)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = new Color(1f, 0.5f, 0f);
        SetLineColor();

        RectTransform rt = OrangeCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = OrangeCrayon;
    }

    public void ColorYellowButton(GameObject YellowCrayon)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = Color.yellow;
        SetLineColor();

        RectTransform rt = YellowCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = YellowCrayon;
    }

    public void ColorGreenButton(GameObject GreenCrayon)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = new Color(0f, 0.392f, 0f);
        SetLineColor();

        RectTransform rt = GreenCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = GreenCrayon;
    }

    public void ColorSkyBlueButton(GameObject SkyBlueCrayon)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = new Color(0.529f, 0.808f, 0.922f); 
        SetLineColor();

        RectTransform rt = SkyBlueCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = SkyBlueCrayon;
    }

    public void ColorBlueButton(GameObject BlueCrayon)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = Color.blue;
        SetLineColor();

        RectTransform rt = BlueCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = BlueCrayon;
    }

    public void ColorPurpleButton(GameObject PurpleCrayon)
    {
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        lineColor = new Color(0.859f, 0.439f, 0.576f);
        SetLineColor();

        RectTransform rt = PurpleCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; 
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        
        previousButton = PurpleCrayon;
    }

    #endregion

}