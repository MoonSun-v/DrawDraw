using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchExplain : MonoBehaviour
{

    public Text Explain;                 // 설명 텍스트 


    public GameObject BlackLineAnim;     // 검은선 색칠하는 오브젝트 
    public GameObject SelectDraw;        // 도안선택 창
    public GameObject BlackScratch;      // 도안 활성화 


    [SerializeField]
    private ScratchDraw scratchdraw;
    [SerializeField]
    private ScratchBlack scratchblack;


    public Camera cameraToCapture;           // 캡처할 카메라
    public GameObject targetObject;          // 캡처할 영역의 오브젝트
    public bool stopCalculating = false;     // 계산 중단 플래그
    float whitePixelRatio;


    private bool isSelectCryon;         // 색연필을 선택 했는가?
    private bool isStart;               // 색칠을 시작했는가?
    private bool isDrawing;             // 색칠을 하고 있는가?
    private bool isBlackLine;           // 검은색 칠해지는 애니메이션?
    private bool isSelect;              // 도안 선택 창 띄워졌는가?
    private bool isBlackAct;            // 도안 활성화 되었는가?
    private bool isStartSratch;         // 검은 도안 긁어졌는가?
    private bool isPlaying;             // 검은 도안 긁고 있는가?
    private bool isGray;                // 회색을 절반 정도 긁었는가?


    private Coroutine ColorChangeCoroutine;
    private Coroutine DrawingCoroutine;


    void Start()
    {
        StartCoroutine(CalculateWhitePixelRatioCoroutine(5f));   // 코루틴 시작
    }


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
        else if(scratchdraw.isStartDraw && (!isStart))
        {
            ColorChangeCoroutine = StartCoroutine(ExplainColorChangeOK(8f));

            isStart = true;
        }



        // -> 열심히 흰 도화지를 모두 색칠해보자! 
        else if(isStart && (!isDrawing) && (!isBlackLine) && (!stopCalculating))
        {
            DrawingCoroutine = StartCoroutine(ExplainDrawing(18f));

            isDrawing = true;
        }



        // 흰 도화지의 50퍼 이상이 칠해졌다면
        // -> 색칠을 모두 끝냈다면 완성 버튼을 클릭해보자
        else if(whitePixelRatio < 0.5f && (!stopCalculating))
        {
            if (ColorChangeCoroutine != null)
            {
                StopCoroutine(ColorChangeCoroutine);
                print("ColorChange 코루틴이 중단되었습니다.");
            }
            if (DrawingCoroutine != null)
            {
                StopCoroutine(DrawingCoroutine);
                print("Drawing 코루틴이 중단되었습니다.");
            }

            Explain.text = "색칠을 모두 끝냈다면 다음 버튼을 클릭해보자";

            stopCalculating = true;
        }



        // 완성 버튼이 클릭 되고, 검은색 덮이는 애니메이션 재생 중
        // -> 도화지가 검은색으로 덮이고 있어
        else if(BlackLineAnim.activeSelf && (!isBlackLine))
        {
            if (ColorChangeCoroutine != null)
            {
                StopCoroutine(ColorChangeCoroutine);
                print("ColorChange 코루틴이 중단되었습니다.");
            }
            if (DrawingCoroutine != null)
            {
                StopCoroutine(DrawingCoroutine);
                print("Drawing 코루틴이 중단되었습니다.");
            }

            Explain.text = "도화지가 검은색으로 덮이고 있어!";
            stopCalculating = true;
            isBlackLine = true;
        }



        // 도안 선택 창이 활성화 되면
        // -> 4가지 그림 중 마음에 드는 그림을 골라볼까?
        else if(SelectDraw.activeSelf && (!isSelect))
        {
            Explain.text = "4가지 그림 중 마음에 드는 그림을 골라볼까?";
            isSelect = true;
        }



        // 도안이 활성화 되면
        // -> 검은색과 회색 중에서 회색 부분을 펜으로 그어볼래?
        else if(BlackScratch.activeSelf && (!isBlackAct))
        {
            Explain.text = "검은색과 회색 중에서 회색 부분을 펜으로 그어볼래?";
            isBlackAct = true;
        }



        // 한번 긁어낸 후
        // -> 회색을 긁어내니까 아까 우리가 색칠한 도화지가 보이네!
        // -> 회색부분만 모두 긁어내볼까? 
        else if(scratchblack.isScratching && (!isStartSratch) && (!isGray))
        {
            Explain.text = "회색을 긁어내니까 아까 우리가 색칠한 도화지가 보이네!";
            StartCoroutine(ExplainScratching(5f));
            isStartSratch = true;
        }



        // 스크래치 입력이 있고 난 후 
        // -> 잘하고 있어! 회색을 모두 긁어내면 완성 버튼을 눌러줘!
        else if (isPlaying && (!isGray))
        {
            print("잘하고 있어! 회색을 모두 긁어내면 완성 버튼을 눌러줘!");
            StartCoroutine(ExplainFinish(20f));
            isGray = true;
        }

    }

    IEnumerator ExplainColorChangeOK(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "색연필의 색깔은 언제나 마음대로 바꿀 수 있어";
    }

    IEnumerator ExplainDrawing(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "열심히 흰 부분을 모두 색칠해보자!";
    }

    IEnumerator ExplainScratching(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "회색부분만 모두 긁어내볼까?";
        isPlaying = true;
    }

    IEnumerator ExplainFinish(float interval)
    {
        yield return new WaitForSeconds(interval);

        Explain.text = "잘하고 있어! 회색을 모두 긁어내면 완성 버튼을 눌러줘!";
    }



    // 특정 간격으로 흰색 픽셀 비율을 계산 : 코루틴 
    IEnumerator CalculateWhitePixelRatioCoroutine(float interval)
    {
        while (!stopCalculating)
        {
            Rect captureRect = GetScreenRectFromObject(targetObject, cameraToCapture);  // 오브젝트의 화면 영역을 계산

            Texture2D screenShot = CaptureScreenshot(cameraToCapture, captureRect);     // 스크린샷을 캡처 -> 흰색 픽셀 비율을 계산
            whitePixelRatio = CalculateWhitePixelRatio(screenShot); 
            // print("흰색 픽셀의 비율: " + whitePixelRatio);

            
            yield return new WaitForSeconds(interval);  // 대기
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

        
        Bounds bounds = renderer.bounds;   // 오브젝트의 경계(Bounds)를 구하기 

        
        Vector3 minScreen = cam.WorldToScreenPoint(bounds.min);   // 월드 좌표 -> 화면 좌표 변환
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
    // ( 계산량 감소 위해 10 픽셀 간격으로 샘플링 )
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
