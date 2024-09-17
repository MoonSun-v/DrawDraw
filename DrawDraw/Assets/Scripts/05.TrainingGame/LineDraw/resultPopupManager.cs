using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resultPopupManager : MonoBehaviour
{
    public Text Text_GameResult; // ������ ����� ǥ������ Text Ui
    public Text ScoreText; // ������ ����� ������ Text Ui

    //private GameResultSO gameResult; // ���� ���ȭ�� ���� SO
    //private EdgeCollider2D edgeCollider;

    

    public void Show()
    {
        transform.gameObject.SetActive(true); // ��� �˾� â�� ȭ�鿡 ǥ��

        if (CollisionCounter.Instance.IsSafe())
        {
            Text_GameResult.text = ScoreText.text + "�� ������ϴ�."; // �˾��� ���� â�� ���� ������ ǥ���Ѵ�.
        }
        else
        {
            Text_GameResult.text = "�ر׸��ۿ��� �׷������ϴ�.";
        }
    }
}
