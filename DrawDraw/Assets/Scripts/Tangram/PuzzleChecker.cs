using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleChecker : MonoBehaviour
{
    public Tangram[] puzzlePieces;
    public Text completedText; // 완료 메시지

    void Start()
    {
        // 완료 텍스트를 숨감
        if (completedText != null)
        {
            completedText.gameObject.SetActive(false);
        }
    }

    public void OnCheckCompletion()
    {
        bool allInCorrectPosition = true;

        foreach (Tangram piece in puzzlePieces)
        {
            if (!piece.IsInCorrectPosition())
            {
                allInCorrectPosition = false;
                break;
            }
        }

        if (allInCorrectPosition)
        {
            Debug.Log("Puzzle completed successfully!");
            // 완료 텍스트를 표시
            if (completedText != null)
            {
                completedText.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Puzzle is not completed yet.");
        }
    }
}
