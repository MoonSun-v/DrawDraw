using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestBackToSelect : MonoBehaviour
{
    public GameObject CheckPopup;

    // ¿Ï·á È®ÀÎ ÆË¾÷
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }

    // ÆË¾÷ : ¾Æ´Ï
    public void NoBtn()
    {
        CheckPopup.SetActive(false);
    }

    // ÆË¾÷ : ÀÀ
    public void YesBtn()
    {
        CheckPopup.SetActive(false);
        SceneManager.LoadScene("SelectScene");
    }
}
