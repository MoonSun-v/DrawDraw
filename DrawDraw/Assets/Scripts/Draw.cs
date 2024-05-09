using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer; // ���� �� �׸��� �� ���Ǵ� LineRenderer ������Ʈ ����

    Vector2 lastPos; // ���������� �׷��� ���� ��ġ�� ���� 

    [SerializeField, Range(0.0f, 2.0f)]
    private float width; // �� ���� ���� 

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
            currentLineRenderer = null; // ���� �׸��� �� ����
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
}
