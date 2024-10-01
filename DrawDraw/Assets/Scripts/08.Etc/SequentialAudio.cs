using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialAudio : MonoBehaviour
{
    public AudioSource firstAudioSource;  // 1�� ����
    public AudioSource secondAudioSource; // 2�� ����

    private bool hasPlayedSecondSound = false;  // 2�� ���尡 ����Ǿ����� üũ�ϴ� ����

    void Start()
    {
        // ù ��° ���� ����� �����մϴ�.
        StartCoroutine(PlaySequentialSounds());
    }

    IEnumerator PlaySequentialSounds()
    {
        // ù ��° ���带 ����մϴ�.
        firstAudioSource.Play();

        // ù ��° ���尡 ���� ������ ����մϴ�.
        yield return new WaitForSeconds(firstAudioSource.clip.length);

        // �� ��° ���尡 ���� ������� �ʾ��� ���� ����
        if (!hasPlayedSecondSound)
        {
            // �� ��° ���带 ����մϴ�.
            secondAudioSource.Play();
            hasPlayedSecondSound = true;  // 2�� ���尡 ����Ǿ����� ǥ��
        }
    }
}
