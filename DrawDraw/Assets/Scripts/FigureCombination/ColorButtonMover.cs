using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonMover : MonoBehaviour
{
    private Vector3 originalPosition;
    private ButtonManager buttonManager;

    void Start()
    {
        // ��ư�� �ʱ� ��ġ�� �����մϴ�.
        originalPosition = transform.localPosition;

        // ButtonManager ������Ʈ�� ã���ϴ�.
        buttonManager = FindObjectOfType<ButtonManager>();
    }

    // ��ư�� Ŭ���Ǿ��� �� ȣ��� �޼���
    public void OnButtonClick()
    {
        // Ŭ���� ��ư�� ��ġ�� �������� �̵���ŵ�ϴ�.
        transform.localPosition = originalPosition + new Vector3(-70, 0, 0);

        // ButtonManager���� �� ��ư�� Ŭ���Ǿ����� �˸��ϴ�.
        buttonManager.OnButtonClicked(this);
    }

    // ��ư�� ��ġ�� ���� ��ġ�� �ǵ����� �޼���
    public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}