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
    public GameObject line3;

    public GameObject curveline1;
    public GameObject curveline2;
    public GameObject curveline3;

    public GameObject Shapes1;
    public GameObject Shapes2;
    public GameObject Shapes3;

    public void OnClick_result() // Ȯ��â �ϼ� ��ư�� Ŭ�� -> ��� �����ֱ�
    {
        Debug.Log("�ϼ���ư Ŭ��");
        Debug.Log(line1.activeSelf);
        Debug.Log(curveline1);

        if (line1.activeSelf && curveline1 != null) // ù ��° �׸� �ϼ� -> �� ��° �׸� ����
        {
            // ���� ��Ȱ��ȭ
            line1.SetActive(false);
            line2.SetActive(false);
            line3.SetActive(false);

            if(curveline1 != null)
            {
                //� Ȱ��ȭ
                curveline1.SetActive(true);
                curveline2.SetActive(true);
                curveline3.SetActive(true);
            }
            //�׷��� �� ��� �����
            ClearAllLines();
            Debug.Log("1");

        }
        else if(curveline1 == null) // ù��° �׸��� ���� ���
        {
            Debug.Log("����˾� ����");
            result_popup.Show(); // ��� �˾� ����
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 == null) // �ر׸� 2��
        {
            result_popup.Show(); // ��� �˾� ����
            Debug.Log("2");
        }
        else if (curveline1 != null && curveline1.activeSelf && Shapes1 != null) // �ر׸� 3��
        {
            // ���� ��Ȱ��ȭ
            curveline1.SetActive(false);
            curveline2.SetActive(false);
            curveline3.SetActive(false);

            //� Ȱ��ȭ
            Shapes1.SetActive(true);
            Shapes2.SetActive(true);
            Shapes3.SetActive(true);

            //�׷��� �� ��� �����
            ClearAllLines();
            Debug.Log("3");
        }
        else if (Shapes1 != null && Shapes1.activeSelf)
        {
            result_popup.Show(); // ��� �˾� ����
            Debug.Log("4");
        }
        checkPopup.SetActive(false);
        Debug.Log("5");
    }

    void ClearAllLines()
    {
        DrawLine.ClearAllLines();
    }

}

