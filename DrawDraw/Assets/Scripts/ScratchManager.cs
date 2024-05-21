using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchManager : MonoBehaviour
{
    private Camera mainCamera;

    public Transform Limit_l;
    public Transform Limit_R;
    public Transform Limit_T;
    public Transform Limit_B;

    [SerializeField]
    private ScratchDraw scratchdraw;

    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러
    private Texture2D texture; // 스프라이트의 텍스처


    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        /*
        // 기존 스프라이트의 텍스처 가져오기
        Texture2D originalTexture = spriteRenderer.sprite.texture;

        // 읽기/쓰기 가능한 새로운 텍스처 생성
        texture = new Texture2D(originalTexture.width, originalTexture.height, originalTexture.format, false);
        texture.filterMode = originalTexture.filterMode;
        texture.wrapMode = originalTexture.wrapMode;
        texture.SetPixels(originalTexture.GetPixels());
        texture.Apply();

        // 스프라이트 렌더러에 새 텍스처를 적용
        spriteRenderer.sprite = Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        */
    }


    void Update()
    {
        #region 1. 그리기 영역 제한
        
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 Draw 비활성화 
        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x || mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if(scratchdraw.iscurrentLineRenderer())
            {
                // scratchdraw.currentLineRenderer = null; // 현재 그리는 선 종료
                scratchdraw.FinishLineRenderer(); // 현재 그리는 선 종료
            }

            scratchdraw.enabled = false;
        }
        else
        {
            scratchdraw.enabled = true;
        }
        
        #endregion


        #region 2. 스크래치 구현
        /*
        if (Input.GetMouseButton(0))
        {
            // 마우스 위치를 텍스처 내에서의 좌표로 변환
            Vector2 mousePos = Input.mousePosition;
            Vector2Int pixelPos = GetMousePixelPosition(mousePos);

            // 스크래치 반경 내의 픽셀을 투명하게 만들기
            int scratchRadius = 10;
            for (int x = pixelPos.x - scratchRadius; x < pixelPos.x + scratchRadius; x++)
            {
                for (int y = pixelPos.y - scratchRadius; y < pixelPos.y + scratchRadius; y++)
                {
                    if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                    {
                        texture.SetPixel(x, y, Color.clear);
                    }
                }
            }
            texture.Apply();
        }
        */
        #endregion

    }

    Vector2Int GetMousePixelPosition(Vector2 mousePos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 localPos = spriteRenderer.transform.InverseTransformPoint(worldPos);

        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        int x = Mathf.RoundToInt(localPos.x * pixelsPerUnit + texture.width / 2);
        int y = Mathf.RoundToInt(localPos.y * pixelsPerUnit + texture.height / 2);

        return new Vector2Int(x, y);
    }

}
