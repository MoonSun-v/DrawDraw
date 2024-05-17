using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineDrawManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject DrawArea; // 그리기 활성화 영역
    Vector2[] vertices = new Vector2[4]; // 그리기 사각형 영역의 꼭짓점
    public bool DrawActivate=true; // 활성화 여부

    private PopupManager popup;

    void Awake()
    {
        mainCamera = Camera.main;

        // 오브젝트의 Transform 컴포넌트를 가져옴
        Transform rectTransform = DrawArea.GetComponent<Transform>();

        // 사각형의 가로와 세로 크기를 얻음
        Vector2 size = rectTransform.localScale;

        // 사각형의 중심 위치를 얻음
        Vector2 center = rectTransform.position;

        // 사각형의 각 꼭짓점의 상대적인 위치를 계산
        vertices[0] = center + new Vector2(-size.x / 2, -size.y / 2); // 왼쪽 아래 꼭짓점
        vertices[1] = center + new Vector2(size.x / 2, -size.y / 2); // 오른쪽 아래 꼭짓점
        vertices[2] = center + new Vector2(size.x / 2, size.y / 2); // 오른쪽 위 꼭짓점
        vertices[3] = center + new Vector2(-size.x / 2, size.y / 2); // 왼쪽 위 꼭짓점

        //Debug.Log("그리기 영역 꼭짓점 : "+ vertices[0]+vertices[1]+ vertices[2]+ vertices[3]);
    }

    void Update()
    {

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // 입력 마우스의 x, y 좌표가 범위 밖으로 벗어나면 Draw 비활성화 
        if (mousePos.x < vertices[0].x || mousePos.x > vertices[2].x || mousePos.y < vertices[0].y || mousePos.y > vertices[2].y)
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
    }
}
