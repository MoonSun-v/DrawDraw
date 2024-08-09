using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Sprite newSprite;   // ������ ��������Ʈ�� ������ ����
    // private SpriteRenderer spriteRenderer;

    private Image Background;


    void Start()
    {
        
        DateTime currentTime = DateTime.Now;     // ���� �ð� ��������

        Debug.Log("���� �ð�: " + currentTime);  // �ð� ���

        // ���� ������Ʈ�� SpriteRenderer ������Ʈ�� ������
        // spriteRenderer = GetComponent<SpriteRenderer>();

        Background = GetComponent<Image>();      // ���� ������Ʈ�� Image ������Ʈ�� ������

        CheckAndChangeSprite(currentTime);       // �ð� Ȯ�� �� ��������Ʈ ����
    }


    void Update()
    {
        //// �� �����Ӹ��� ���� �ð� ������Ʈ �� ���
        // DateTime currentTime = DateTime.Now;
        // Debug.Log("���� �ð�: " + currentTime);
    }


    void CheckAndChangeSprite(DateTime currentTime)
    {
        if (currentTime.Hour >= 18 || currentTime.Hour < 5)     // ���� 6�ú��� ������ ���� 5�� �������� Ȯ��
        {
            Background.sprite = newSprite;                      // ��������Ʈ ����
        }
    }
}
