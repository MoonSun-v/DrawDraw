using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;  // 배경음 슬라이더
    public Slider sfxSlider;  // 효과음 슬라이더
    public AudioSource bgmSource;  // 배경음 오디오 소스
    public AudioSource[] sfxSources;  // 여러 개의 효과음 오디오 소스

    void Start()
    {
        // 슬라이더 값 변경 시 호출될 리스너 설정
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);

        // 저장된 볼륨 불러오기
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgmSlider.value = savedBGMVolume;
        sfxSlider.value = savedSFXVolume;

        bgmSource.volume = savedBGMVolume;
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = savedSFXVolume;
        }
    }

    // 배경음 볼륨 변경 함수
    public void OnBGMVolumeChange(float volume)
    {
        bgmSource.volume = volume;

        // 볼륨 값을 저장
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    // 효과음 볼륨 변경 함수
    public void OnSFXVolumeChange(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }

        // 볼륨 값을 저장
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}

