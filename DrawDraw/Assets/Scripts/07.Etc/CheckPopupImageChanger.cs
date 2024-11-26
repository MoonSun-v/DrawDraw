using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPopupImageChanger : MonoBehaviour
{
    // Image ������Ʈ�� ����
    public Image imageComponent;

    private bool userPreference = false; // ����� ������ ������� ���� ("dog" �Ǵ� "cat")


    // ����̿� ������ �̹��� ��������Ʈ
    public Sprite catImage;
    public Sprite dogImage;

    private void Start()
    {
        userPreference = GameData.instance.playerdata.PlayerCharacter;
        Debug.Log(userPreference);
        ChangeImage(userPreference);
    }

    // ���ǿ� ���� �̹����� �����ϴ� �Լ�
    public void ChangeImage(bool isCat)
    {
        // PlayerCharacter: false->������ , true->����� 
        if (userPreference == false && dogImage != null)
        {

            // false�̸� ������ �̹����� ����
            imageComponent.sprite = dogImage;
        }
        else if (userPreference == true && catImage != null)
        {
            // true�̸� ����� �̹����� ����
            imageComponent.sprite = catImage;
        }
    }
}