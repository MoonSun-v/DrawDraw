using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineDrawManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject DrawArea; // �׸��� Ȱ��ȭ ����
    //Vector2[] vertices = new Vector2[4]; // �׸��� �簢�� ������ ������
    public bool DrawActivate=true; // Ȱ��ȭ ����

    //private PopupManager popup;
    private SpriteRenderer spriteRenderer;
    private Vector2[] corners;

    public GameObject GameResult; // ������ ��� �˾�


    void Awake()
    {
        mainCamera = Camera.main;

        //// ������Ʈ�� Transform ������Ʈ�� ������
        //Transform rectTransform = DrawArea.GetComponent<Transform>();

        //// �簢���� ���ο� ���� ũ�⸦ ����
        //Vector2 size = rectTransform.localScale;

        //// �簢���� �߽� ��ġ�� ����
        //Vector2 center = rectTransform.position;

        //// �簢���� �� �������� ������� ��ġ�� ���
        //vertices[0] = center + new Vector2(-size.x / 2, -size.y / 2); // ���� �Ʒ� ������
        //vertices[1] = center + new Vector2(size.x / 2, -size.y / 2); // ������ �Ʒ� ������
        //vertices[2] = center + new Vector2(size.x / 2, size.y / 2); // ������ �� ������
        //vertices[3] = center + new Vector2(-size.x / 2, size.y / 2); // ���� �� ������

        //Debug.Log("�׸��� ���� ������ : "+ vertices[0]+vertices[1]+ vertices[2]+ vertices[3]);

        spriteRenderer = DrawArea.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            corners = GetSpriteCorners(spriteRenderer);
            //foreach (Vector3 corner in corners)
            //{
            //    Debug.Log(corner);
            //}
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
        if (mousePos.x < corners[0].x || mousePos.x > corners[1].x || mousePos.y < corners[0].y || mousePos.y > corners[2].y || GameResult.activeSelf == true)
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
