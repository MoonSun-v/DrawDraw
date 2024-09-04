using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleColoring : MonoBehaviour
{
    public Color crayonColor; // 크레용 버튼의 색상
    public GameObject[] Pieces;
    public GameObject[] Puzzles;

    private Color[] pieceColors;

    private int status = 0; //0:색칠, 1:맞추기

    public GameObject colorboard;
    public GameObject puzzleboard;
    public GameObject crayons;

    public PuzzleMove puzzleMove;

    void Start()
    {
        if (status == 0)
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
    }

    void Update()
    {
        if (status == 0)
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
    }

    public void FinishBtn()
    {
        if (status == 0)
        {
            for (int i = 0; i < Pieces.Length; i++)
            {
                SpriteRenderer spriteRenderer = Pieces[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    pieceColors[i] = spriteRenderer.color;
                }
            }

            colorboard.gameObject.SetActive(false);
            crayons.gameObject.SetActive(false);
            puzzleboard.gameObject.SetActive(true);

            status = 1;

            puzzleMove.enabled = true;
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


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleColoring : MonoBehaviour
{
    public Color crayonColor; // 크레용 버튼의 색상
    private static Color selectedColor = Color.white; // 선택된 색상, 기본은 흰색
    public GameObject[] Pieces; 
    public GameObject[] Puzzles;

    private Color[] pieceColors;

    private int status = 0; //0:색칠,1:맞추기

    private Vector3[] initialPositions;

    public GameObject correctForm;
    private bool isMoving;

    public GameObject colorboard;
    public GameObject puzzleboard;
    public GameObject crayons;

    private float startPosX;
    private float startPosY;

    private int selectedPieceIndex = -1;

    void Start()
    {
        if (status == 0)
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

        if (status == 1)
        {
            for (int i = 0; i < Pieces.Length; i++)
            {
                Pieces[i].transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                initialPositions[i] = Pieces[i].transform.position;
            }
            initialPositions = new Vector3[Pieces.Length];
            
        }
    }

    void Update()
    {
        if (status == 0)
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

        if (status == 1)
        {
            if (isMoving && selectedPieceIndex != -1)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // 선택된 조각과 그에 대응하는 조각을 함께 이동시킴
                Pieces[selectedPieceIndex].transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, Pieces[selectedPieceIndex].transform.position.z);
                Puzzles[selectedPieceIndex].transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, Puzzles[selectedPieceIndex].transform.position.z);
            }
        }
    }

    public void FinishBtn()
    {
        if (status == 0)
        {
            for (int i = 0; i < Pieces.Length; i++)
            {
                SpriteRenderer spriteRenderer = Pieces[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    pieceColors[i] = spriteRenderer.color;
                }
                //Pieces[i].gameObject.SetActive(false);
            }

            Debug.Log("Colors saved:");

            for (int i = 0; i < pieceColors.Length; i++)
            {
                Debug.Log($"Piece {i}: {pieceColors[i]}");
            }

            colorboard.gameObject.SetActive(false);
            crayons.gameObject.SetActive(false);
            puzzleboard.gameObject.SetActive(true);

            status = 1;
        }

        if (status == 1)
        {
            // 퍼즐 맞추기 로직
        }
    }

    private void OnMouseDown()
    {
        if (status == 1 && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                for (int i = 0; i < Pieces.Length; i++)
                {
                    if (hit.collider.gameObject == Pieces[i] || hit.collider.gameObject == Puzzles[i])
                    {
                        selectedPieceIndex = i;

                        startPosX = mousePos.x - Pieces[i].transform.position.x;
                        startPosY = mousePos.y - Pieces[i].transform.position.y;

                        isMoving = true;

                        Debug.Log($"Piece {i} selected, isMoving set to true");

                        break;
                    }
                }
            }
            else
            {
                Debug.Log("No object hit by Raycast");
            }
        }
    }

    private void OnMouseUp()
    {
        if (status == 1)
        {
            isMoving = false;
            selectedPieceIndex = -1;
            Debug.Log("Mouse released, isMoving set to false");
        }
    }

    public void ResetPosition()
    {
        for (int i = 0; i < Pieces.Length; i++)
        {
            Pieces[i].transform.position = initialPositions[i];
            Puzzles[i].transform.position = initialPositions[i];
        }
    }

    public void ColorRedButton(GameObject redCrayon)
    {
        crayonColor = Color.red;
        //updatebuttonposition(redcrayon);
    }

    public void colorOrangebutton(GameObject orangecrayon)
    {
        crayonColor = new Color(1f, 0.5f, 0f); // orange color
        //updatebuttonposition(orangecrayon);
    }

    public void colorYellowbutton(GameObject yellowcrayon)
    {
        crayonColor = Color.yellow;
        //updatebuttonposition(yellowcrayon);
    }

    public void colorGreenbutton(GameObject greencrayon)
    {
        crayonColor = new Color(0f, 0.392f, 0f); // dark green color
        //updatebuttonposition(greencrayon);
    }

    public void colorSkybluebutton(GameObject skybluecrayon)
    {
        crayonColor = new Color(0.529f, 0.808f, 0.922f); // sky blue color
        //updatebuttonposition(skybluecrayon);
    }

    public void colorBluebutton(GameObject bluecrayon)
    {
        crayonColor = Color.blue;
        //updatebuttonposition(bluecrayon);
    }

    public void colorPurplebutton(GameObject purplecrayon)
    {
        crayonColor = new Color(0.859f, 0.439f, 0.576f); // purple color
        //updatebuttonposition(purplecrayon);
    }

    public void Eraserbutton(GameObject eraser)
    {
        crayonColor = Color.white;
        //updatebuttonposition(eraser);
    }
}
*/