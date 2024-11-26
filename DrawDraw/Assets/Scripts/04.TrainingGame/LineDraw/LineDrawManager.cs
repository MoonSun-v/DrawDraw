using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineDrawManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject DrawArea; // �׸��� Ȱ��ȭ ����
    public bool DrawActivate = true; // Ȱ��ȭ ����

    //private PopupManager popup;
    private SpriteRenderer spriteRenderer;
    private Vector2[] corners;

    public GameObject check; // ������ Ȯ��â �˾�

    // ��Ȱ��ȭ�� ��ũ��Ʈ�� ���� ����
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


        // �Է� ���콺�� x, y ��ǥ�� ���� ������ ����� Draw ��Ȱ��ȭ 
        if (mousePos.x < corners[0].x || mousePos.x > corners[1].x || mousePos.y < corners[0].y || mousePos.y > corners[2].y)
        {
            SetDrawActivate(false);
        }
        else if (check.activeSelf == true)
        {
            SetDrawActivate(false);

        }
        else // �׸��� ���� �ȿ� ������ Draw Ȱ��ȭ
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
        // ��������Ʈ�� ��� ����
        Bounds bounds = spriteRenderer.bounds;

        // ��� ������ �߽ɰ� ũ��
        Vector2 center = bounds.center;
        Vector2 extents = bounds.extents;

        // ������ ���
        Vector2[] corners = new Vector2[4];
        corners[0] = new Vector3(center.x - extents.x, center.y - extents.y); // Bottom Left
        corners[1] = new Vector3(center.x + extents.x, center.y - extents.y); // Bottom Right
        corners[2] = new Vector3(center.x - extents.x, center.y + extents.y); // Top Left
        corners[3] = new Vector3(center.x + extents.x, center.y + extents.y); // Top Right

        return corners;
    }

}
