using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchDraw: MonoBehaviour
{
    public Camera m_camera;

    //// �׸����� ���� ���� 
    public GameObject brush; // �귯�� ������ 
    private LineRenderer lineRenderer;
    private Color lineColor;

    LineRenderer currentLineRenderer; // ���� �� �׸��� �� ���Ǵ� LineRenderer ������Ʈ ����

    Vector2 lastPos; // ���������� �׷��� ���� ��ġ�� ���� 

    [SerializeField, Range(0.0f, 2.0f)]
    private float width; // �� ���� ���� 

    public ScratchManager Scratch;

    public GameObject previousButton; // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition; // ���� ��ư�� ���� ��ġ�� ����
    private int CrayonMove = 90;

    public List<GameObject> lineRenderers = new List<GameObject>(); // ������ LineRenderer�� �����ϱ� ���� ����Ʈ

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
        // ���� ���� ������ ���� �ʾ����� �׸��� ����
        if (previousButton == null)
        {
            return;
        }

        Drawing();
    }

    void Drawing()
    {

        if (Input.GetMouseButtonDown(0)) // ������ �� �ѹ��� (������ �־ �ѹ�..!)
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0)) // ������ �ִ� ���
        {
            PointToMousePos();
        }
        else if (Input.GetMouseButtonUp(0)) // ������ ��
        {
            // currentLineRenderer = null; // ���� �׸��� �� ����
            FinishLineRenderer();
        }

    }

    // �귯�ø� ����, �ʱ�ȭ
    void CreateBrush()
    {
        // ���콺 ��ġ�� �̿��� �귯���� LineRenderer�� ���۰� �� ���� ����

        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = currentLineRenderer.endWidth = width; // �� ���� �׻� �����ϰ� 

        // ���� �������� �����Ϸ��� 2�� ���� �־�� �ϴϱ�
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

        // ������ LineRenderer ��ü�� ����Ʈ�� �߰�
        lineRenderers.Add(brushInstance);
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
        currentLineRenderer = null; // ���� �׸��� �� ����

        if (!isStartDraw) // ù ��ĥ�� ���۵Ǹ� 
        {
            isStartDraw = true;
            print("isStartDraw�� true�� �Ǿ����ϴ�");
        }
    }

    public void SetLineColor()
    {
        // LineRenderer�� ������ �����մϴ�.
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
        // ScratchBlack�� ��Ȱ��ȭ�� ���� ���� ����
        if (!ScratchBlack.activeSelf)
        {
            foreach (GameObject lineRendererObject in lineRenderers)
            {
                Destroy(lineRendererObject);
            }
            lineRenderers.Clear(); // ����Ʈ �ʱ�ȭ
        }
        
    }

    //// �׸� ���� ���� �� �귯�� �������� ���� ����

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
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = new Color(1f, 0.5f, 0f);
        SetLineColor();

        RectTransform rt = OrangeCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = OrangeCrayon;
    }

    public void ColorYellowButton(GameObject YellowCrayon)
    {
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = Color.yellow;
        SetLineColor();

        RectTransform rt = YellowCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = YellowCrayon;
    }

    public void ColorGreenButton(GameObject GreenCrayon)
    {
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = new Color(0f, 0.392f, 0f);
        SetLineColor();

        RectTransform rt = GreenCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = GreenCrayon;
    }

    public void ColorSkyBlueButton(GameObject SkyBlueCrayon)
    {
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = new Color(0.529f, 0.808f, 0.922f); 
        SetLineColor();

        RectTransform rt = SkyBlueCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = SkyBlueCrayon;
    }

    public void ColorBlueButton(GameObject BlueCrayon)
    {
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = Color.blue;
        SetLineColor();

        RectTransform rt = BlueCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = BlueCrayon;
    }

    public void ColorPurpleButton(GameObject PurpleCrayon)
    {
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư ó��
        lineColor = new Color(0.859f, 0.439f, 0.576f);
        SetLineColor();

        RectTransform rt = PurpleCrayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
        previousButton = PurpleCrayon;
    }
}
