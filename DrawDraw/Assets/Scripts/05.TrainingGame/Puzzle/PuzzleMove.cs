using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    public GameObject[] Pieces;
    public GameObject[] Puzzles;

    private Vector3[] initialPositions;
    private bool isMoving;
    private float startPosX;
    private float startPosY;
    private int selectedPieceIndex = -1;

    void Start()
    {
        initialPositions = new Vector3[Pieces.Length];

        for (int i = 0; i < Pieces.Length; i++)
        {
            initialPositions[i] = Pieces[i].transform.position;
        }
    }

    void Update()
    {
        if (isMoving && selectedPieceIndex != -1)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Pieces[selectedPieceIndex].transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, Pieces[selectedPieceIndex].transform.position.z);
            //Puzzles[selectedPieceIndex].transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, Puzzles[selectedPieceIndex].transform.position.z);
        }
    }

    private void OnMouseDown()
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
                    break;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;
        selectedPieceIndex = -1;
    }

    public void ResetPosition()
    {
        for (int i = 0; i < Pieces.Length; i++)
        {
            Pieces[i].transform.position = initialPositions[i];
            //Puzzles[i].transform.position = initialPositions[i];
        }
    }
}
