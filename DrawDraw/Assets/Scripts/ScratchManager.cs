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

    public SpriteRenderer spriteRenderer; // ��������Ʈ ������
    private Texture2D texture; // ��������Ʈ�� �ؽ�ó


    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        /*
        // ���� ��������Ʈ�� �ؽ�ó ��������
        Texture2D originalTexture = spriteRenderer.sprite.texture;

        // �б�/���� ������ ���ο� �ؽ�ó ����
        texture = new Texture2D(originalTexture.width, originalTexture.height, originalTexture.format, false);
        texture.filterMode = originalTexture.filterMode;
        texture.wrapMode = originalTexture.wrapMode;
        texture.SetPixels(originalTexture.GetPixels());
        texture.Apply();

        // ��������Ʈ �������� �� �ؽ�ó�� ����
        spriteRenderer.sprite = Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        */
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


        #region 2. ��ũ��ġ ����
        /*
        if (Input.GetMouseButton(0))
        {
            // ���콺 ��ġ�� �ؽ�ó �������� ��ǥ�� ��ȯ
            Vector2 mousePos = Input.mousePosition;
            Vector2Int pixelPos = GetMousePixelPosition(mousePos);

            // ��ũ��ġ �ݰ� ���� �ȼ��� �����ϰ� �����
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
