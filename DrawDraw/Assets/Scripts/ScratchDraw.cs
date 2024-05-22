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

    private void Start()
    {
        lineRenderer = brush.GetComponent<LineRenderer>();

        // �⺻ ������ �׷���
        lineColor = Color.gray;
        SetLineColor();
    }

    private void Update()
    {
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
        currentLineRenderer = null; // ���� �׸��� �� ����
    }

    public void SetLineColor()
    {
        // LineRenderer�� ������ �����մϴ�.
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }


    //// �׸� ���� ���� �� �귯�� �������� ���� ����
    
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
