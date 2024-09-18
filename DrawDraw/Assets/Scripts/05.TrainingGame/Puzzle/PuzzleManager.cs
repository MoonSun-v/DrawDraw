using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int status = 0; //0:»öÄ¥, 1:¸ÂÃß±â

    public PuzzleColoring PuzzleColoring;
    public PuzzleMove[] puzzleMoves;

    public GameObject[] button;

    void Start()
    {
        
    }

    void Update()
    {
        if (status == 0)
        {
            PuzzleColoring.enabled = true;
            SetPuzzleMoveEnabled(false);

            button[0].SetActive(true);
            button[1].SetActive(false);
        }
        else if (status == 1)
        {
            PuzzleColoring.enabled = false;
            SetPuzzleMoveEnabled(true);

            button[0].SetActive(false);
            button[1].SetActive(true);
        }
        else
        {
            PuzzleColoring.enabled = false;
            SetPuzzleMoveEnabled(false);

            button[0].SetActive(false);
            button[1].SetActive(false);
        }
    }

    void SetPuzzleMoveEnabled(bool enabled)
    {
        foreach (var puzzleMove in puzzleMoves)
        {
            puzzleMove.enabled = enabled;
        }
    }
}
