using UnityEngine;
using UnityEngine.UI;

public class ScratchBlack : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private ScratchManager scratchManager;

    private SpriteRenderer spriteRenderer;            // ��������Ʈ ������ ������Ʈ
    private Texture2D scratchTexture;                 // ��ũ��ġ �ؽ�ó
    public bool isScratching = false;                 // ��ũ��ġ ������ ����

    private int scratchSize = 20;                     // ��ũ��ġ ���� ũ��
    private Vector2? lastMousePosition = null;        // ������ ���콺 ��ġ ���� : null�� ���
    private bool textureNeedsUpdate = false;          // �ؽ�ó ������Ʈ �÷���

    private Color[] originalColors;                   // ���� ���� �迭

    public GameObject scratchBlack;                   // �ڱ��ڽ� 




    void Awake()
    {
        mainCamera = Camera.main;
    }


    void Start()
    {
         
        spriteRenderer = GetComponent<SpriteRenderer>();            // ��������Ʈ ������ ������Ʈ ��������

        if (spriteRenderer == null)
        {
            Debug.LogWarning("spriteRenderer�� null �Դϴ�.");
        }

       
        scratchTexture = textureFromSprite(spriteRenderer.sprite);   // ��������Ʈ�� ���� �б� ������ ���� ������ �ؽ�ó ���� 

        
        originalColors = scratchTexture.GetPixels();                 // ���� ������ ����


        // ���Ӱ� ������ �ؽ�ó�� �̿��� ���ο� ��������Ʈ�� �����ϰ� ����
        spriteRenderer.sprite = Sprite.Create(scratchTexture, new Rect(0, 0, scratchTexture.width, scratchTexture.height), Vector2.one * 0.5f);

    }

    void Update()
    {
        // ���콺 �Է� ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // �Է� ���콺�� x, y ��ǥ�� ���� ������ ����� X
            if (mousePos.x < scratchManager.Limit_l.position.x || 
                mousePos.x > scratchManager.Limit_R.position.x || 
                mousePos.y < scratchManager.Limit_B.position.y || 
                mousePos.y > scratchManager.Limit_T.position.y)
            {
                return;
            }
            else
            {
                isScratching = true;
                lastMousePosition = null;     // ���콺�� ó�� ���� �� ���� ��ġ �ʱ�ȭ
            }
            
        }
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);

        }
        else if (Input.GetMouseButtonUp(0))
        {

            isScratching = false;
            lastMousePosition = null;        // ���콺�� �� �� ���� ��ġ �ʱ�ȭ

            
            if (textureNeedsUpdate)          // ���콺 ��ư�� �� �� �ؽ�ó�� ����
            {
                scratchTexture.Apply();
                textureNeedsUpdate = false;
            }
        }


        // ���콺 ��ư�� �����ִ� ���� �ֱ������� �ؽ�ó ����
        if (textureNeedsUpdate && Time.frameCount % 5 == 0)
        {
            scratchTexture.Apply();
            textureNeedsUpdate = false;
        }

    }



    // ��ġ�� ��ġ�� �ȼ��� ��������� �����ϴ� �Լ�
    void Scratch(Vector2 touchPosition)
    {
       
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);                         // ��������Ʈ�� ���� ��ǥ�� ��ġ ��ǥ�� ��ȯ
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        
        if (lastMousePosition.HasValue)          // ���� ��ġ�� ���� ��ġ�� �̿��Ͽ� ���� �׸��� �Լ� ȣ��
        {
            DrawLine(lastMousePosition.Value, localTouchPosition);
        }

        lastMousePosition = localTouchPosition;
        textureNeedsUpdate = true;
    }



    // ���� ���� :  �� �� ������ ���� �׸��� �Լ� -> �� �����ϴ� ��� ���� ó���ϱ� ���� (���� ���� ����)
    void DrawLine(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);    // ����-�� �� �Ÿ� ���
        int steps = Mathf.CeilToInt(distance * 5);        // ���� ���� �׸� �ȼ� �� ��� 

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
                
                if (x >= 0 && x < scratchTexture.width && y >= 0 && y < scratchTexture.height)  // �ȼ� ��ǥ�� ��ȿ�� ���� ���� �ִ��� Ȯ��
                {
                    
                    scratchTexture.SetPixel(x, y, Color.clear);                                 // �ȼ� ������ �����(���İ� 0)���� ����
                }
            }
        }
    }



    // ��������Ʈ���� �ؽ�ó�� �����ϴ� �Լ�
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)    // ��������Ʈ�� ũ��� �ؽ�ó�� ũ�Ⱑ �ٸ��� ���ο� �ؽ�ó ����
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
    // (��ũ��ġ ���� �� �ѹ� �� ������Ѽ� �������ƾ� �� !!!!) : �̹��� ���� ��ü�� �ȼ��� ���� ��Ű�� ���̹Ƿ�...
    public void ResetScratch()
    {
        if (scratchBlack.activeSelf)                        // scratchBlack�� Ȱ��ȭ �Ǿ����� ���� ��ũ��ġ ���� 
        {
            scratchTexture.SetPixels(originalColors);       // ���� �������� �ؽ�ó�� ����
            scratchTexture.Apply();
        }
        
    }



    // ȸ�� �κ��� ��� �����ϰ� ���ߴ��� Ȯ���ϴ� �Լ�
    bool CheckIfGrayPartsCleared(out float percentage)
    {
        Color[] currentColors = scratchTexture.GetPixels();
        int totalNonBlackPixels = 0; 
        int clearedNonBlackPixels = 0; 

        for (int i = 0; i < currentColors.Length; i++)      // �������� ������ ��� ������ �����ϰ� ��������� Ȯ��
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
    public float CheckGrayPercentage()
    {
        
        if (scratchBlack.activeSelf)    // scratchBlack�� Ȱ��ȭ �Ǿ����� ���� ���  
        {
            float percentage;
            bool allGrayCleared = CheckIfGrayPartsCleared(out percentage);
            // print("ȸ�� �κ��� �������� �ۼ�Ʈ: " + percentage + "%");

            ResetScratch();            // ���� ���� ��, ���� ���� �̹��� ���� �ϱ� 

            return percentage;
        }

        return -1f;
    }
}
