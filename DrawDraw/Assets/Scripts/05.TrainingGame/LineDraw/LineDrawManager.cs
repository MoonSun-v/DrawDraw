using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineDrawManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject DrawArea; // 그리기 활성화 영역
    public bool DrawActivate = true; // 활성화 여부

    //private PopupManager popup;
    private SpriteRenderer spriteRenderer;
    private Vector2[] corners;

    public GameObject check; // 게임의 확인창 팝업

    // 비활성화할 스크립트에 대한 참조
    public MonoBehaviour CollisionCounter;

    void Awake()
    {
        mainCamera = Camera.main;

        spriteRenderer = DrawArea.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            corners = GetSpriteCorners(spriteRenderer);
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found!");
        }
    }

    void Update()
    {

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 Draw 비활성화 
        if (mousePos.x < corners[0].x || mousePos.x > corners[1].x || mousePos.y < corners[0].y || mousePos.y > corners[2].y)
        {
            SetDrawActivate(false);
        }
        else if (check.activeSelf == true)
        {
            SetDrawActivate(false);

        }
        else // 그리기 영역 안에 있으면 Draw 활성화
        {
            SetDrawActivate(true);

        }
    }

    public void SetDrawActivate(bool isActivate)
    {
        DrawActivate = isActivate;
        CollisionCounter.enabled = isActivate;
    }

    Vector2[] GetSpriteCorners(SpriteRenderer spriteRenderer)
    {
        // 스프라이트의 경계 상자
        Bounds bounds = spriteRenderer.bounds;

        // 경계 상자의 중심과 크기
        Vector2 center = bounds.center;
        Vector2 extents = bounds.extents;

        // 꼭짓점 계산
        Vector2[] corners = new Vector2[4];
        corners[0] = new Vector3(center.x - extents.x, center.y - extents.y); // Bottom Left
        corners[1] = new Vector3(center.x + extents.x, center.y - extents.y); // Bottom Right
        corners[2] = new Vector3(center.x - extents.x, center.y + extents.y); // Top Left
        corners[3] = new Vector3(center.x + extents.x, center.y + extents.y); // Top Right

        return corners;
    }

}
