using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;  // ����� �����̴�
    public Slider sfxSlider;  // ȿ���� �����̴�
    public AudioSource bgmSource;  // ����� ����� �ҽ�
    public AudioSource[] sfxSources;  // ���� ���� ȿ���� ����� �ҽ�

    void Start()
    {
        // �����̴� �� ���� �� ȣ��� ������ ����
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);

        // ����� ���� �ҷ�����
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

    // ����� ���� ���� �Լ�
    public void OnBGMVolumeChange(float volume)
    {
        bgmSource.volume = volume;

        // ���� ���� ����
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    // ȿ���� ���� ���� �Լ�
    public void OnSFXVolumeChange(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }

        // ���� ���� ����
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}

