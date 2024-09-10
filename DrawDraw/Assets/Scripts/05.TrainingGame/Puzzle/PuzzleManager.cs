using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int status = 0; //0:»öÄ¥, 1:¸ÂÃß±â

    public PuzzleColoring PuzzleColoring;
    public PuzzleMove PuzzleMove;

    void Start()
    {
        
    }

    void Update()
    {
        if (status == 0)
        {
            PuzzleColoring.enabled = true;
            PuzzleMove.enabled = false;
        }
        else if (status == 1)
        {
            PuzzleColoring.enabled = false;
            PuzzleMove.enabled = true;
        }
    }
}
