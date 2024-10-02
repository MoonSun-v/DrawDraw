using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleColoring : MonoBehaviour
{
    public Color crayonColor; // ũ���� ��ư�� ����
    public GameObject[] Pieces;
    public GameObject[] Puzzles;

    private Color[] pieceColors;

    public GameObject colorboard;
    public GameObject puzzleboard;
    public GameObject crayons;

    public PuzzleManager PuzzleManager;
    public Color puzzleColor = new Color(0.8f, 0.8f, 0.8f);

    public GameObject CheckPopup;

    // [ ���� ���� ���� ���� ]
    public GameObject previousButton;                 // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition;    // ���� ��ư�� ���� ��ġ�� ����
    private const int CrayonMove = 90;                // ��ư �̵� �Ÿ�

    private GameObject currentCrayon;                 // ���� ���õ� ũ����

    public Sprite[] selectedSprites; // ���õ� ��������Ʈ�� ������ �迭
    public Sprite previousButtonOriginalSprite;       // ���� ��ư�� ���� ��������Ʈ�� ����


    void Start()
    {
        pieceColors = new Color[Pieces.Length];

        foreach (GameObject piece in Pieces)
        {
            if (piece.GetComponent<Collider2D>() == null)
            {
                piece.AddComponent<PolygonCollider2D>();
            }
        }

        for (int i = 0; i < Puzzles.Length; i++)
        {
            SpriteRenderer puzzleRenderer = Puzzles[i].GetComponent<SpriteRenderer>();
            if (puzzleRenderer != null)
            {
                puzzleRenderer.color = puzzleColor;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                foreach (GameObject piece in Pieces)
                {
                    if (hit.collider.gameObject == piece)
                    {
                        SpriteRenderer spriteRenderer = piece.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            // ������ ũ���� ���� ����
                            Color newColor = crayonColor;
                            newColor.a = 1f;
                            spriteRenderer.color = newColor;
                        }
                        break;
                    }
                }
            }
        }
    }

    public void ResetBtn()
    {
        // Pieces �迭�� �ִ� ��� ���� �������� ������ �Ͼ������ ����
        foreach (GameObject piece in Pieces)
        {
            SpriteRenderer pieceRenderer = piece.GetComponent<SpriteRenderer>();
            if (pieceRenderer != null)
            {
                pieceRenderer.color = Color.white; // ������ �Ͼ������ ����
            }
        }
    }

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }

    // �˾� : �ϼ��̾�
    public void NextBtn()
    {
        CheckPopup.SetActive(false);

        if (PuzzleManager.status == 0)
        {
            // ���� ���� ���� ����
            for (int i = 0; i < Pieces.Length; i++)
            {
                SpriteRenderer spriteRenderer = Pieces[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    pieceColors[i] = spriteRenderer.color;
                }
            }

            float m = 1.7f;

            // ���� ���� ��ġ�� ũ�� ����
            for (int i = 0; i < Pieces.Length; i++)
            {
                Pieces[i].transform.localScale *= 0.5f;
                Pieces[i].transform.position = new Vector3(5.4f, m, Pieces[i].transform.position.z);
                m -= 1.7f;
            }

            float n = 1.7f;

            for (int i = 0; i < Puzzles.Length; i++)
            {
                Puzzles[i].transform.localScale *= 0.5f;
                Puzzles[i].transform.position = new Vector3(5.4f, n, Puzzles[i].transform.position.z);

                SpriteRenderer puzzleRenderer = Puzzles[i].GetComponent<SpriteRenderer>();
                if (puzzleRenderer != null)
                {
                    puzzleRenderer.color = Color.white;
                }

                n -= 1.7f;
            }

            colorboard.SetActive(false);
            crayons.SetActive(false);
            puzzleboard.SetActive(true);

            PuzzleManager.status = 1;
        }
    }

    public void SelectColor(GameObject crayon, Color selectedColor, int spriteIndex)
    {
        crayonColor = selectedColor;

        // ������ ������ ��ư�� �ִٸ� ���� �̹����� ����
        if (previousButton != null)
        {
            Image prevImage = previousButton.GetComponent<Image>();
            prevImage.sprite = previousButtonOriginalSprite;  // ���� ��ư ��������Ʈ ����
            prevImage.SetNativeSize(); // ���� ��ư�� ũ�⸦ ���� �̹��� ũ��� ����
        }

        // ���� ���õ� ũ������ �̹����� ����
        Image currentImage = crayon.GetComponent<Image>();
        previousButtonOriginalSprite = currentImage.sprite;  // ���� ��������Ʈ ����
        currentImage.sprite = selectedSprites[spriteIndex];  // ���õ� ��������Ʈ�� ����
        currentImage.SetNativeSize(); // ���õ� ũ���� ��ư�� ũ�⸦ ���� �̹��� ũ��� ����

        previousButton = crayon;  // ���õ� ��ư�� ����
    }

    // ���� ����Ʈ ��ư Ŭ�� �̺�Ʈ
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, new Color(0.8901961f, 0.01568628f, 0.01960784f), 0); // ������ ��������Ʈ�� �迭�� ù ��° ���
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(0.9411765f, 0.5294118f, 0.04705882f), 1); // ��Ȳ�� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, new Color(0.945098f, 0.8431373f, 0.07058824f), 2); // ����� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0.2313726f, 0.6117647f, 0f), 3); // £�� �ʷϻ� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.007843138f, 0.5215687f, 0.9960784f), 4); // �ϴû� ��������Ʈ�� �迭�� �ټ� ��° ���
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, new Color(0.1803922f, 0.2f, 0.8431373f), 5); // �Ķ��� ��������Ʈ�� �迭�� ���� ��° ���
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.3529412f, 0.0627451f, 0.7333333f), 6); // ����� ��������Ʈ�� �迭�� �ϰ� ��° ���
    }
}
