using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleChecker : MonoBehaviour
{
    public Tangram[] puzzlePieces;
    public Text completedText; // �Ϸ� �޽���

    void Start()
    {
        // �Ϸ� �ؽ�Ʈ�� ����
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
            // �Ϸ� �ؽ�Ʈ�� ǥ��
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
