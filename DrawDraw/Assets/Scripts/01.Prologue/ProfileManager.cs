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
        // ÀÌ¸§ ÀÔ·Â¶õÀÇ ±ÛÀÚ ¼ö¸¦ 6ÀÚ·Î Á¦ÇÑ
        NameInput.characterLimit = 6;

        // Æ¯¼ö¹®ÀÚ, °ø¹é ÇÊÅÍ¸µ
        NameInput.onValueChanged.AddListener(ValidateInput);
    }

    // °­¾ÆÁö ¼±ÅÃ
    public void SelectDog()
    {
        isDog = true; isCat = false;
    }

    // °í¾çÀÌ ¼±ÅÃ
    public void SelectCat()
    {
        isDog = false; isCat = true;
    }

    // ¿Ï·á ¹öÆ° Å¬¸¯
    public void Finish()
    {
        // PlayerName ¾÷µ¥ÀÌÆ®
        PlayerName = NameInput.GetComponent<InputField>().text;


        // Ä³¸¯ÅÍ¿Í ÀÌ¸§ °Ë»ç
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


        // ÇÃ·¹ÀÌ¾î Á¤º¸ Ãâ·Â
        print("ÇÃ·¹ÀÌ¾î ÀÌ¸§ = " + PlayerName);

        if (isDog) { print("ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ = °­¾ÆÁö"); }
        if (isCat) { print("ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ = °í¾çÀÌ"); }
        SceneManager.LoadScene("SelectScene");

    }


    // Æ¯¼ö¹®ÀÚ ÇÊÅÍ¸µ ¸Ş¼­µå
    void ValidateInput(string input)
    {
        // ¼ıÀÚ, ¾ËÆÄºª, ÇÑ±Û Çã¿ë, Æ¯¼ö¹®ÀÚ¿Í °ø¹éÀº Á¦°Å
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9°¡-ÆR]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }
}
