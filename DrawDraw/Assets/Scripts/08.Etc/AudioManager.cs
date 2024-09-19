using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource bgmSource;

    void Awake()
    {
        // �ٸ� AudioManager �ν��Ͻ��� ������ ���� ������Ʈ�� �ı�
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // �ν��Ͻ��� ������ �� ������Ʈ�� �ν��Ͻ��� �����ϰ� �ı����� �ʵ��� ����
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // ������� ��� ������ ������ ����� ����
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    // ����� ���� ���� �Լ�
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
