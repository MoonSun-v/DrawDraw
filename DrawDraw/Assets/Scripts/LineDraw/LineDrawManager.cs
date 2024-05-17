using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LineDrawManager : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject DrawArea; // �׸��� Ȱ��ȭ ����
    Vector2[] vertices = new Vector2[4]; // �׸��� �簢�� ������ ������
    public bool DrawActivate=true; // Ȱ��ȭ ����

    private PopupManager popup;

    void Awake()
    {
        mainCamera = Camera.main;

        // ������Ʈ�� Transform ������Ʈ�� ������
        Transform rectTransform = DrawArea.GetComponent<Transform>();

        // �簢���� ���ο� ���� ũ�⸦ ����
        Vector2 size = rectTransform.localScale;

        // �簢���� �߽� ��ġ�� ����
        Vector2 center = rectTransform.position;

        // �簢���� �� �������� ������� ��ġ�� ���
        vertices[0] = center + new Vector2(-size.x / 2, -size.y / 2); // ���� �Ʒ� ������
        vertices[1] = center + new Vector2(size.x / 2, -size.y / 2); // ������ �Ʒ� ������
        vertices[2] = center + new Vector2(size.x / 2, size.y / 2); // ������ �� ������
        vertices[3] = center + new Vector2(-size.x / 2, size.y / 2); // ���� �� ������

        //Debug.Log("�׸��� ���� ������ : "+ vertices[0]+vertices[1]+ vertices[2]+ vertices[3]);
    }

    void Update()
    {

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // �Է� ���콺�� x, y ��ǥ�� ���� ������ ����� Draw ��Ȱ��ȭ 
        if (mousePos.x < vertices[0].x || mousePos.x > vertices[2].x || mousePos.y < vertices[0].y || mousePos.y > vertices[2].y)
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
}
