using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // ��� ���� ���� ����
    public GameObject Background;
    private Image BackgroundImg;
    public Sprite newSprite;       // ������ ��������Ʈ�� ������ ����


    // ������ �˾� ���� ����
    public GameObject Profile;


    void Start()
    {
        DateTime currentTime = DateTime.Now;                   // ���� �ð� ��������
        Debug.Log("���� �ð�: " + currentTime);

        BackgroundImg = Background.GetComponent<Image>();      // ����� Image ������Ʈ ������

        CheckAndChangeSprite(currentTime);                     // �ð� Ȯ�� �� ��������Ʈ ����
    }


    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     // ���� 6�ú��� ������ ���� 5�� �������� Ȯ��
        {
            BackgroundImg.sprite = newSprite;                   // ��������Ʈ ����
        }
    }


    
    // ������ �˾� 
    public void OnProfilePopup()
    {
        Profile.SetActive(true);
    }
    public void OffProfilePopup()
    {
        Profile.SetActive(false);
    }

}
