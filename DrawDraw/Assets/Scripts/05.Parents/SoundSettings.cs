using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;  // 배경음 슬라이더
    public Slider sfxSlider;  // 효과음 슬라이더
    private AudioManager audioManager;  // AudioManager 가져오기

    void Start()
    {
        // AudioManager 인스턴스 찾기
        audioManager = FindObjectOfType<AudioManager>();

        // AudioManager가 있는지 확인
        if (audioManager != null)
        {
            // 슬라이더 값 변경 시 호출될 리스너 설정
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);

            // 저장된 볼륨 불러오기
            float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            // 슬라이더 초기화
            bgmSlider.value = savedBGMVolume;
            sfxSlider.value = savedSFXVolume;

            // AudioManager에서 배경음 및 효과음 오디오 소스에 저장된 값 적용
            audioManager.SetBGMVolume(savedBGMVolume);
            audioManager.SetSFXVolume(savedSFXVolume);
        }
        else
        {
            Debug.LogError("AudioManager를 찾을 수 없습니다.");
        }
    }


    // 배경음 볼륨 변경 함수
    public void OnBGMVolumeChange(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetBGMVolume(volume);

            // 볼륨 값을 저장
            PlayerPrefs.SetFloat("BGMVolume", volume);
            PlayerPrefs.Save();
        }
    }

    // 효과음 볼륨 변경 함수
    public void OnSFXVolumeChange(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetSFXVolume(volume);

            // 볼륨 값을 저장
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();
        }
    }
}
