using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    // [ ��� ���� ���� ���� ]
    public GameObject Background;
    private Image BackgroundImg;
    public Sprite newSprite;       // ������ ��������Ʈ�� ������ ����


    // [ ������ �˾� ���� ����] 
    public GameObject Profile;



    // - ���� �ð� ��������
    // - ����� Image ������Ʈ ��������
    // - �ð� Ȯ�� �� ��������Ʈ ���� 
    void Start()
    {
        DateTime currentTime = DateTime.Now;                   
        Debug.Log("���� �ð�: " + currentTime);

        BackgroundImg = Background.GetComponent<Image>();      

        CheckAndChangeSprite(currentTime);                     
    }



    // [ �ð��뺰(����/����) ��� ���� �޼ҵ� ]
    // ���� 6�ú��� ������ ���� 5�� �������� Ȯ���� �� Sprite ����
    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     
        {
            BackgroundImg.sprite = newSprite;                   
        }
    }



    // [ ������ �˾� ���� ��ư �޼ҵ� ]
    public void OnProfilePopup()
    {
        Profile.SetActive(true);
    }
    public void OffProfilePopup()
    {
        Profile.SetActive(false);
    }

}