using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleChecker : MonoBehaviour
{
    public PuzzleMove[] puzzleMoves;

    public GameObject CheckPopup; // Ȯ�� �˾� â
    public GameResultSO gameResult;

    void Start()
    {

    }

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
        SetMoveEnabled("Ignore Raycast");

        // OnBlocker();
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        // �˾� ��Ȱ��ȭ
        CheckPopup.SetActive(false);
        SetMoveEnabled("Parent");

        // OffBlocker();
    }

    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        bool allInCorrectPosition = true;

        foreach (PuzzleMove piece in puzzleMoves)
        {
            if (!piece.IsInCorrectPosition())
            {
                allInCorrectPosition = false;
                break;
            }
        }

        if (allInCorrectPosition)
        {
            print("����");
            gameResult.score = 100; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        }
        else
        {
            print("����");
            gameResult.score = 0; // ���� ���� 
            gameResult.previousScene = SceneManager.GetActiveScene().name; // ���� �� �̸� ����
        }

        // ��� ȭ������ �Ѿ�� 
        StartCoroutine(ResultSceneDelay()); // StartCoroutine( "�޼ҵ��̸�", �Ű����� );
    }

    void SetMoveEnabled(string layer)
    {
        // Puzzle�� ���̾� ����
        foreach (var puzzle in puzzleMoves)
        {
            puzzle.gameObject.layer = LayerMask.NameToLayer(layer); // layer �̸����� ���̾� ����
        }
    }

    IEnumerator ResultSceneDelay()
    {
        // 2 �� �� ����
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ResultScene");
    }
}
