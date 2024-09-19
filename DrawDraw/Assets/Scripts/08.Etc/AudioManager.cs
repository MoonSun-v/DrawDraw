using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource bgmSource;  // 배경음 오디오 소스
    public AudioSource[] sfxSources;  // 여러 개의 효과음 오디오 소스

    void Awake()
    {
        // AudioManager의 중복 생성을 방지
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 파괴되지 않도록 설정
    }

    // 배경음 볼륨 조절 함수
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // 효과음 볼륨 조절 함수
    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }
}
