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
    public AudioSource audioSource; // 오디오 소스 컴포넌트

    public AudioClip countAudio;
    public AudioSource countaudioSource; // 오디오 소스 컴포넌트

    // 플레이어 캐릭터 정보 불러오기
    bool isDog;

    // 1. 커튼 닫힌 상태에서 캐릭터가 밑에서 올라옴 
    // 2. 말풍선 생성됨
    // 3. 캐릭터가 밑으로 내려감 
    // 4. 커튼이 열림 
    // 5. 카운트다운 시작

    void Start()
    {
        
        animator_curtain = Curtain.GetComponent<Animator>();
        animator_character = Character.gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // countaudioSource 초기화
        if (countaudioSource == null)
        {
            countaudioSource = gameObject.AddComponent<AudioSource>();
            countaudioSource.clip = countAudio;  // countAudio를 연결
            countaudioSource.playOnAwake = false;  // 자동 재생 방지
        }

        isDog = !GameData.instance.playerdata.PlayerCharacter;  // 강아지면 true, 고양이면 false
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
