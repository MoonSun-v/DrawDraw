using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    // public Text countText;

    public Image CountDown;
    public Sprite Two;
    public Sprite One;

    void Start()
    {
        StartCoroutine(CountSecCoroutine());
    }

    private IEnumerator CountSecCoroutine()
    {
        yield return new WaitForSeconds(1f);
        CountDown.sprite = Two;
        yield return new WaitForSeconds(1f);
        CountDown.sprite = One;
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Test_1Line");
    }
}
