using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPop : MonoBehaviour
{
    public GameObject check_popup;
    public GameObject check_popup1;

    public void onCheckPop() // Ȯ��â ����
    {
        check_popup.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
    }
    public void CloseCheckPop()
    {
        check_popup.SetActive(false);
    }
    public void onCheckPop1() // Ȯ��â ����
    {
        check_popup1.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
    }
    public void CloseCheckPop1()
    {
        check_popup1.SetActive(false);
    }
}
