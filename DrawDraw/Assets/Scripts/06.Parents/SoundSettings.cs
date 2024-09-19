using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;  // ����� �����̴�
    public Slider sfxSlider;  // ȿ���� �����̴�
    private AudioManager audioManager;  // AudioManager ��������

    void Start()
    {
        // AudioManager �ν��Ͻ� ã��
        audioManager = FindObjectOfType<AudioManager>();

        // AudioManager�� �ִ��� Ȯ��
        if (audioManager != null)
        {
            // �����̴� �� ���� �� ȣ��� ������ ����
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChange);
            sfxSlider.onValueChanged.AddListener(OnSFXVolumeChange);

            // ����� ���� �ҷ�����
            float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            // �����̴� �ʱ�ȭ
            bgmSlider.value = savedBGMVolume;
            sfxSlider.value = savedSFXVolume;

            // AudioManager���� ����� �� ȿ���� ����� �ҽ��� ����� �� ����
            audioManager.SetBGMVolume(savedBGMVolume);
            audioManager.SetSFXVolume(savedSFXVolume);
        }
        else
        {
            Debug.LogError("AudioManager�� ã�� �� �����ϴ�.");
        }
    }


    // ����� ���� ���� �Լ�
    public void OnBGMVolumeChange(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetBGMVolume(volume);

            // ���� ���� ����
            PlayerPrefs.SetFloat("BGMVolume", volume);
            PlayerPrefs.Save();
        }
    }

    // ȿ���� ���� ���� �Լ�
    public void OnSFXVolumeChange(float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetSFXVolume(volume);

            // ���� ���� ����
            PlayerPrefs.SetFloat("SFXVolume", volume);
            PlayerPrefs.Save();
        }
    }
}
