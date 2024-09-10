using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int status = 0; //0:»öÄ¥, 1:¸ÂÃß±â

    public PuzzleColoring PuzzleColoring;
    public PuzzleMove[] puzzleMoves;

    void Start()
    {
        
    }

    void Update()
    {
        if (status == 0)
        {
            PuzzleColoring.enabled = true;
            SetPuzzleMoveEnabled(false);
        }
        else if (status == 1)
        {
            PuzzleColoring.enabled = false;
            SetPuzzleMoveEnabled(true);
        }
        else
        {
            PuzzleColoring.enabled = false;
            SetPuzzleMoveEnabled(false);
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
