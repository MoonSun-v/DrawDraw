using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultPopupManager : MonoBehaviour
{
    public Text Text_GameResult; // ������ ����� ǥ������ Text Ui
    public Text ScoreText; // ������ ����� ������ Text Ui\

    public void Show()
    {

        Text_GameResult.text = ScoreText.text+"�� ������ϴ�."; // �˾��� ���� â�� ���� ������ ǥ���Ѵ�.
        transform.gameObject.SetActive(true); // ��� �˾� â�� ȭ�鿡 ǥ��
        //DrawArea.SetDrawActivate(false);
    }
}
