using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public InputField NameInput;
    private string PlayerName = null;

    private bool isDog;
    private bool isCat;

    void Start()
    {
        // �̸� �Է¶��� ���� ���� 6�ڷ� ����
        NameInput.characterLimit = 6;

        // Ư������, ���� ���͸�
        NameInput.onValueChanged.AddListener(ValidateInput);
    }

    // ������ ����
    public void SelectDog()
    {
        isDog = true; isCat = false;
    }

    // ����� ����
    public void SelectCat()
    {
        isDog = false; isCat = true;
    }

    // �Ϸ� ��ư Ŭ��
    public void Finish()
    {
        // PlayerName ������Ʈ
        PlayerName = NameInput.GetComponent<InputField>().text;


        // ĳ���Ϳ� �̸� �˻�
        if (string.IsNullOrEmpty(PlayerName))
        {
            print(isDog || isCat ? "�̸��� �����ּ���" : "ĳ���͸� �����ϰ� �̸��� �����ּ���");
            return;
        }

        if (!isDog && !isCat)
        {
            print("ĳ���͸� ������ �ּ���");
            return;
        }


        // �÷��̾� ���� ���
        print("�÷��̾� �̸� = " + PlayerName);

        if (isDog) { print("�÷��̾� ĳ���� = ������"); }
        if (isCat) { print("�÷��̾� ĳ���� = �����"); }
        SceneManager.LoadScene("SelectScene");

    }


    // Ư������ ���͸� �޼���
    void ValidateInput(string input)
    {
        // ����, ���ĺ�, �ѱ� ���, Ư�����ڿ� ������ ����
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9��-�R]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }
}
