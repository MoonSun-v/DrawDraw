using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchExplain : MonoBehaviour
{
    public Text Explain; // ���� �ؽ�Ʈ 

    // public GameObject ScratchBlack; // �������� 

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public Camera cameraToCapture; // ĸó�� ī�޶�
    public GameObject targetObject; // ĸó�� ������ ������Ʈ
    public bool stopCalculating = false; // ��� �ߴ� �÷���

    private bool isSelectCryon; // �������� ���� �ߴ°�?
    private bool isStart; // ��ĥ�� �����ߴ°�?

    void Start()
    {
        // �ڷ�ƾ ����
        StartCoroutine(CalculateWhitePixelRatioCoroutine(5f));
    }

    // Update is called once per frame
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
        else if(scratchdraw.isStartDraw && !isStart)
        {
            StartCoroutine(ExplainColorChangeOK(8f));

            isStart = true;
        }

        // �� ��ȭ���� 50�� �̻��� ĥ�����ٸ�
        // -> ��ĥ�� ��� ���´ٸ� �ϼ� ��ư�� Ŭ���غ���


    }

    IEnumerator ExplainColorChangeOK(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "�������� ������ ������ ������� �ٲ� �� �־�";
    }

    // Ư�� �������� ��� �ȼ� ������ ��� : �ڷ�ƾ 
    IEnumerator CalculateWhitePixelRatioCoroutine(float interval)
    {
        while (!stopCalculating)
        {
            // ������Ʈ�� ȭ�� ������ ���
            Rect captureRect = GetScreenRectFromObject(targetObject, cameraToCapture);

            // ��ũ������ ĸó -> ��� �ȼ� ������ ���
            Texture2D screenShot = CaptureScreenshot(cameraToCapture, captureRect);
            float whitePixelRatio = CalculateWhitePixelRatio(screenShot);
            print("��� �ȼ��� ����: " + whitePixelRatio);

            // ���
            yield return new WaitForSeconds(interval);
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

        // ������Ʈ�� ���(Bounds)�� ���ϱ� 
        Bounds bounds = renderer.bounds;
        
        // ���� ��ǥ -> ȭ�� ��ǥ ��ȯ
        Vector3 minScreen = cam.WorldToScreenPoint(bounds.min);
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
    // 10 �ȼ� �������� ���ø� (��귮 ���� ����)
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
