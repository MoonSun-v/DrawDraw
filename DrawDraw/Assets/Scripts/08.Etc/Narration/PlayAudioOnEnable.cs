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
        // ���ӿ�����Ʈ2�� null�� �ƴϰ� Ȱ��ȭ�Ǹ� 2�� ����� ���
        if (gameObject2 != null && gameObject2.activeSelf && audioSource.clip != audioClip2)
        {
            PlayAudio(audioClip2);
        }

        // ���ӿ�����Ʈ3�� null�� �ƴϰ� Ȱ��ȭ�Ǹ� 3�� ����� ���
        if (gameObject3 != null && gameObject3.activeSelf && audioSource.clip != audioClip3)
        {
            PlayAudio(audioClip3);
        }

        // ���ӿ�����Ʈ4�� null�� �ƴϰ� Ȱ��ȭ�Ǹ� 3�� ����� ���
        if (gameObject4 != null && gameObject4.activeSelf && audioSource.clip != audioClip3)
        {
            PlayAudio(audioClip4);
        }
    }

    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}