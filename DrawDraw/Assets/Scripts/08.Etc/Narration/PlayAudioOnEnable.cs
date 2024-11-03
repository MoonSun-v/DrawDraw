using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnable : MonoBehaviour
{
    public AudioClip audioClip1; // 1�� �����
    public AudioClip audioClip2; // 2�� �����
    public AudioClip audioClip3; // 3�� ����� (���ӿ�����Ʈ3�� ���� ��)
    public AudioClip audioClip4; // 3�� ����� (���ӿ�����Ʈ3�� ���� ��)

    public GameObject gameObject2; // �� ��° ������Ʈ
    public GameObject gameObject3; // �� ��° ������Ʈ (null�� �� ����)
    public GameObject gameObject4; // �� ��° ������Ʈ (null�� �� ����)

    private AudioSource audioSource;

    private bool hasPlayedClip2 = false;
    private bool hasPlayedClip3 = false;
    private bool hasPlayedClip4 = false;

    public SequentialAudio sequentialAudio;
    public bool Blocker_activeSelf_false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // 1�� ������� null�� �ƴ� ��츸 ���
        if (audioClip1 != null)
        {
            PlayAudio(audioClip1);
        }
    }

    void Update()
    {
        // ���ӿ�����Ʈ2�� null�� �ƴϰ� Ȱ��ȭ�Ǹ� 2�� ����� ��� (�� ����)
        if (gameObject2 != null && gameObject2.activeSelf && !hasPlayedClip2)
        {
            PlayAudio(audioClip2);
            hasPlayedClip2 = true;
        }

         // ���ӿ�����Ʈ3�� null�� �ƴϰ� ��Ȱ��ȭ�Ǹ� 3�� ����� ��� (�� ����)
         if (gameObject3 != null && !gameObject3.activeSelf && !hasPlayedClip3 && Blocker_activeSelf_false && hasPlayedClip2)
            {
                PlayAudio(audioClip3);
                hasPlayedClip3 = true;
            }

            // ���ӿ�����Ʈ3�� null�� �ƴϰ� Ȱ��ȭ�Ǹ� 3�� ����� ��� (�� ����)
            if (gameObject3 != null && gameObject3.activeSelf && !hasPlayedClip3 && !Blocker_activeSelf_false && hasPlayedClip2)
            {
                PlayAudio(audioClip3);
                hasPlayedClip3 = true;
            }
        
        // ���ӿ�����Ʈ4�� null�� �ƴϰ� Ȱ��ȭ�Ǹ� 4�� ����� ��� (�� ����)
        if (gameObject4 != null && gameObject4.activeSelf && !hasPlayedClip4)
        {
            PlayAudio(audioClip4);
            hasPlayedClip4 = true;
        }

    }

    void PlayAudio(AudioClip clip)
    {
        // SequentialAudio ��ũ��Ʈ�� ��Ȱ��ȭ
        if (sequentialAudio != null)
        {
            sequentialAudio.enabled = false;
            // SequentialAudio ��ũ��Ʈ�� ��� �ڷ�ƾ�� ����
            sequentialAudio.StopAllCoroutines();
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}