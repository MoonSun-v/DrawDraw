using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScratchSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
    private Texture2D scratchTexture; // ��ũ��ġ �ؽ�ó
    private bool isScratching = false; // ��ũ��ġ ������ ����

    private int scratchSize = 20; // ��ũ��ġ ���� ũ��

    void Start()
    {
        // ��������Ʈ ������ ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("spriteRenderer�� null �Դϴ�.");
        }

        // ��������Ʈ�� ���� �б� ������ ���� ������ �ؽ�ó ���� 
        scratchTexture = textureFromSprite(spriteRenderer.sprite);

        // ���Ӱ� ������ �ؽ�ó�� �̿��� ���ο� ��������Ʈ�� �����ϰ� ����
        spriteRenderer.sprite = Sprite.Create(scratchTexture, new Rect(0, 0, scratchTexture.width, scratchTexture.height), Vector2.one * 0.5f);


        #region ���� ���� ��� 
        /*
        // ��������Ʈ�� �ؽ�ó�� �����ͼ� ���� ������ �ؽ�ó�� ���
        Texture2D originalTexture = spriteRenderer.sprite.texture;
        scratchTexture = Instantiate(originalTexture); // ���� �ؽ�ó�� �����ؼ� ���
        */

        // ��������Ʈ �������� �ҽ� �ؽ�ó�� ��ũ��ġ �ؽ�ó�� ����
        // spriteRenderer.material.mainTexture = scratchTexture;

        /*
        Color[] pixels = spriteRenderer.sprite.texture.GetPixels();
        print(pixels[3]);
        pixels[3] = new Color(1, 1, 1);
        spriteRenderer.sprite.texture.SetPixels(pixels);
        spriteRenderer.sprite.texture.Apply();
        */
        #endregion
    }

    void Update()
    {
        // ���콺 �Է� ����
        if (Input.GetMouseButtonDown(0))
        {
            // print("���콺 ��ư�� ���Ƚ��ϴ�.");
            isScratching = true;
        }
        // ���콺 �����̴� ���� ��ũ��ġ ����
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);
        }
        // ���콺 ��ư�� ������ �� ��ũ��ġ ����
        else if (Input.GetMouseButtonUp(0))
        {
            isScratching = false;
        }

        #region touchCount ���
        /*
        // ��ġ �Է� ����
        if (Input.touchCount > 0)
        {
            print("��ġ �Է��� �����Ǿ����ϴ�.");
            Touch touch = Input.GetTouch(0);

            // ��ġ ���� �� ��ũ��ġ ����
            if (touch.phase == TouchPhase.Began)
            {
                isScratching = true;
            }
            // ��ġ �̵� ���� �� ��ũ��ġ ����
            else if (touch.phase == TouchPhase.Moved && isScratching)
            {
                Scratch(touch.position);
            }
            // ��ġ ���� �� ��ũ��ġ ����
            else if (touch.phase == TouchPhase.Ended)
            {
                isScratching = false;
            }
        }
        */
        #endregion
    }


    // ��ġ�� ��ġ�� �ȼ��� ��������� �����ϴ� �Լ�
    void Scratch(Vector2 touchPosition)
    {
        // ��������Ʈ�� ���� ��ǥ�� ��ġ ��ǥ�� ��ȯ
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        // ��ġ ��ġ�� �߽����� �� �簢�� ���� ���� �ȼ��� �����ϰ� ����� 
        int startX = Mathf.RoundToInt((localTouchPosition.x + spriteRenderer.bounds.extents.x) * scratchTexture.width / spriteRenderer.bounds.size.x) - scratchSize / 2;
        int startY = Mathf.RoundToInt((localTouchPosition.y + spriteRenderer.bounds.extents.y) * scratchTexture.height / spriteRenderer.bounds.size.y) - scratchSize / 2;

        for (int x = startX; x < startX + scratchSize; x++)
        {
            for (int y = startY; y < startY + scratchSize; y++)
            {
                // �ȼ� ��ǥ�� ��ȿ�� ���� ���� �ִ��� Ȯ��
                if (x >= 0 && x < scratchTexture.width && y >= 0 && y < scratchTexture.height)
                {
                    // �ȼ� ������ �����(���İ� 0)���� ����
                    scratchTexture.SetPixel(x, y, Color.clear);
                }
            }
        }

        // ����� �ȼ� ����
        scratchTexture.Apply();
    }

    // ��������Ʈ���� �ؽ�ó�� �����ϴ� �Լ�
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        // ��������Ʈ�� ũ��� �ؽ�ó�� ũ�Ⱑ �ٸ��� ���ο� �ؽ�ó ����
        if (sprite.rect.width != sprite.texture.width)
        {
            print("��������Ʈ�� �ؽ�ó ũ�Ⱑ �ٸ��ϴ�. ���ο� �ؽ�ó �����մϴ�.");
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
        {
            return sprite.texture;
        }
    }
}
