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


    // 스크래치를 적용할 레이어와 배경을 표시할 레이어
    public LayerMask scratchLayer;
    public LayerMask backgroundLayer;

    // 스크래치를 수행할 카메라
    public Camera scratchCamera;

    // 스크래치 가능한 텍스처
    public Texture2D scratchTexture; // ㅇ거 몽미

    // 스크래치 반지름
    public float scratchRadius = 10f;

    void Awake()
    {
        mainCamera = Camera.main;
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

        /*
        if (Input.GetMouseButton(0))
        {
            // 마우스 위치를 텍스처 내에서의 좌표로 변환
            Vector2 scratchPosition = GetMousePositionInTexture();

            // 스크래치 레이어에 스크래치를 적용
            Scratch(scratchPosition);
        }
        */
    }
    /*
    private void Scratch(Vector2 position)
    {
        // 스크래치 반경 내의 픽셀을 투명하게 만듦
        for (int x = (int)(position.x - scratchRadius); x < position.x + scratchRadius; x++)
        {
            for (int y = (int)(position.y - scratchRadius); y < position.y + scratchRadius; y++)
            {
                // 스크래치 레이어에만 스크래치 적용
                if (IsWithinLayer(new Vector2(x, y), scratchLayer))
                {
                    // 스크래치 위치 주변의 픽셀을 투명하게 만드는 함수 호출
                    SetPixelTransparent(scratchTexture, x, y);
                }
            }
        }
        scratchTexture.Apply(); // 변경된 텍스처 적용
    }

    // 마우스 위치를 텍스처 내에서의 좌표로 변환하는 함수
    private Vector2 GetMousePositionInTexture()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPosition = scratchCamera.ScreenToWorldPoint(mousePosition);
        return new Vector2(Mathf.Clamp(localPosition.x, 0, scratchCamera.pixelWidth), Mathf.Clamp(localPosition.y, 0, scratchCamera.pixelHeight));
    }

    // 주어진 위치가 특정 레이어에 속하는지 확인하는 함수
    private bool IsWithinLayer(Vector2 position, LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f, layer);
        return hit.collider != null;
    }

    // 특정 픽셀을 투명하게 만드는 함수
    private void SetPixelTransparent(Texture2D texture, int x, int y)
    {
        Color transparentColor = new Color(0, 0, 0, 0); // 투명한 색상
        texture.SetPixel(x, y, transparentColor); // 해당 픽셀을 투명하게 설정
    }
    */
}
