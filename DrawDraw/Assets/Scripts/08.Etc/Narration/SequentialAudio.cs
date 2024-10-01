using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialAudio : MonoBehaviour
{
    public AudioSource firstAudioSource;  // 1번 사운드
    public AudioSource secondAudioSource; // 2번 사운드
    public AudioSource thirdAudioSource;  // 3번 사운드

    private bool hasPlayedSecondSound = false;  // 2번 사운드가 재생되었는지 체크하는 변수
    private bool hasPlayedThirdSound = false;   // 3번 사운드가 재생되었는지 체크하는 변수


    void Start()
    {
        // 첫 번째 사운드 재생을 시작합니다.
        StartCoroutine(PlaySequentialSounds());

    }

    IEnumerator PlaySequentialSounds()
    {
        // 첫 번째 사운드를 재생합니다.
        firstAudioSource.Play();

        // 첫 번째 사운드가 끝날 때까지 대기합니다.
        yield return new WaitForSeconds(firstAudioSource.clip.length);

        // 두 번째 사운드가 아직 재생되지 않았을 때만 실행
        if (!hasPlayedSecondSound)
        {
            // 두 번째 사운드를 재생합니다.
            secondAudioSource.Play();
            hasPlayedSecondSound = true;  // 2번 사운드가 재생되었음을 표시
                                          // 두 번째 사운드가 끝날 때까지 대기합니다.
            yield return new WaitForSeconds(secondAudioSource.clip.length);
        }

        // 세 번째 사운드가 null이 아니고 아직 재생되지 않았을 때만 실행
        if (thirdAudioSource != null && !hasPlayedThirdSound)
        {
            // 세 번째 사운드를 재생합니다.
            thirdAudioSource.Play();
            hasPlayedThirdSound = true;  // 3번 사운드가 재생되었음을 표시
        }
    }
}
