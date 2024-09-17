using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleColoring : MonoBehaviour
{
    public Color crayonColor; // 크레용 버튼의 색상
    public GameObject[] Pieces;
    public GameObject[] Puzzles;

    private Color[] pieceColors;

    public GameObject colorboard;
    public GameObject puzzleboard;
    public GameObject crayons;

    public PuzzleManager PuzzleManager;

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
                            spriteRenderer.color = crayonColor;
                        }
                        break;
                    }
                }
            }
        }
    }

    public void FinishBtn()
    {
        if (PuzzleManager.status == 0)
        {
            for (int i = 0; i < Pieces.Length; i++)
            {
                SpriteRenderer spriteRenderer = Pieces[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    pieceColors[i] = spriteRenderer.color;
                }
            }

            float m = 1.7f;

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


                n -= 1.7f;
            }

            colorboard.gameObject.SetActive(false);
            crayons.gameObject.SetActive(false);
            puzzleboard.gameObject.SetActive(true);

            PuzzleManager.status = 1;
        }
    }

    public void ColorRedButton(GameObject redCrayon)
    {
        crayonColor = Color.red;
    }

    public void colorOrangebutton(GameObject orangecrayon)
    {
        crayonColor = new Color(1f, 0.5f, 0f); // orange color
    }

    public void colorYellowbutton(GameObject yellowcrayon)
    {
        crayonColor = Color.yellow;
    }

    public void colorGreenbutton(GameObject greencrayon)
    {
        crayonColor = new Color(0f, 0.392f, 0f); // dark green color
    }

    public void colorSkybluebutton(GameObject skybluecrayon)
    {
        crayonColor = new Color(0.529f, 0.808f, 0.922f); // sky blue color
    }

    public void colorBluebutton(GameObject bluecrayon)
    {
        crayonColor = Color.blue;
    }

    public void colorPurplebutton(GameObject purplecrayon)
    {
        crayonColor = new Color(0.859f, 0.439f, 0.576f); // purple color
    }

    public void Eraserbutton(GameObject eraser)
    {
        crayonColor = Color.white;
    }
}