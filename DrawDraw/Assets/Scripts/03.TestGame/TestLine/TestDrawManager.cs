using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDrawManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject DrawArea; // 그리기 활성화 영역
    private SpriteRenderer spriteRenderer;
    private Vector2[] corners;

    public bool DrawActivate = true; // 활성화 여부

    public Image Fighting;
    public Sprite DogFighting;
    private bool isDog;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        isDog = !GameData.instance.playerdata.PlayerCharacter;  // 강아지면 true, 고양이면 false
        if (isDog) { Fighting.sprite = DogFighting; }

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

    private Vector2[] GetSpriteCorners(SpriteRenderer spriteRenderer)
    {
        // 스프라이트의 경계 계산
        Vector2[] corners = new Vector2[4];
        Bounds bounds = spriteRenderer.bounds;

        corners[0] = new Vector2(bounds.min.x, bounds.min.y); // Bottom-left
        corners[1] = new Vector2(bounds.max.x, bounds.min.y); // Bottom-right
        corners[2] = new Vector2(bounds.max.x, bounds.max.y); // Top-right
        corners[3] = new Vector2(bounds.min.x, bounds.max.y); // Top-left

        return corners;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 Draw 비활성화 
        if (mousePos.x < corners[0].x || mousePos.x > corners[1].x || mousePos.y < corners[0].y || mousePos.y > corners[2].y)
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
        //CollisionCounter.enabled = isActivate;
    }
}
