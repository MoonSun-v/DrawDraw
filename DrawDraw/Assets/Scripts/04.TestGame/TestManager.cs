using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public Image CountDown;
    public Sprite Two;
    public Sprite One;

    public GameObject Curtain;
    private Animator animator;

    void Start()
    {
        animator = Curtain.GetComponent<Animator>();
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isStart", true);
        yield return new WaitForSeconds(3f);
        CountDown.sprite = Two;
        yield return new WaitForSeconds(1f);
        CountDown.sprite = One;
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Test_1Line");
    }
}
