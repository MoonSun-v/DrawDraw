using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchExplain : MonoBehaviour
{
    public Text Explain; // 설명 텍스트 

    // public GameObject ScratchBlack; // 검은도안 

    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;

    public Camera cameraToCapture; // 캡처할 카메라
    public GameObject targetObject; // 캡처할 영역의 오브젝트
    public bool stopCalculating = false; // 계산 중단 플래그

    private bool isSelectCryon; // 색연필을 선택 했는가?
    private bool isStart; // 색칠을 시작했는가?

    void Start()
    {
        // 코루틴 시작
        StartCoroutine(CalculateWhitePixelRatioCoroutine(5f));
    }

    // Update is called once per frame
    void Update()
    {
        // 색연필의 색깔을 골랐다면
        // -> 흰 부분을 색칠해서 채워보자
        if(scratchdraw.isSelectColor && (!isSelectCryon))
        {
            Explain.text = "흰 부분을 색칠해서 채워보자";

            isSelectCryon = true; // 색연필을 골랐을 때 처음 한번만 실행을 위해 
        }

        // 처음 색을 칠하고 5초 정도 후에 
        // -> 색연필의 색깔은 언제나 마음대로 바꿀 수 있어
        else if(scratchdraw.isStartDraw && !isStart)
        {
            StartCoroutine(ExplainColorChangeOK(8f));

            isStart = true;
        }

        // 흰 도화지의 50퍼 이상이 칠해졌다면
        // -> 색칠을 모두 끝냈다면 완성 버튼을 클릭해보자


    }

    IEnumerator ExplainColorChangeOK(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "색연필의 색깔은 언제나 마음대로 바꿀 수 있어";
    }

    // 특정 간격으로 흰색 픽셀 비율을 계산 : 코루틴 
    IEnumerator CalculateWhitePixelRatioCoroutine(float interval)
    {
        while (!stopCalculating)
        {
            // 오브젝트의 화면 영역을 계산
            Rect captureRect = GetScreenRectFromObject(targetObject, cameraToCapture);

            // 스크린샷을 캡처 -> 흰색 픽셀 비율을 계산
            Texture2D screenShot = CaptureScreenshot(cameraToCapture, captureRect);
            float whitePixelRatio = CalculateWhitePixelRatio(screenShot);
            print("흰색 픽셀의 비율: " + whitePixelRatio);

            // 대기
            yield return new WaitForSeconds(interval);
        }

        print("흰색 비율 계산 중단합니다.");
    }

    // 오브젝트의 월드 좌표 -> 화면 좌표로 변환 -> Rect로 반환
    Rect GetScreenRectFromObject(GameObject obj, Camera cam)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null)
        {
            return new Rect();
        }

        // 오브젝트의 경계(Bounds)를 구하기 
        Bounds bounds = renderer.bounds;
        
        // 월드 좌표 -> 화면 좌표 변환
        Vector3 minScreen = cam.WorldToScreenPoint(bounds.min);
        Vector3 maxScreen = cam.WorldToScreenPoint(bounds.max);

        // Rect 생성
        float x = Mathf.Min(minScreen.x, maxScreen.x);
        float y = Mathf.Min(minScreen.y, maxScreen.y);
        float width = Mathf.Abs(maxScreen.x - minScreen.x);
        float height = Mathf.Abs(maxScreen.y - minScreen.y);

        return new Rect(x, y, width, height);
    }

    // 주어진 Rect 영역 캡처 -> Texture2D로 반환
    Texture2D CaptureScreenshot(Camera cam, Rect rect, int downscaleFactor = 2)
    {
        // 다운 스케일링된 크기 계산 (계산량 감소를 위해 다운 스케일링 진행)
        int width = (int)rect.width / downscaleFactor;
        int height = (int)rect.height / downscaleFactor;

        // RenderTexture 생성, 설정 
        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;
        cam.Render();

        // RenderTexture -> Texture2D
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        // 원래 설정 복구
        cam.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        return screenShot;
    }

    // 주어진 Texture2D에서 흰색 픽셀 비율 계산
    // 10 픽셀 간격으로 샘플링 (계산량 감소 위해)
    float CalculateWhitePixelRatio(Texture2D texture, int sampleInterval = 10)
    {
        Color[] pixels = texture.GetPixels();
        int whitePixelCount = 0;
        int sampleCount = 0;

        // 일정 간격으로 픽셀을 샘플링해 흰색 픽셀 비율 계산
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

        // 흰색 픽셀 비율 반환
        return (float)whitePixelCount / sampleCount;
    }
}
