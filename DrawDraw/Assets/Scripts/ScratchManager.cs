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


    // ��ũ��ġ�� ������ ���̾�� ����� ǥ���� ���̾�
    public LayerMask scratchLayer;
    public LayerMask backgroundLayer;

    // ��ũ��ġ�� ������ ī�޶�
    public Camera scratchCamera;

    // ��ũ��ġ ������ �ؽ�ó
    public Texture2D scratchTexture; // ���� ����

    // ��ũ��ġ ������
    public float scratchRadius = 10f;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        #region 1. �׸��� ���� ����

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // �Է� ���콺�� x, y ��ǥ�� ���� ������ ����� Draw ��Ȱ��ȭ 
        if (mousePos.x < Limit_l.position.x || mousePos.x > Limit_R.position.x || mousePos.y < Limit_B.position.y || mousePos.y > Limit_T.position.y)
        {
            if(scratchdraw.iscurrentLineRenderer())
            {
                // scratchdraw.currentLineRenderer = null; // ���� �׸��� �� ����
                scratchdraw.FinishLineRenderer(); // ���� �׸��� �� ����
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
            // ���콺 ��ġ�� �ؽ�ó �������� ��ǥ�� ��ȯ
            Vector2 scratchPosition = GetMousePositionInTexture();

            // ��ũ��ġ ���̾ ��ũ��ġ�� ����
            Scratch(scratchPosition);
        }
        */
    }
    /*
    private void Scratch(Vector2 position)
    {
        // ��ũ��ġ �ݰ� ���� �ȼ��� �����ϰ� ����
        for (int x = (int)(position.x - scratchRadius); x < position.x + scratchRadius; x++)
        {
            for (int y = (int)(position.y - scratchRadius); y < position.y + scratchRadius; y++)
            {
                // ��ũ��ġ ���̾�� ��ũ��ġ ����
                if (IsWithinLayer(new Vector2(x, y), scratchLayer))
                {
                    // ��ũ��ġ ��ġ �ֺ��� �ȼ��� �����ϰ� ����� �Լ� ȣ��
                    SetPixelTransparent(scratchTexture, x, y);
                }
            }
        }
        scratchTexture.Apply(); // ����� �ؽ�ó ����
    }

    // ���콺 ��ġ�� �ؽ�ó �������� ��ǥ�� ��ȯ�ϴ� �Լ�
    private Vector2 GetMousePositionInTexture()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPosition = scratchCamera.ScreenToWorldPoint(mousePosition);
        return new Vector2(Mathf.Clamp(localPosition.x, 0, scratchCamera.pixelWidth), Mathf.Clamp(localPosition.y, 0, scratchCamera.pixelHeight));
    }

    // �־��� ��ġ�� Ư�� ���̾ ���ϴ��� Ȯ���ϴ� �Լ�
    private bool IsWithinLayer(Vector2 position, LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f, layer);
        return hit.collider != null;
    }

    // Ư�� �ȼ��� �����ϰ� ����� �Լ�
    private void SetPixelTransparent(Texture2D texture, int x, int y)
    {
        Color transparentColor = new Color(0, 0, 0, 0); // ������ ����
        texture.SetPixel(x, y, transparentColor); // �ش� �ȼ��� �����ϰ� ����
    }
    */
}
