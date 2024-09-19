using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource bgmSource;  // ����� ����� �ҽ�
    public AudioSource[] sfxSources;  // ���� ���� ȿ���� ����� �ҽ�

    void Awake()
    {
        // AudioManager�� �ߺ� ������ ����
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // �ı����� �ʵ��� ����
    }

    // ����� ���� ���� �Լ�
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // ȿ���� ���� ���� �Լ�
    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }
}
