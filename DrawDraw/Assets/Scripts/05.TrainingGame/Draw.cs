using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;     // ���� �� �׸��� �� ���Ǵ� LineRenderer ������Ʈ ����

    Vector2 lastPos;                      // ���������� �׷��� ���� ��ġ�� ���� 

    [SerializeField, Range(0.0f, 2.0f)]
    private float width;                  // �� ���� ���� 

    public List<GameObject> lineRenderers = new List<GameObject>();   // ������ LineRenderer�� �����ϱ� ���� ����Ʈ


    private void Update()
    {
        Drawing();
    }


    void Drawing()
    {

        if (Input.GetMouseButtonDown(0))     // ������ �� �ѹ��� (������ �־ �ѹ�..!)
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))    // ������ �ִ� ���
        {
            PointToMousePos();
        }
        else if (Input.GetMouseButtonUp(0))  // ������ ��
        {
            currentLineRenderer = null;      // ���� �׸��� �� ����
        }
    }



    // �귯�� ����, �ʱ�ȭ
    void CreateBrush()
    {
        // ���콺 ��ġ�� �̿��� �귯���� LineRenderer�� ���۰� �� ���� ����

        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.startWidth = currentLineRenderer.endWidth = width;   // �� ���� �׻� �����ϰ� 

       
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);      // ���� �������� �����Ϸ��� 2�� ���� �־�� �ϴϱ�

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

        
        lineRenderers.Add(brushInstance);         // ������ LineRenderer ��ü�� ����Ʈ�� �߰�                           
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
        if ((lastPos - mousePos).magnitude > 0.1f)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    // ������ ��� LineRenderer ��ü�� �����ϴ� �޼���
    public void ClearAllLineRenderers()
    {
        foreach (GameObject lineRendererObject in lineRenderers)
        {
            Destroy(lineRendererObject);
        }
        lineRenderers.Clear();                                    // ����Ʈ �ʱ�ȭ

    }
}
