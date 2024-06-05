using UnityEngine;
using UnityEngine.UI;

public class ScratchBlack : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
    private Texture2D scratchTexture; // ��ũ��ġ �ؽ�ó
    private bool isScratching = false; // ��ũ��ġ ������ ����

    private int scratchSize = 15; // ��ũ��ġ ���� ũ��
    private Vector2? lastMousePosition = null; // ������ ���콺 ��ġ ���� : null�� ���
    private bool textureNeedsUpdate = false; // �ؽ�ó ������Ʈ �÷���

    private Color[] originalColors; // ���� ���� �迭

    public GameObject scratchBlack; // �ڱ��ڽ� 

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

        // ���� ������ ����
        originalColors = scratchTexture.GetPixels();

        // ���Ӱ� ������ �ؽ�ó�� �̿��� ���ο� ��������Ʈ�� �����ϰ� ����
        spriteRenderer.sprite = Sprite.Create(scratchTexture, new Rect(0, 0, scratchTexture.width, scratchTexture.height), Vector2.one * 0.5f);

    }

    void Update()
    {
        // ���콺 �Է� ����
        if (Input.GetMouseButtonDown(0))
        {
            isScratching = true;
            lastMousePosition = null; // ���콺�� ó�� ���� �� ���� ��ġ �ʱ�ȭ
        }
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isScratching = false;
            lastMousePosition = null; // ���콺�� �� �� ���� ��ġ �ʱ�ȭ

            // ���콺 ��ư�� �� �� �ؽ�ó�� ����
            if (textureNeedsUpdate)
            {
                scratchTexture.Apply();
                textureNeedsUpdate = false;
            }
        }

        // ���콺 ��ư�� �����ִ� ���� �ֱ������� �ؽ�ó�� ����
        if (textureNeedsUpdate && Time.frameCount % 5 == 0)
        {
            scratchTexture.Apply();
            textureNeedsUpdate = false;
        }
    }

    // ��ġ�� ��ġ�� �ȼ��� ��������� �����ϴ� �Լ�
    void Scratch(Vector2 touchPosition)
    {
        // ��������Ʈ�� ���� ��ǥ�� ��ġ ��ǥ�� ��ȯ
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        // ���� ��ġ�� ���� ��ġ�� �̿��Ͽ� ���� �׸��� �Լ� ȣ��
        if (lastMousePosition.HasValue)
        {
            DrawLine(lastMousePosition.Value, localTouchPosition);
        }

        lastMousePosition = localTouchPosition;
        textureNeedsUpdate = true;
    }

    // ���� ���� :  �� �� ������ ���� �׸��� �Լ�
    // �� �����ϴ� ��� ���� ó���ϱ� ���� (���� ���� ����)
    void DrawLine(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end); // ����-�� �� �Ÿ� ���
        int steps = Mathf.CeilToInt(distance * 5); // ���� ���� �׸� �ȼ� �� ��� 

        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            Vector2 point = Vector2.Lerp(start, end, t);
            DrawCircle(point);
        }
    }

    // Ư�� ��ġ�� ���� �׸��� �Լ�
    void DrawCircle(Vector2 position)
    {
        int startX = Mathf.RoundToInt((position.x + spriteRenderer.bounds.extents.x) * scratchTexture.width / spriteRenderer.bounds.size.x) - scratchSize / 2;
        int startY = Mathf.RoundToInt((position.y + spriteRenderer.bounds.extents.y) * scratchTexture.height / spriteRenderer.bounds.size.y) - scratchSize / 2;

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
    }

    // ��������Ʈ���� �ؽ�ó�� �����ϴ� �Լ�
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        // ��������Ʈ�� ũ��� �ؽ�ó�� ũ�Ⱑ �ٸ��� ���ο� �ؽ�ó ����
        if (sprite.rect.width != sprite.texture.width)
        {
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

    // ��ũ��ġ ȿ���� �����ϴ� �Լ�
    // ��ũ��ġ ��� ���� ���� �� �ѹ� �� ������Ѽ� �ʱ�ȭ ���� ���ƾ� �� 
    public void ResetScratch()
    {
        // scratchBlack�� Ȱ��ȭ �Ǿ����� ���� ��ũ��ġ ���� 
        if (scratchBlack.activeSelf)
        {
            // ���� �������� �ؽ�ó�� ����
            scratchTexture.SetPixels(originalColors);
            scratchTexture.Apply();
        }
        
    }

    // ȸ�� �κ��� ��� �����ϰ� ���ߴ��� Ȯ���ϴ� �Լ�
    bool CheckIfGrayPartsCleared(out float percentage)
    {
        Color[] currentColors = scratchTexture.GetPixels();
        int totalNonBlackPixels = 0; 
        int clearedNonBlackPixels = 0; 

        // �������� ������ ��� ������ �����ϰ� ��������� Ȯ��
        for (int i = 0; i < currentColors.Length; i++)
        {
            if (currentColors[i] != Color.black)
            {
                totalNonBlackPixels++;
                if (currentColors[i].a == 0)
                {
                    clearedNonBlackPixels++;
                }
            }
        }

        if (totalNonBlackPixels > 0)
        {
            percentage = (float)clearedNonBlackPixels / totalNonBlackPixels * 100f;
            return clearedNonBlackPixels == totalNonBlackPixels;
        }
        else
        {
            percentage = 0;
            return false;
        }
    }

    // ȸ�� �κ��� ������ Ȯ���ϴ� �Լ�
    public void CheckGrayPercentage()
    {
        // scratchBlack�� Ȱ��ȭ �Ǿ����� ���� ���  
        if (scratchBlack.activeSelf)
        {
            float percentage;
            bool allGrayCleared = CheckIfGrayPartsCleared(out percentage);
            Debug.Log("ȸ�� �κ��� �������� �ۼ�Ʈ: " + percentage + "%");


        }
            
    }
}
