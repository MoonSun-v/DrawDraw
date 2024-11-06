using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileAudio : MonoBehaviour
{
    public AudioSource audioSource1;  // ��ư 1�� ����� �ҽ�
    public AudioSource audioSource2;  // ��ư 2�� ����� �ҽ�

    public AudioSource GuideAudioSource;  

    // ��ư 1�� Ŭ���Ǿ��� �� ����� �Լ�
    public void PlayAudio1()
    {
        // ��ư 2�� ������� ��� ���̶�� ����
        if (audioSource2.isPlaying)
        {
            audioSource2.Stop();
        }
        if (GuideAudioSource.isPlaying)
        {
            GuideAudioSource.Stop();
        }

        // ��ư 1�� ������� ���
        audioSource1.Play();
    }

    // ��ư 2�� Ŭ���Ǿ��� �� ����� �Լ�
    public void PlayAudio2()
    {
        // ��ư 1�� ������� ��� ���̶�� ����
        if (audioSource1.isPlaying)
        {
            audioSource1.Stop();
        }
        if (GuideAudioSource.isPlaying)
        {
            GuideAudioSource.Stop();
        }

        // ��ư 2�� ������� ���
        audioSource2.Play();
    }
}