using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ���� ��ư Ŭ�� �Լ� (��� ��ư�� ���� ���������� ��� ����)
    public void SelectColor(GameObject crayon, Color selectedColor)
    {
        crayonColor = selectedColor;

        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;  // ���� ��ư ��ġ ����
        }

        RectTransform rt = crayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition;
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = crayon;
    }

    // ���� ���� ��ư Ŭ�� �̺�Ʈ
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, Color.red);
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(1f, 0.5f, 0f)); // ��Ȳ��
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, Color.yellow);
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0f, 0.392f, 0f)); // £�� �ʷϻ�
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.529f, 0.808f, 0.922f)); // �ϴû�
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, Color.blue);
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.859f, 0.439f, 0.576f)); // �����
    }

    public void EraserButton(GameObject eraser)
    {
        SelectColor(eraser, Color.white); // ���찳�� ���
    }
}
