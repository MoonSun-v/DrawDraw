using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource bgmSource;

    void Awake()
    {
        // 다른 AudioManager 인스턴스가 있으면 현재 오브젝트를 파괴
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스가 없으면 이 오브젝트를 인스턴스로 설정하고 파괴되지 않도록 설정
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 배경음이 재생 중이지 않으면 배경음 시작
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    // 배경음 볼륨 조절 함수
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
