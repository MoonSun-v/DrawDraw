using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialAudio : MonoBehaviour
{
    public AudioSource audioSource;  // 1번 사운드

    public AudioClip[] sequentialAudio;

    void Start()
    {
        // 첫 번째 사운드 재생을 시작합니다.
        StartCoroutine(PlaySequentialSounds(audioSource, sequentialAudio));

    }

    IEnumerator PlaySequentialSounds(AudioSource audioSource, AudioClip[] sequentialAudio)
    {
        if (sequentialAudio == null || sequentialAudio.Length == 0)
        {
            Debug.LogWarning("사운드 클립 배열이 null이거나 비어 있습니다.");
            yield break; // 배열이 유효하지 않으면 코루틴 종료
        }

        for (int i = 0; i < sequentialAudio.Length; i++)
        {
            if (sequentialAudio[i] != null)
            {
                // 현재 오디오 클립 설정 및 재생
                audioSource.clip = sequentialAudio[i];
                audioSource.Play();

                // 오디오 클립의 길이만큼 대기
                yield return new WaitForSeconds(audioSource.clip.length);
            }
            else
            {
                Debug.LogWarning("배열의 " + i + "번째 요소가 null입니다. 다음 클립으로 넘어갑니다.");
            }
        }

        Debug.Log("모든 사운드 클립 재생이 완료되었습니다.");
    }
}
