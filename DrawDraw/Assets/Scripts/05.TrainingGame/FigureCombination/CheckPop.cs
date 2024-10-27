using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPop : MonoBehaviour
{
    public GameObject check_popup;
    public GameObject check_popup1;

    public CanvasGroup colorButtonGroup;  // ��ĥ ��ư �׷�
    private void Start()
    {
            // ó�� ������ �� ���� ��ư�� ���̰�, ��ĥ ��ư�� ������ �ʵ��� ����
            //SetCanvasGroupActive(shapeButtonGroup, true);
            SetCanvasGroupActive(colorButtonGroup, false);
            Debug.Log("����Ʈ ���ֱ�");
    }

    void SetCanvasGroupActive(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }


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
