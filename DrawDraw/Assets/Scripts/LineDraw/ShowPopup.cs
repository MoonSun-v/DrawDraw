using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopup : MonoBehaviour
{
    public CheckpopupManager CheckpopupManager; // PopupManager ��ũ��Ʈ�� ������ ����

    public void ShowCheckPopup()
    {
        CheckpopupManager.check(); // ��� �˾� ����
    }
}
