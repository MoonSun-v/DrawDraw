using UnityEngine;
using UnityEngine.UI;

public class ScratchBlack : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private ScratchManager scratchManager;

    private SpriteRenderer spriteRenderer;            // 스프라이트 렌더러 컴포넌트
    private Texture2D scratchTexture;                 // 스크레치 텍스처
    public bool isScratching = false;                 // 스크레치 중인지 여부

    private int scratchSize = 20;                     // 스크레치 영역 크기
    private Vector2? lastMousePosition = null;        // 마지막 마우스 위치 저장 : null도 허용
    private bool textureNeedsUpdate = false;          // 텍스처 업데이트 플래그

    private Color[] originalColors;                   // 원래 색상 배열

    public GameObject scratchBlack;                   // 자기자신 




    void Awake()
    {
        mainCamera = Camera.main;
    }


    void Start()
    {
         
        spriteRenderer = GetComponent<SpriteRenderer>();            // 스프라이트 렌더러 컴포넌트 가져오기

        if (spriteRenderer == null)
        {
            Debug.LogWarning("spriteRenderer가 null 입니다.");
        }

       
        scratchTexture = textureFromSprite(spriteRenderer.sprite);   // 스프라이트로 부터 읽기 가능한 직접 변경할 텍스처 생성 

        
        originalColors = scratchTexture.GetPixels();                 // 원래 색상을 저장


        // 새롭게 생성한 텍스처를 이용해 새로운 스프라이트를 생성하고 설정
        spriteRenderer.sprite = Sprite.Create(scratchTexture, new Rect(0, 0, scratchTexture.width, scratchTexture.height), Vector2.one * 0.5f);

    }

    void Update()
    {
        // 마우스 입력 감지
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 X
            if (mousePos.x < scratchManager.Limit_l.position.x || 
                mousePos.x > scratchManager.Limit_R.position.x || 
                mousePos.y < scratchManager.Limit_B.position.y || 
                mousePos.y > scratchManager.Limit_T.position.y)
            {
                return;
            }
            else
            {
                isScratching = true;
                lastMousePosition = null;     // 마우스를 처음 누를 때 이전 위치 초기화
            }
            
        }
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);

        }
        else if (Input.GetMouseButtonUp(0))
        {

            isScratching = false;
            lastMousePosition = null;        // 마우스를 뗄 때 이전 위치 초기화

            
            if (textureNeedsUpdate)          // 마우스 버튼을 뗄 때 텍스처를 적용
            {
                scratchTexture.Apply();
                textureNeedsUpdate = false;
            }
        }


        // 마우스 버튼이 눌려있는 동안 주기적으로 텍스처 적용
        if (textureNeedsUpdate && Time.frameCount % 5 == 0)
        {
            scratchTexture.Apply();
            textureNeedsUpdate = false;
        }

    }



    // 터치한 위치의 픽셀을 투명색으로 변경하는 함수
    void Scratch(Vector2 touchPosition)
    {
       
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);                         // 스프라이트의 로컬 좌표를 터치 좌표로 변환
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        
        if (lastMousePosition.HasValue)          // 현재 위치와 이전 위치를 이용하여 선을 그리는 함수 호출
        {
            DrawLine(lastMousePosition.Value, localTouchPosition);
        }

        lastMousePosition = localTouchPosition;
        textureNeedsUpdate = true;
    }



    // 선형 보간 :  두 점 사이의 선을 그리는 함수 -> 선 구성하는 모든 점을 처리하기 위해 (끊김 현상 개선)
    void DrawLine(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);    // 시작-끝 점 거리 계산
        int steps = Mathf.CeilToInt(distance * 5);        // 선을 따라 그릴 픽셀 수 계산 

        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            Vector2 point = Vector2.Lerp(start, end, t);
            DrawCircle(point);
        }
    }



    // 특정 위치에 원을 그리는 함수
    void DrawCircle(Vector2 position)
    {
        int startX = Mathf.RoundToInt((position.x + spriteRenderer.bounds.extents.x) * scratchTexture.width / spriteRenderer.bounds.size.x) - scratchSize / 2;
        int startY = Mathf.RoundToInt((position.y + spriteRenderer.bounds.extents.y) * scratchTexture.height / spriteRenderer.bounds.size.y) - scratchSize / 2;

        for (int x = startX; x < startX + scratchSize; x++)
        {
            for (int y = startY; y < startY + scratchSize; y++)
            {
                
                if (x >= 0 && x < scratchTexture.width && y >= 0 && y < scratchTexture.height)  // 픽셀 좌표가 유효한 범위 내에 있는지 확인
                {
                    
                    scratchTexture.SetPixel(x, y, Color.clear);                                 // 픽셀 색상을 투명색(알파값 0)으로 변경
                }
            }
        }
    }



    // 스프라이트에서 텍스처를 추출하는 함수
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)    // 스프라이트의 크기와 텍스처의 크기가 다르면 새로운 텍스처 생성
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
        {
            return sprite.texture;
        }
    }



    // 스크래치 효과를 리셋하는 함수
    // (스크래치 끝난 후 한번 더 실행시켜서 돌려놓아야 함 !!!!) : 이미지 파일 자체의 픽셀을 변경 시키는 것이므로...
    public void ResetScratch()
    {
        if (scratchBlack.activeSelf)                        // scratchBlack이 활성화 되어있을 때만 스크래치 리셋 
        {
            scratchTexture.SetPixels(originalColors);       // 원래 색상으로 텍스처를 복원
            scratchTexture.Apply();
        }
        
    }



    // 회색 부분이 모두 투명하게 변했는지 확인하는 함수
    bool CheckIfGrayPartsCleared(out float percentage)
    {
        Color[] currentColors = scratchTexture.GetPixels();
        int totalNonBlackPixels = 0; 
        int clearedNonBlackPixels = 0; 

        for (int i = 0; i < currentColors.Length; i++)      // 검은색을 제외한 모든 색상을 투명하게 만들었는지 확인
        {
            if (currentColors[i] != Color.black)
            {
                totalNonBlackPixels++;
                if (currentColors[i].a == 0)
                {
                    clearedNonBlackPixels++;
                }
            }
        }

        if (totalNonBlackPixels > 0)
        {
            percentage = (float)clearedNonBlackPixels / totalNonBlackPixels * 100f;
            return clearedNonBlackPixels == totalNonBlackPixels;
        }
        else
        {
            percentage = 0;
            return false;
        }
    }



    // 회색 부분의 투명도를 확인하는 함수
    public float CheckGrayPercentage()
    {
        
        if (scratchBlack.activeSelf)    // scratchBlack이 활성화 되어있을 때만 계산  
        {
            float percentage;
            bool allGrayCleared = CheckIfGrayPartsCleared(out percentage);
            // print("회색 부분이 투명해진 퍼센트: " + percentage + "%");

            ResetScratch();            // 점수 산출 시, 재사용 위해 이미지 복구 하기 

            return percentage;
        }

        return -1f;
    }
}
