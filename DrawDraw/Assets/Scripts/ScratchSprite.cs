using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScratchSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트
    private Texture2D scratchTexture; // 스크레치 텍스처
    private bool isScratching = false; // 스크레치 중인지 여부

    private int scratchSize = 20; // 스크레치 영역 크기

    void Start()
    {
        // 스프라이트 렌더러 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("spriteRenderer가 null 입니다.");
        }

        // 스프라이트로 부터 읽기 가능한 직접 변경할 텍스처 생성 
        scratchTexture = textureFromSprite(spriteRenderer.sprite);

        // 새롭게 생성한 텍스처를 이용해 새로운 스프라이트를 생성하고 설정
        spriteRenderer.sprite = Sprite.Create(scratchTexture, new Rect(0, 0, scratchTexture.width, scratchTexture.height), Vector2.one * 0.5f);


        #region 직접 변경 방법 
        /*
        // 스프라이트의 텍스처를 가져와서 직접 변경할 텍스처로 사용
        Texture2D originalTexture = spriteRenderer.sprite.texture;
        scratchTexture = Instantiate(originalTexture); // 기존 텍스처를 복제해서 사용
        */

        // 스프라이트 렌더러의 소스 텍스처를 스크레치 텍스처로 변경
        // spriteRenderer.material.mainTexture = scratchTexture;

        /*
        Color[] pixels = spriteRenderer.sprite.texture.GetPixels();
        print(pixels[3]);
        pixels[3] = new Color(1, 1, 1);
        spriteRenderer.sprite.texture.SetPixels(pixels);
        spriteRenderer.sprite.texture.Apply();
        */
        #endregion
    }

    void Update()
    {
        // 마우스 입력 감지
        if (Input.GetMouseButtonDown(0))
        {
            // print("마우스 버튼이 눌렸습니다.");
            isScratching = true;
        }
        // 마우스 움직이는 동안 스크레치 적용
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);
        }
        // 마우스 버튼이 떼졌을 때 스크레치 종료
        else if (Input.GetMouseButtonUp(0))
        {
            isScratching = false;
        }

        #region touchCount 사용
        /*
        // 터치 입력 감지
        if (Input.touchCount > 0)
        {
            print("터치 입력이 감지되었습니다.");
            Touch touch = Input.GetTouch(0);

            // 터치 시작 시 스크레치 시작
            if (touch.phase == TouchPhase.Began)
            {
                isScratching = true;
            }
            // 터치 이동 중일 때 스크레치 적용
            else if (touch.phase == TouchPhase.Moved && isScratching)
            {
                Scratch(touch.position);
            }
            // 터치 종료 시 스크레치 종료
            else if (touch.phase == TouchPhase.Ended)
            {
                isScratching = false;
            }
        }
        */
        #endregion
    }


    // 터치한 위치의 픽셀을 투명색으로 변경하는 함수
    void Scratch(Vector2 touchPosition)
    {
        // 스프라이트의 로컬 좌표를 터치 좌표로 변환
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        // 터치 위치를 중심으로 한 사각형 영역 내의 픽셀을 투명하게 만들기 
        int startX = Mathf.RoundToInt((localTouchPosition.x + spriteRenderer.bounds.extents.x) * scratchTexture.width / spriteRenderer.bounds.size.x) - scratchSize / 2;
        int startY = Mathf.RoundToInt((localTouchPosition.y + spriteRenderer.bounds.extents.y) * scratchTexture.height / spriteRenderer.bounds.size.y) - scratchSize / 2;

        for (int x = startX; x < startX + scratchSize; x++)
        {
            for (int y = startY; y < startY + scratchSize; y++)
            {
                // 픽셀 좌표가 유효한 범위 내에 있는지 확인
                if (x >= 0 && x < scratchTexture.width && y >= 0 && y < scratchTexture.height)
                {
                    // 픽셀 색상을 투명색(알파값 0)으로 변경
                    scratchTexture.SetPixel(x, y, Color.clear);
                }
            }
        }

        // 변경된 픽셀 적용
        scratchTexture.Apply();
    }

    // 스프라이트에서 텍스처를 추출하는 함수
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        // 스프라이트의 크기와 텍스처의 크기가 다르면 새로운 텍스처 생성
        if (sprite.rect.width != sprite.texture.width)
        {
            print("스프라이트와 텍스처 크기가 다릅니다. 새로운 텍스처 생성합니다.");
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
}
