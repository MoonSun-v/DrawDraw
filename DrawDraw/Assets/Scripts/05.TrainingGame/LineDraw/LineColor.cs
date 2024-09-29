using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
    public GameObject LineArea;
    public GameObject ColorArea;

    public GameObject baseSquare;
    public GameObject baseSquare1;
    public GameObject baseSquare2;

    public GameObject checkPopup;

    public resultPopupManager result_popup;

    private bool color = false;

    public void OnClick_color() // Ȯ��â �ϼ� ��ư�� Ŭ�� -> ��� �����ֱ�
    {
        if (color)
        {
            result_popup.Show(); // ��� �˾� ����
        }
        else
        {
            LineArea.SetActive(false);
            ColorArea.SetActive(true);

            baseSquare.transform.position += Vector3.left * 1.5f;
            baseSquare1.SetActive(false);
            baseSquare2.SetActive(false);

            MoveClones();

            color = true;
        }

        checkPopup.SetActive(false);
    }

    // ������ ��� Ŭ���� �̵���Ű�� �Լ�
    void MoveClones()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("LineBrush"); // �±׷� ������ ã��

        foreach (GameObject clone in clones)
        {
            clone.transform.position += Vector3.left * 1.5f;
        }
    }  


}
