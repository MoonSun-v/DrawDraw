using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialAudio : MonoBehaviour
{
    public AudioSource audioSource;  // 1�� ����

    public AudioClip[] sequentialAudio;

    void Start()
    {
        // ù ��° ���� ����� �����մϴ�.
        StartCoroutine(PlaySequentialSounds(audioSource, sequentialAudio));

    }

    IEnumerator PlaySequentialSounds(AudioSource audioSource, AudioClip[] sequentialAudio)
    {
        if (sequentialAudio == null || sequentialAudio.Length == 0)
        {
            Debug.LogWarning("���� Ŭ�� �迭�� null�̰ų� ��� �ֽ��ϴ�.");
            yield break; // �迭�� ��ȿ���� ������ �ڷ�ƾ ����
        }

        for (int i = 0; i < sequentialAudio.Length; i++)
        {
            if (sequentialAudio[i] != null)
            {
                // ���� ����� Ŭ�� ���� �� ���
                audioSource.clip = sequentialAudio[i];
                audioSource.Play();

                // ����� Ŭ���� ���̸�ŭ ���
                yield return new WaitForSeconds(audioSource.clip.length);
            }
            else
            {
                Debug.LogWarning("�迭�� " + i + "��° ��Ұ� null�Դϴ�. ���� Ŭ������ �Ѿ�ϴ�.");
            }
        }

        Debug.Log("��� ���� Ŭ�� ����� �Ϸ�Ǿ����ϴ�.");
    }
}
