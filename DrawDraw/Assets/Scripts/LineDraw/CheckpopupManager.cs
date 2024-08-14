using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CheckpopupManager : MonoBehaviour
{
    public GameObject checkPopup;
    public resultPopupManager result_popup; // PopupManager ��ũ��Ʈ�� ������ ����
    public DrawLine DrawLine;

    public GameObject line1;
    public GameObject line2;

    public GameObject curveline1;
    public GameObject curveline2;
    public GameObject curveline3;

    public void check()
    {
        transform.gameObject.SetActive(true); // Ȯ�� �˾� â�� ȭ�鿡 ǥ��
    }

    public void OnClick_result() // Ȯ��â �ϼ� ��ư�� Ŭ�� -> ��� �����ֱ�
    {
        checkPopup.SetActive(false);
        if (line1.activeSelf)
        {
            // ���� ��Ȱ��ȭ
            line1.SetActive(false);
            line2.SetActive(false);

            //� Ȱ��ȭ
            curveline1.SetActive(true);
            curveline2.SetActive(true);
            curveline3.SetActive(true);

            //�׷��� �� ��� �����
            ClearAllLines();

        }
        else if (curveline1.activeSelf)
        {
            result_popup.Show(); // ��� �˾� ����
        }

    }

    void ClearAllLines()
    {
        DrawLine.ClearAllLines();
    }

}

