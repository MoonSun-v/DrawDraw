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

    public GameObject Light_Dog;
    public GameObject Light_Cat;

    public GameObject InputPopup;
    public GameObject InputText;

    public GameObject Curtain;
    private Animator animator;


    // �� [ �̸� �Է� ���� ���� ]
    // - �̸� �Է¶��� ���� �� 6�ڷ� ����
    // - Ư������, ���� ���͸�
    void Start()
    {
        NameInput.characterLimit = 6;
        NameInput.onValueChanged.AddListener(ValidateInput);

        animator = Curtain.GetComponent<Animator>();
    }



    // �� [ ������ / ����� ���� �޼ҵ� ]
    public void SelectDog(){ isDog = true; isCat = false; if (Light_Cat.activeSelf) { Light_Cat.SetActive(false); } Light_Dog.SetActive(true); }
    public void SelectCat(){ isDog = false; isCat = true; if (Light_Dog.activeSelf) { Light_Dog.SetActive(false); } Light_Cat.SetActive(true); }



    // �� [ �Ϸ� ��ư Ŭ�� ]
    //  ĳ���Ϳ� �̸� ���� �Է� Ȯ�� ���ǹ�
    //  �÷��̾� ���� ��� (�ӽ�, ���� GUI�ϼ� �� ���� �ʿ�)
    // -----------
    //  ProfileDataSetting() : �÷��̾� ������ �ۼ� ���� ���� 
    // -----------
    //  ���� ������ �̵� 
    // 
    public void Finish()
    {

        PlayerName = NameInput.GetComponent<InputField>().text;

        // ĳ���Ϳ� �̸� �Է� Ȯ�� ���ǹ�
        if (string.IsNullOrEmpty(PlayerName) && !isDog && !isCat)
        {
            ShowMessage("ĳ���Ϳ� �̸��� �Է�����!");
            return;
        }
        if (string.IsNullOrEmpty(PlayerName))
        {
            ShowMessage("�̸��� �Է�����!");
            return;
        }
        if (!isDog && !isCat)
        {
            ShowMessage("ĳ���͸� ��������!");
            return;
        }

        // �÷��̾� ���� ��� (�ӽ�)
        print("�÷��̾� �̸� = " + PlayerName);
        if (isDog) { print("�÷��̾� ĳ���� = ������"); }
        if (isCat) { print("�÷��̾� ĳ���� = �����"); }

        // --------------------------------------------------------------------------------------

        ProfileSetting(); // �÷��̾� ������ �ۼ� ���� ���� 

        // --------------------------------------------------------------------------------------
        animator.SetBool("isEnd", true);

        StartCoroutine(GoToTest());

    }

    private IEnumerator GoToTest()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("TestStartScene");
    }



    // �� [ Ư������ ���͸� �޼��� ]
    // ����, ���ĺ�, �ѱ� ���, Ư�����ڿ� ������ ����
    void ValidateInput(string input)
    {
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9��-�R]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }



    // �� [ ������ ���� �����ϴ� �޼ҵ� ]
    // 
    // PlayerCharacter : �÷��̾� ĳ���� ( Dog �� 0 or Cat �� 1 ) 
    // PlayerName      : �÷��̾� �̸�
    void ProfileSetting()
    {
        if (isDog) { GameData.instance.playerdata.PlayerCharacter = false; }
        else if (isCat) { GameData.instance.playerdata.PlayerCharacter = true; }

        GameData.instance.playerdata.PlayerName = PlayerName;

        GameData.instance.SavePlayerData();
        GameData.instance.LoadPlayerData();
    }


    void ShowMessage(string message)
    {
        InputPopup.SetActive(true);
        InputText.SetActive(true);

        if (InputText != null)
        {
            InputText.GetComponent<Text>().text = message;
        }

        StartCoroutine(DeactivateObjectAfterDelay(InputPopup, 2f));
    }

    private IEnumerator DeactivateObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        InputText.SetActive(false);
    }

}
