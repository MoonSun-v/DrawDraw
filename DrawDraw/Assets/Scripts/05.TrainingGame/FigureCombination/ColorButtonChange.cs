using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonChange : MonoBehaviour
{
    // ��ư�� Ŭ���Ǿ��� �� ������ �̹���
    public Sprite clickedImage;

    // ���� ��ư�� �̹���
    private Sprite originalImage;

    // �� ��ư�� ID
    public int buttonID;

    // ��ư�� Image ������Ʈ ����
    private Image buttonImage;

    private ColorButtonManager buttonManager;

    // Ŭ�� �� ������ scale
    public Vector3 clickedScale = new Vector3(1.75f, 1.75f, 1);  // Ŭ�� �� ũ�⸦ 1.2��� Ű��

    // ���� scale ����
    private Vector3 originalScale;


    void Start()
    {
        // ��ư�� Image ������Ʈ�� �����ɴϴ�.
        buttonImage = GetComponent<Image>();

        // ���� �̹��� ����
        originalImage = buttonImage.sprite;

        // ButtonManager ������Ʈ�� ã���ϴ�.
        buttonManager = FindObjectOfType<ColorButtonManager>();

        // ���� scale ����
        originalScale = transform.localScale;
    }

    // ��ư�� Ŭ���Ǿ��� �� ȣ��� �޼���
    public void OnButtonClick()
    {
        // Ŭ���� ��ư�� �̹����� �����մϴ�.
        buttonImage.sprite = clickedImage;

        // ��ư�� scale�� �����մϴ�.
        transform.localScale = clickedScale;

        // ButtonManager���� �� ��ư�� Ŭ���Ǿ����� �˸��ϴ�.
        buttonManager.OnButtonClicked(this);

        // ButtonManager���� Ŭ���� ��ư�� ID�� �����մϴ�.
        buttonManager.SetSelectedButtonID(buttonID);


    }
    // ��ư�� �̹����� ���� �̹����� �ǵ����� �޼���
    public void ResetImage()
    {
        buttonImage.sprite = originalImage;

        // scale�� ���� ũ��� �ǵ����ϴ�.
        transform.localScale = originalScale;
    }


}