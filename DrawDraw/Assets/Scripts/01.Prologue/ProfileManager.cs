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


    // - ÀÌ¸§ ÀÔ·Â¶õÀÇ ±ÛÀÚ ¼ö¸¦ 6ÀÚ·Î Á¦ÇÑ
    // - Æ¯¼ö¹®ÀÚ, °ø¹é ÇÊÅÍ¸µ
    void Start()
    {
        NameInput.characterLimit = 6;
        NameInput.onValueChanged.AddListener(ValidateInput);
    }


    // [ °­¾ÆÁö / °í¾çÀÌ ¼±ÅÃ ¸Ş¼Òµå ]
    public void SelectDog(){ isDog = true; isCat = false; }
    public void SelectCat(){ isDog = false; isCat = true; }


    // [ ¿Ï·á ¹öÆ° Å¬¸¯ ]
    // PlayerName ¾÷µ¥ÀÌÆ®
    // Ä³¸¯ÅÍ¿Í ÀÌ¸§ Á¤º¸ ÀÔ·Â Çß´ÂÁö È®ÀÎÇÏ´Â Á¶°Ç¹®
    // ÇÃ·¹ÀÌ¾î Á¤º¸ Ãâ·Â (ÀÓ½Ã, ÃßÈÄ GUI¿Ï¼º ½Ã º¯°æ ÇÊ¿ä)
    public void Finish()
    {

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


        // ÇÃ·¹ÀÌ¾î Á¤º¸ Ãâ·Â (ÀÓ½Ã)
        print("ÇÃ·¹ÀÌ¾î ÀÌ¸§ = " + PlayerName);

        if (isDog) { print("ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ = °­¾ÆÁö"); }
        if (isCat) { print("ÇÃ·¹ÀÌ¾î Ä³¸¯ÅÍ = °í¾çÀÌ"); }

        SceneManager.LoadScene("SelectScene");

    }


    // [ Æ¯¼ö¹®ÀÚ ÇÊÅÍ¸µ ¸Ş¼­µå ]
    // ¼ıÀÚ, ¾ËÆÄºª, ÇÑ±Û Çã¿ë, Æ¯¼ö¹®ÀÚ¿Í °ø¹éÀº Á¦°Å
    void ValidateInput(string input)
    {
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9°¡-ÆR]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }

}
