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

    public GameObject scratchBlack;                   // 자기 자신 참조




    void Awake()
    {
        mainCamera = Camera.main;
    }



    // ★ [ Sprite를 읽기/쓰기 가능하도록 변경 ] ★
    // 
    // 1. scratchTexture  :  스프라이트를 읽기 및 쓰기 가능한 Texture2D 객체로 변환
    //                       ( 기본 spriteRenderer.sprite 는 읽기 전용 )
    // 2. originalColors  :  원래 색상 저장 ( 추후 되돌려놓기 위해 )
    // 3. 텍스처(scratchTexture) 를 기반으로 새로운 스프라이트 생성 -> spriteRenderer에 다시 할당
    // 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();          
        if (spriteRenderer == null) { Debug.LogWarning("spriteRenderer가 null 입니다."); }

        scratchTexture = textureFromSprite(spriteRenderer.sprite);   // 1

        originalColors = scratchTexture.GetPixels();                 // 2

        spriteRenderer.sprite = Sprite.Create(                       // 3
            scratchTexture, 
            new Rect(0, 0, scratchTexture.width, scratchTexture.height), 
            Vector2.one * 0.5f);

    }



    // ★ [ 스크래치 작업 로직 처리 ]
    // 
    // - 마우스 입력 처리
    // - 마우스 버튼이 눌려있는 동안 일정 프레임마다 텍스처를 업데이트
    // 
    void Update()
    {
        MouseInput();

        if (textureNeedsUpdate && Time.frameCount % 5 == 0)
        {
            ApplyTextureUpdate();
        }
    }



    // ★ [ 마우스 입력 처리 ] ★
    // 
    // 1. 처음 눌렀을 때 : 화면 좌표 -> 월드좌표
    //                     스크래치 가능한 영역 내에 있는지 확인
    //                     ( 스크래칭 시작 , 이전 위치 초기화 )
    // 2. 누른 상태      : 스크래치 진행
    // 3. 떼었을 때      : ( 스크래칭 종료 , 이전 위치 초기화 )
    //                   : 텍스처가 업데이트될 필요 있으면 적용
    // 
    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (IsWithinBounds(mousePos))
            {
                isScratching = true;  
                lastMousePosition = null; 
            }
        }
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isScratching = false;  
            lastMousePosition = null;  

            ApplyTextureUpdate();
        }
    }



    // ★ [ 텍스처 업데이트 ]
    // 
    // - 텍스처에 변경 사항 적용 , 업데이트 플래그 초기화
    private void ApplyTextureUpdate()
    {
        if (textureNeedsUpdate)
        {
            scratchTexture.Apply();  
            textureNeedsUpdate = false;
        }
    }



    // ★ [ 픽셀을 투명색으로 변경하는 함수 ]
    //
    // - 스프라이트의 로컬 좌표를 터치 좌표로 변환
    // - 현재 위치와 이전 위치를 이용하여 선을 그리는 함수 호출
    // 
    void Scratch(Vector2 touchPosition)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);                        
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        if (lastMousePosition.HasValue)          
        {
            DrawLine(lastMousePosition.Value, localTouchPosition);
        }

        lastMousePosition = localTouchPosition;
        textureNeedsUpdate = true;
    }



    // ★ [ 선형 보간 ]
    //
    // 두 점 사이의 선을 그리는 함수 -> 선 구성하는 모든 점을 처리하기 위해 (끊김 현상 개선)
    //
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



    // ★ [ 특정 위치에 원을 그리는 함수 ]
    // 
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



    // ★ [ 스프라이트에서 텍스처를 추출하는 함수 ]
    // 
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



    // ★ [ 스크래치 효과를 리셋하는 함수 ]
    // 
    // (스크래치 끝난 후 한번 더 실행시켜서 돌려놓아야 함 !!!!) : 이미지 파일 자체의 픽셀을 변경 시키는 것이므로...
    //
    public void ResetScratch()
    {
        if (scratchBlack.activeSelf)                        // scratchBlack이 활성화 되어있을 때만 스크래치 리셋 
        {
            scratchTexture.SetPixels(originalColors);       // 원래 색상으로 텍스처를 복원
            scratchTexture.Apply();
        }
    }



    // ★ [ 회색 부분이 모두 투명하게 변했는지 확인하는 함수 ]
    // 
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



    // ★ [ 회색 부분의 투명도를 확인하는 함수 ]
    //
    // ResetScratch() : 점수 산출 시, 재사용 위해 이미지 복구 하기
    // 
    public float CheckGrayPercentage()
    {
        if (scratchBlack.activeSelf)    
        {
            float percentage;
            CheckIfGrayPartsCleared(out percentage);
            // print("회색 부분이 투명해진 퍼센트: " + percentage + "%");

            ResetScratch();       

            return percentage;
        }
        return -1f;
    }



    // ★ [ 입력 좌표가 스크래치 영역 내에 있는지 확인하는 함수 ]
    //
    private bool IsWithinBounds(Vector2 position)
    {
        return position.x >= scratchManager.Limit_l.position.x &&
               position.x <= scratchManager.Limit_R.position.x &&
               position.y >= scratchManager.Limit_B.position.y &&
               position.y <= scratchManager.Limit_T.position.y;
    }
}
