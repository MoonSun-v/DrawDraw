using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowResult : MonoBehaviour
{
    public PopupManager popupManager; // PopupManager ��ũ��Ʈ�� ������ ����

    public void ShowResultPopup()
    {
        //Debug.Log("��� ����");
        popupManager.Show(); // ��� �˾� ����
    }
}
