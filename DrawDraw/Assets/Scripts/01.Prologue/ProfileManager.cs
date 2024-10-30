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


    // ¡Ú [ ÀÌ¸§ ÀÔ·Â Á¦ÇÑ Á¶°Ç ]
    // - ÀÌ¸§ ÀÔ·Â¶õÀÇ ±ÛÀÚ ¼ö 6ÀÚ·Î Á¦ÇÑ
    // - Æ¯¼ö¹®ÀÚ, °ø¹é ÇÊÅÍ¸µ
    void Start()
    {
        NameInput.characterLimit = 6;
        NameInput.onValueChanged.AddListener(ValidateInput);
    }



    // ¡Ú [ °­¾ÆÁö / °í¾çÀÌ ¼±ÅÃ ¸Ş¼Òµå ]
    public void SelectDog(){ isDog = true; isCat = false; if (Light_Cat.activeSelf) { Light_Cat.SetActive(false); } Light_Dog.SetActive(true); }
    public void SelectCat(){ isDog = false; isCat = true; if (Light_Dog.activeSelf) { Light_Dog.SetActive(false); } Light_Cat.SetActive(true); }



    // ¡Ú [ ¿Ï·á ¹öÆ° Å¬¸¯ ]
    //  Ä³¸¯ÅÍ¿Í ÀÌ¸§ Á¤º¸ ÀÔ·Â È®ÀÎ Á¶°Ç¹®
    //  ÇÃ·¹ÀÌ¾î Á¤º¸ Ãâ·Â (ÀÓ½Ã, ÃßÈÄ GUI¿Ï¼º ½Ã º¯°æ ÇÊ¿ä)
    // -----------
    //  ProfileDataSetting() : ÇÃ·¹ÀÌ¾î ÇÁ·ÎÇÊ ÀÛ¼º Á¤º¸ ÀúÀå 
    // -----------
    //  ´ÙÀ½ ¾ÀÀ¸·Î ÀÌµ¿ 
    // 
    public void Finish()
    {

        PlayerName = NameInput.GetComponent<InputField>().text;

        // Ä³¸¯ÅÍ¿Í ÀÌ¸§ È®ÀÎ Á¶°Ç¹® 
        if (string.IsNullOrEmpty(PlayerName))
        {
            print(isDog || isCat ? "ÀÌ¸§À» Àû¾îÁÖ¼¼¿ä" : "Ä³¸¯ÅÍ¸¦ ¼±ÅÃÇÏ°í ÀÌ¸§À» Àû¾îÁÖ¼¼¿ä");
            return;
        }
        if (!isDog && !isCat)
        {
            print("Ä³¸¯ÅÍ¸¦ ¼±ÅÃÇØ ÁÖ¼¼¿ä");
            return;
        }

        // ÇÃ·¹ÀÌ¾î Á¤º¸ Ãâ·Â (ÀÓ½Ã)
        print("ÇÃ·¹ÀÌ¾î ÀÌ¸§ = " + PlayerName);
        if (isDog) { print("ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ = °­¾ÆÁö"); }
        if (isCat) { print("ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ = °í¾çÀÌ"); }

        // --------------------------------------------------------------------------------------

        ProfileSetting(); // ÇÃ·¹ÀÌ¾î ÇÁ·ÎÇÊ ÀÛ¼º Á¤º¸ ÀúÀå 

        // --------------------------------------------------------------------------------------

        SceneManager.LoadScene("TestStartScene");

    }


    // ¡Ú [ Æ¯¼ö¹®ÀÚ ÇÊÅÍ¸µ ¸Ş¼­µå ]
    // ¼ıÀÚ, ¾ËÆÄºª, ÇÑ±Û Çã¿ë, Æ¯¼ö¹®ÀÚ¿Í °ø¹éÀº Á¦°Å
    void ValidateInput(string input)
    {
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9°¡-ÆR]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }



    // ¡Ú [ ÇÁ·ÎÇÊ Á¤º¸ ÀúÀåÇÏ´Â ¸Ş¼Òµå ]
    // 
    // PlayerCharacter : ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ ( Dog ´Â 0 or Cat Àº 1 ) 
    // PlayerName      : ÇÃ·¹ÀÌ¾î ÀÌ¸§
    void ProfileSetting()
    {
        if (isDog) { GameData.instance.playerdata.PlayerCharacter = false; }
        else if (isCat) { GameData.instance.playerdata.PlayerCharacter = true; }

        GameData.instance.playerdata.PlayerName = PlayerName;

        GameData.instance.SavePlayerData();
        GameData.instance.LoadPlayerData();
    }
}
