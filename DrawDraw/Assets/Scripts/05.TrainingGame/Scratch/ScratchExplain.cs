using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchExplain : MonoBehaviour
{

    public Text Explain;                 // ���� �ؽ�Ʈ 


    public GameObject BlackLineAnim;     // ������ ��ĥ�ϴ� ������Ʈ 
    public GameObject SelectDraw;        // ���ȼ��� â
    public GameObject BlackScratch;      // ���� Ȱ��ȭ 


    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;


    public Camera cameraToCapture;           // ĸó�� ī�޶�
    public GameObject targetObject;          // ĸó�� ������ ������Ʈ
    public bool stopCalculating = false;     // ��� �ߴ� �÷���
    float whitePixelRatio;


    private bool isSelectCryon;         // �������� ���� �ߴ°�?
    private bool isStart;               // ��ĥ�� �����ߴ°�?
    private bool isDrawing;             // ��ĥ�� �ϰ� �ִ°�?
    private bool isBlackLine;           // ������ ĥ������ �ִϸ��̼�?
    private bool isSelect;              // ���� ���� â ������°�?
    private bool isBlackAct;            // ���� Ȱ��ȭ �Ǿ��°�?
    private bool isStartSratch;         // ���� ���� �ܾ����°�?
    private bool isPlaying;             // ���� ���� �ܰ� �ִ°�?
    private bool isGray;                // ȸ���� ���� ���� �ܾ��°�?


    private Coroutine ColorChangeCoroutine;
    private Coroutine DrawingCoroutine;


    void Start()
    {
        StartCoroutine(CalculateWhitePixelRatioCoroutine(5f));   // �ڷ�ƾ ����
    }


    void Update()
    {

        // �������� ������ ����ٸ�
        // -> �� �κ��� ��ĥ�ؼ� ä������
        if(scratchdraw.isSelectColor && (!isSelectCryon))
        {
            Explain.text = "�� �κ��� ��ĥ�ؼ� ä������";

            isSelectCryon = true; // �������� ����� �� ó�� �ѹ��� ������ ���� 
        }



        // ó�� ���� ĥ�ϰ� 5�� ���� �Ŀ� 
        // -> �������� ������ ������ ������� �ٲ� �� �־�
        else if(scratchdraw.isStartDraw && (!isStart))
        {
            ColorChangeCoroutine = StartCoroutine(ExplainColorChangeOK(8f));

            isStart = true;
        }



        // -> ������ �� ��ȭ���� ��� ��ĥ�غ���! 
        else if(isStart && (!isDrawing) && (!isBlackLine) && (!stopCalculating))
        {
            DrawingCoroutine = StartCoroutine(ExplainDrawing(18f));

            isDrawing = true;
        }



        // �� ��ȭ���� 50�� �̻��� ĥ�����ٸ�
        // -> ��ĥ�� ��� ���´ٸ� �ϼ� ��ư�� Ŭ���غ���
        else if(whitePixelRatio < 0.5f && (!stopCalculating))
        {
            if (ColorChangeCoroutine != null)
            {
                StopCoroutine(ColorChangeCoroutine);
                print("ColorChange �ڷ�ƾ�� �ߴܵǾ����ϴ�.");
            }
            if (DrawingCoroutine != null)
            {
                StopCoroutine(DrawingCoroutine);
                print("Drawing �ڷ�ƾ�� �ߴܵǾ����ϴ�.");
            }

            Explain.text = "��ĥ�� ��� ���´ٸ� ���� ��ư�� Ŭ���غ���";

            stopCalculating = true;
        }



        // �ϼ� ��ư�� Ŭ�� �ǰ�, ������ ���̴� �ִϸ��̼� ��� ��
        // -> ��ȭ���� ���������� ���̰� �־�
        else if(BlackLineAnim.activeSelf && (!isBlackLine))
        {
            if (ColorChangeCoroutine != null)
            {
                StopCoroutine(ColorChangeCoroutine);
                print("ColorChange �ڷ�ƾ�� �ߴܵǾ����ϴ�.");
            }
            if (DrawingCoroutine != null)
            {
                StopCoroutine(DrawingCoroutine);
                print("Drawing �ڷ�ƾ�� �ߴܵǾ����ϴ�.");
            }

            Explain.text = "��ȭ���� ���������� ���̰� �־�!";
            stopCalculating = true;
            isBlackLine = true;
        }



        // ���� ���� â�� Ȱ��ȭ �Ǹ�
        // -> 4���� �׸� �� ������ ��� �׸��� ��󺼱�?
        else if(SelectDraw.activeSelf && (!isSelect))
        {
            Explain.text = "4���� �׸� �� ������ ��� �׸��� ��󺼱�?";
            isSelect = true;
        }



        // ������ Ȱ��ȭ �Ǹ�
        // -> �������� ȸ�� �߿��� ȸ�� �κ��� ������ �׾��?
        else if(BlackScratch.activeSelf && (!isBlackAct))
        {
            Explain.text = "�������� ȸ�� �߿��� ȸ�� �κ��� ������ �׾��?";
            isBlackAct = true;
        }



        // �ѹ� �ܾ ��
        // -> ȸ���� �ܾ�ϱ� �Ʊ� �츮�� ��ĥ�� ��ȭ���� ���̳�!
        // -> ȸ���κи� ��� �ܾ����? 
        else if(scratchblack.isScratching && (!isStartSratch) && (!isGray))
        {
            Explain.text = "ȸ���� �ܾ�ϱ� �Ʊ� �츮�� ��ĥ�� ��ȭ���� ���̳�!";
            StartCoroutine(ExplainScratching(5f));
            isStartSratch = true;
        }



        // ��ũ��ġ �Է��� �ְ� �� �� 
        // -> ���ϰ� �־�! ȸ���� ��� �ܾ�� �ϼ� ��ư�� ������!
        else if (isPlaying && (!isGray))
        {
            print("���ϰ� �־�! ȸ���� ��� �ܾ�� �ϼ� ��ư�� ������!");
            StartCoroutine(ExplainFinish(20f));
            isGray = true;
        }

    }

    IEnumerator ExplainColorChangeOK(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "�������� ������ ������ ������� �ٲ� �� �־�";
    }

    IEnumerator ExplainDrawing(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "������ �� �κ��� ��� ��ĥ�غ���!";
    }

    IEnumerator ExplainScratching(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "ȸ���κи� ��� �ܾ����?";
        isPlaying = true;
    }

    IEnumerator ExplainFinish(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "���ϰ� �־�! ȸ���� ��� �ܾ�� �ϼ� ��ư�� ������!";
    }



    // Ư�� �������� ��� �ȼ� ������ ��� : �ڷ�ƾ 
    IEnumerator CalculateWhitePixelRatioCoroutine(float interval)
    {
        while (!stopCalculating)
        {
            Rect captureRect = GetScreenRectFromObject(targetObject, cameraToCapture);  // ������Ʈ�� ȭ�� ������ ���

            Texture2D screenShot = CaptureScreenshot(cameraToCapture, captureRect);     // ��ũ������ ĸó -> ��� �ȼ� ������ ���
            whitePixelRatio = CalculateWhitePixelRatio(screenShot); 
            // print("��� �ȼ��� ����: " + whitePixelRatio);

            
            yield return new WaitForSeconds(interval);  // ���
        }

        print("��� ���� ��� �ߴ��մϴ�.");
    }



    // ������Ʈ�� ���� ��ǥ -> ȭ�� ��ǥ�� ��ȯ -> Rect�� ��ȯ
    Rect GetScreenRectFromObject(GameObject obj, Camera cam)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            return new Rect();
        }

        
        Bounds bounds = renderer.bounds;   // ������Ʈ�� ���(Bounds)�� ���ϱ� 

        
        Vector3 minScreen = cam.WorldToScreenPoint(bounds.min);   // ���� ��ǥ -> ȭ�� ��ǥ ��ȯ
        Vector3 maxScreen = cam.WorldToScreenPoint(bounds.max);


        // Rect ����
        float x = Mathf.Min(minScreen.x, maxScreen.x);
        float y = Mathf.Min(minScreen.y, maxScreen.y);
        float width = Mathf.Abs(maxScreen.x - minScreen.x);
        float height = Mathf.Abs(maxScreen.y - minScreen.y);


        return new Rect(x, y, width, height);
    }



    // �־��� Rect ���� ĸó -> Texture2D�� ��ȯ
    Texture2D CaptureScreenshot(Camera cam, Rect rect, int downscaleFactor = 2)
    {

        // �ٿ� �����ϸ��� ũ�� ��� (��귮 ���Ҹ� ���� �ٿ� �����ϸ� ����)
        int width = (int)rect.width / downscaleFactor;
        int height = (int)rect.height / downscaleFactor;


        // RenderTexture ����, ���� 
        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;
        cam.Render();


        // RenderTexture -> Texture2D
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();


        // ���� ���� ����
        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);


        return screenShot;
    }



    // �־��� Texture2D���� ��� �ȼ� ���� ���
    // ( ��귮 ���� ���� 10 �ȼ� �������� ���ø� )
    float CalculateWhitePixelRatio(Texture2D texture, int sampleInterval = 10)
    {
        Color[] pixels = texture.GetPixels();
        int whitePixelCount = 0;
        int sampleCount = 0;


        // ���� �������� �ȼ��� ���ø��� ��� �ȼ� ���� ���
        for (int y = 0; y < texture.height; y += sampleInterval)
        {
            for (int x = 0; x < texture.width; x += sampleInterval)
            {
                Color pixel = pixels[y * texture.width + x];
                if (pixel.r >= 0.95f && pixel.g >= 0.95f && pixel.b >= 0.95f)
                {
                    whitePixelCount++;
                }
                sampleCount++;
            }
        }

        // ��� �ȼ� ���� ��ȯ
        return (float)whitePixelCount / sampleCount;
    }
}
