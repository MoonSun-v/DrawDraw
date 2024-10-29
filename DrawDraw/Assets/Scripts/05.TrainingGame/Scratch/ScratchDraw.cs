using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchDraw: MonoBehaviour
{
    public Camera m_camera;

    // [ �׸����� ���� ���� ]
    public GameObject brush;                      // �귯�� ������ 
    private Color lineColor;
    private LineRenderer lineRenderer;
    private LineRenderer currentLineRenderer;     // ���� �� �׸���� LineRenderer

    private Vector2 lastPos;                      // ���������� �׷��� ���� ��ġ�� ���� 

    // [ �׸��� ���� ���� ]
    [SerializeField, Range(0.0f, 2.0f)]
    private float width = 0.66f;                  // �� ���� ���� 
    
    public ScratchManager Scratch;

    // [ ���� ���� ���� ���� ]
    public GameObject previousButton;                 // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition;    // ���� ��ư�� ���� ��ġ�� ����
    private const int CrayonMove = 60;                // ��ư �̵� �Ÿ�

    // [ �׷��� �� ���� �� ���� ]
    public List<GameObject> lineRenderers = new List<GameObject>();     // ������ LineRenderer�� �����ϱ� ���� ����Ʈ
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
            // 1. �� [ ����� ��ġ ó�� ]

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // ��ġ ���� �� ( GetMouseButtonDown )
                if (touch.phase == TouchPhase.Began)
                {
                    if (currentLineRenderer == null)
                    {
                        CreateBrush();
                    }
                }

                // ��ġ �̵� �� ( GetMouseButton )
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    Drawing();
                }

                // ��ġ ���� �� ( GetMouseButtonUp )
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    if (currentLineRenderer != null)
                    {
                        FinishLineRenderer();
                    }
                }
            }
            // 2. �� [ ���콺 ó�� ]
            else
            {
                Drawing();
            }
            
        }  
    }



    // �� [ �׸��� �۾� ���� ] ��
    // 
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
        else if (Input.GetMouseButtonUp(0))  
        {
            FinishLineRenderer();
        }

    }


    // �� [ �귯�� ����, �ʱ�ȭ ]
    // 
    // - �� ���� �׻� �����ϰ� ����
    // - ���콺 ��ġ�� �̿��� �귯�� LineRenderer�� ���۰� �� ���� ����
    //   ( ���� �������� �����Ϸ��� 2�� ���� �־�� �ϴϱ� )
    // - ������ LineRenderer ��ü�� ����Ʈ�� �߰�
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



    // �� [ ���콺 ��ġ ���� �� �׸��� ]
    //
    // ( ���콺 ��ġ�� ����Ǿ��� ���� ���ο� �� �߰� )
    // 
    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        if ((lastPos - mousePos).magnitude > 0.1f)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }



    // �� [ ���� ���ο� �� �߰� ]
    //
    // ���� positionCount ������Ű�� ���ο� �� ��ġ ���� 
    // 
    void AddAPoint(Vector2 pointPos)
    {
        if (!currentLineRenderer)
        {
            print("currentLineRenderer�� null �Դϴ�.");
            return;
        }

        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }


    // �� [ ���� �� �׸��� ���� ] 
    //
    // isStartDraw : ���� ���� ������Ʈ ���� �۾�
    //
    public void FinishLineRenderer()
    {
        currentLineRenderer = null;    

        if (!isStartDraw)              
        {
            isStartDraw = true;
            print("isStartDraw�� true�� �Ǿ����ϴ�");
        }
    }


    // �� [ LineRenderer ���� ���� ]
    //
    // isSelectColor : ���� ���� ������Ʈ ���� �۾�
    //
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


    // �� [ ������ ��� LineRenderer ��ü�� ���� ] : 'ó������' ��ư Ŭ��
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


    // �� [ �귯�� ������ ���� ���� ]
    //
    // 1. ���� ���� ��ư ����ġ
    // 2. ���� ����
    // 3. ���� ���� ��ư�� ���� ��ġ ����
    // 4. ���� ��ư�� ���� ��ư���� ������Ʈ
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

        // ��* ���� ��, ���� ���� �� �ι� ��ġ ���� �˾ƺ��� ���� �ڵ� 
        currentLineRenderer = null;  // ���� ���� �� currentLineRenderer �ʱ�ȭ
    }


    #region �귯�� ���� ���� �޼��� ����

    public void ColorRedButton(GameObject RedCrayon)
    {
        SelectColorButton(RedCrayon, new Color(0.8901961f, 0.01568628f, 0.01960784f));
    }

    public void ColorOrangeButton(GameObject OrangeCrayon)
    {
        SelectColorButton(OrangeCrayon, new Color(0.9411765f, 0.5294118f, 0.04705882f));
    }

    public void ColorYellowButton(GameObject YellowCrayon)
    {
        SelectColorButton(YellowCrayon, new Color(0.945098f, 0.8431373f, 0.07058824f));
    }

    public void ColorGreenButton(GameObject GreenCrayon)
    {
        SelectColorButton(GreenCrayon, new Color(0.2313726f, 0.6117647f, 0f));
    }

    public void ColorSkyBlueButton(GameObject SkyBlueCrayon)
    {
        SelectColorButton(SkyBlueCrayon, new Color(0.007843138f, 0.5215687f, 0.9960784f));
    }

    public void ColorBlueButton(GameObject BlueCrayon)
    {
        SelectColorButton(BlueCrayon, new Color(0.1803922f, 0.2f, 0.8431373f));
    }

    public void ColorPurpleButton(GameObject PurpleCrayon)
    {
        SelectColorButton(PurpleCrayon, new Color(0.3529412f, 0.0627451f, 0.7333333f));
    }

    #endregion

}