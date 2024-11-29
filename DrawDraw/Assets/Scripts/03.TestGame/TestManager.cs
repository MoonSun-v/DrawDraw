using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public Image Character;
    public Sprite DogCharacter;
    public Image TextImage;
    public Sprite DogText;

    public Image CountDown;
    public Sprite Two;
    public Sprite One;

    public GameObject Curtain;
    private Animator animator_curtain;
    private Animator animator_character;

    public AudioClip DogAudio;
    public AudioSource audioSource; // ����� �ҽ� ������Ʈ

    public AudioClip countAudio;
    public AudioSource countaudioSource; // ����� �ҽ� ������Ʈ

    // �÷��̾� ĳ���� ���� �ҷ�����
    bool isDog;

    // 1. Ŀư ���� ���¿��� ĳ���Ͱ� �ؿ��� �ö�� 
    // 2. ��ǳ�� ������
    // 3. ĳ���Ͱ� ������ ������ 
    // 4. Ŀư�� ���� 
    // 5. ī��Ʈ�ٿ� ����

    void Start()
    {
        
        animator_curtain = Curtain.GetComponent<Animator>();
        animator_character = Character.gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // countaudioSource �ʱ�ȭ
        if (countaudioSource == null)
        {
            countaudioSource = gameObject.AddComponent<AudioSource>();
            countaudioSource.clip = countAudio;  // countAudio�� ����
            countaudioSource.playOnAwake = false;  // �ڵ� ��� ����
        }

        isDog = !GameData.instance.playerdata.PlayerCharacter;  // �������� true, ����̸� false
        if (isDog) { Character.sprite = DogCharacter; TextImage.sprite = DogText; audioSource.clip = DogAudio; }
        
        StartCoroutine(CharacterAppear());
        
    }

    private IEnumerator CharacterAppear()
    {
        yield return new WaitForSeconds(0.5f);
        animator_character.SetBool("isUp", true);
        yield return new WaitForSeconds(2f);
        TextImage.gameObject.SetActive(true);
        audioSource.Play();
        yield return new WaitForSeconds(2.5f);
        TextImage.gameObject.SetActive(false);
        animator_character.SetBool("isDown", true);

        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(1.5f);
        animator_curtain.SetBool("isStart", true);
        yield return new WaitForSeconds(2f);
        countaudioSource.Play();
        yield return new WaitForSeconds(1f);
        CountDown.sprite = Two;
        yield return new WaitForSeconds(1f);
        CountDown.sprite = One;
        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene("Test_1Line");
    }
}
