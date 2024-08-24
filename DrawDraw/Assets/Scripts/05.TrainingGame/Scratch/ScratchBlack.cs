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

    public GameObject scratchBlack;                   // �ڱ� �ڽ� ����




    void Awake()
    {
        mainCamera = Camera.main;
    }



    // �� [ Sprite�� �б�/���� �����ϵ��� ���� ] ��
    // 
    // 1. scratchTexture  :  ��������Ʈ�� �б� �� ���� ������ Texture2D ��ü�� ��ȯ
    //                       ( �⺻ spriteRenderer.sprite �� �б� ���� )
    // 2. originalColors  :  ���� ���� ���� ( ���� �ǵ������� ���� )
    // 3. �ؽ�ó(scratchTexture) �� ������� ���ο� ��������Ʈ ���� -> spriteRenderer�� �ٽ� �Ҵ�
    // 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();          
        if (spriteRenderer == null) { Debug.LogWarning("spriteRenderer�� null �Դϴ�."); }

        scratchTexture = textureFromSprite(spriteRenderer.sprite);   // 1

        originalColors = scratchTexture.GetPixels();                 // 2

        spriteRenderer.sprite = Sprite.Create(                       // 3
            scratchTexture, 
            new Rect(0, 0, scratchTexture.width, scratchTexture.height), 
            Vector2.one * 0.5f);

    }



    // �� [ ��ũ��ġ �۾� ���� ó�� ]
    // 
    // - ���콺 �Է� ó��
    // - ���콺 ��ư�� �����ִ� ���� ���� �����Ӹ��� �ؽ�ó�� ������Ʈ
    // 
    void Update()
    {
        MouseInput();

        if (textureNeedsUpdate && Time.frameCount % 5 == 0)
        {
            ApplyTextureUpdate();
        }
    }



    // �� [ ���콺 �Է� ó�� ] ��
    // 
    // 1. ó�� ������ �� : ȭ�� ��ǥ -> ������ǥ
    //                     ��ũ��ġ ������ ���� ���� �ִ��� Ȯ��
    //                     ( ��ũ��Ī ���� , ���� ��ġ �ʱ�ȭ )
    // 2. ���� ����      : ��ũ��ġ ����
    // 3. ������ ��      : ( ��ũ��Ī ���� , ���� ��ġ �ʱ�ȭ )
    //                   : �ؽ�ó�� ������Ʈ�� �ʿ� ������ ����
    // 
    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (IsWithinBounds(mousePos))
            {
                isScratching = true;  
                lastMousePosition = null; 
            }
        }
        else if (Input.GetMouseButton(0) && isScratching)
        {
            Scratch(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isScratching = false;  
            lastMousePosition = null;  

            ApplyTextureUpdate();
        }
    }



    // �� [ �ؽ�ó ������Ʈ ]
    // 
    // - �ؽ�ó�� ���� ���� ���� , ������Ʈ �÷��� �ʱ�ȭ
    private void ApplyTextureUpdate()
    {
        if (textureNeedsUpdate)
        {
            scratchTexture.Apply();  
            textureNeedsUpdate = false;
        }
    }



    // �� [ �ȼ��� ��������� �����ϴ� �Լ� ]
    //
    // - ��������Ʈ�� ���� ��ǥ�� ��ġ ��ǥ�� ��ȯ
    // - ���� ��ġ�� ���� ��ġ�� �̿��Ͽ� ���� �׸��� �Լ� ȣ��
    // 
    void Scratch(Vector2 touchPosition)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPosition);                        
        Vector2 localTouchPosition = spriteRenderer.transform.InverseTransformPoint(worldPos);

        if (lastMousePosition.HasValue)          
        {
            DrawLine(lastMousePosition.Value, localTouchPosition);
        }

        lastMousePosition = localTouchPosition;
        textureNeedsUpdate = true;
    }



    // �� [ ���� ���� ]
    //
    // �� �� ������ ���� �׸��� �Լ� -> �� �����ϴ� ��� ���� ó���ϱ� ���� (���� ���� ����)
    //
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



    // �� [ Ư�� ��ġ�� ���� �׸��� �Լ� ]
    // 
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



    // �� [ ��������Ʈ���� �ؽ�ó�� �����ϴ� �Լ� ]
    // 
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



    // �� [ ��ũ��ġ ȿ���� �����ϴ� �Լ� ]
    // 
    // (��ũ��ġ ���� �� �ѹ� �� ������Ѽ� �������ƾ� �� !!!!) : �̹��� ���� ��ü�� �ȼ��� ���� ��Ű�� ���̹Ƿ�...
    //
    public void ResetScratch()
    {
        if (scratchBlack.activeSelf)                        // scratchBlack�� Ȱ��ȭ �Ǿ����� ���� ��ũ��ġ ���� 
        {
            scratchTexture.SetPixels(originalColors);       // ���� �������� �ؽ�ó�� ����
            scratchTexture.Apply();
        }
    }



    // �� [ ȸ�� �κ��� ��� �����ϰ� ���ߴ��� Ȯ���ϴ� �Լ� ]
    // 
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



    // �� [ ȸ�� �κ��� ������ Ȯ���ϴ� �Լ� ]
    //
    // ResetScratch() : ���� ���� ��, ���� ���� �̹��� ���� �ϱ�
    // 
    public float CheckGrayPercentage()
    {
        if (scratchBlack.activeSelf)    
        {
            float percentage;
            CheckIfGrayPartsCleared(out percentage);
            // print("ȸ�� �κ��� �������� �ۼ�Ʈ: " + percentage + "%");

            ResetScratch();       

            return percentage;
        }
        return -1f;
    }



    // �� [ �Է� ��ǥ�� ��ũ��ġ ���� ���� �ִ��� Ȯ���ϴ� �Լ� ]
    //
    private bool IsWithinBounds(Vector2 position)
    {
        return position.x >= scratchManager.Limit_l.position.x &&
               position.x <= scratchManager.Limit_R.position.x &&
               position.y >= scratchManager.Limit_B.position.y &&
               position.y <= scratchManager.Limit_T.position.y;
    }
}
