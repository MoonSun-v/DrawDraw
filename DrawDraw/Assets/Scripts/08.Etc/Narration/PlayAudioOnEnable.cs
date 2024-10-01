using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnable : MonoBehaviour
{
    public AudioClip audioClip1; // 1번 오디오
    public AudioClip audioClip2; // 2번 오디오
    public AudioClip audioClip3; // 3번 오디오 (게임오브젝트3이 있을 때)
    public AudioClip audioClip4; // 3번 오디오 (게임오브젝트3이 있을 때)

    public GameObject gameObject2; // 두 번째 오브젝트
    public GameObject gameObject3; // 세 번째 오브젝트 (null일 수 있음)
    public GameObject gameObject4; // 세 번째 오브젝트 (null일 수 있음)

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 1번 오디오가 null이 아닌 경우만 재생
        if (audioClip1 != null)
        {
            PlayAudio(audioClip1);
        }
    }

    void Update()
    {
        // 게임오브젝트2가 null이 아니고 활성화되면 2번 오디오 재생
        if (gameObject2 != null && gameObject2.activeSelf && audioSource.clip != audioClip2)
        {
            PlayAudio(audioClip2);
        }

        // 게임오브젝트3이 null이 아니고 활성화되면 3번 오디오 재생
        if (gameObject3 != null && gameObject3.activeSelf && audioSource.clip != audioClip3)
        {
            PlayAudio(audioClip3);
        }

        // 게임오브젝트4이 null이 아니고 활성화되면 3번 오디오 재생
        if (gameObject4 != null && gameObject4.activeSelf && audioSource.clip != audioClip3)
        {
            PlayAudio(audioClip4);
        }
    }

    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}