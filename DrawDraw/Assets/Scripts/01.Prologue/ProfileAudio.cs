using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileAudio : MonoBehaviour
{
    public AudioSource audioSource1;  // 버튼 1의 오디오 소스
    public AudioSource audioSource2;  // 버튼 2의 오디오 소스

    public AudioSource GuideAudioSource;  

    // 버튼 1이 클릭되었을 때 실행될 함수
    public void PlayAudio1()
    {
        // 버튼 2의 오디오가 재생 중이라면 중지
        if (audioSource2.isPlaying)
        {
            audioSource2.Stop();
        }
        if (GuideAudioSource.isPlaying)
        {
            GuideAudioSource.Stop();
        }

        // 버튼 1의 오디오를 재생
        audioSource1.Play();
    }

    // 버튼 2가 클릭되었을 때 실행될 함수
    public void PlayAudio2()
    {
        // 버튼 1의 오디오가 재생 중이라면 중지
        if (audioSource1.isPlaying)
        {
            audioSource1.Stop();
        }
        if (GuideAudioSource.isPlaying)
        {
            GuideAudioSource.Stop();
        }

        // 버튼 2의 오디오를 재생
        audioSource2.Play();
    }
}