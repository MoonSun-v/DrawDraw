using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class IdleEventTrigger : MonoBehaviour
{
    public GameObject dogObject; // �������� �� Ȱ��ȭ�� ���� ������Ʈ
    public GameObject catObject; // ������� �� Ȱ��ȭ�� ���� ������Ʈ

    public GameObject triggerObject1; // ù ��° Ʈ���� ������Ʈ
    public GameObject triggerObject2; // �� ��° Ʈ���� ������Ʈ

    private float idleTimeThreshold = 10f; // �Է��� ���� �� ������Ʈ�� Ȱ��ȭ�Ǵ� �ð� (��)
    private float activeDuration = 3f; // ������Ʈ�� Ȱ��ȭ�� �� �ڵ����� ��Ȱ��ȭ�Ǵ� �ð� (��)
    private float idleTimer = 0f;
    private bool isObjectActive = false;
    private int activationCount = 0; // ������Ʈ�� Ȱ��ȭ�� Ƚ��
    private int maxActivations = 3; // �ִ� Ȱ��ȭ Ƚ��

    private bool userPreference = false; // ����� ������ ������� ���� ("dog" �Ǵ� "cat")

    // ����̿� ������ �Ҹ� mp3 ���� ����Ʈ
    public AudioClip[] catSounds;  // ����� �Ҹ� ���
    public AudioClip[] dogSounds;  // ������ �Ҹ� ���
    private AudioClip[] soundClips;
    public AudioSource audioSource; // ����� �ҽ�

    //private int triggerObjectIndex;
    public TextChangeOnTrigger TextChangeOnTrigger;

    public bool Delay = false;

    private void Start()
    {
        //userPreference = GameData.instance.playerdata.PlayerCharacter;

        if(GameData.instance.playerdata.PlayerCharacter)
        {
            userPreference = GameData.instance.playerdata.PlayerCharacter;

        }
        else
        {
            userPreference = false;
        }

        if (Delay == true)
        {
            idleTimeThreshold = 15f;
            Debug.Log(idleTimeThreshold);
        }
    }

    void Update()
    {
        // triggerObject�� �� �� �ϳ��� null�� �ƴϰ� Ȱ��ȭ�Ǿ� ������ ����
        if ((triggerObject1 == null || triggerObject1.activeSelf) || (triggerObject2 == null || triggerObject2.activeSelf))
        {
            // ��ġ �Է� ���� (�����)
            bool touchInputDetected = Input.touchCount > 0;

            // ���콺 �Է� ���� (PC)
            bool mouseInputDetected = Input.GetMouseButton(0); // 0�� ���� Ŭ���� �ǹ�

            // �Է��� ���� ��� Ÿ�̸� �ʱ�ȭ
            if (touchInputDetected || mouseInputDetected)
            {
                idleTimer = 0f;
                if (isObjectActive)
                {
                    DisableObject(); // �Է��� ������ ������Ʈ ��Ȱ��ȭ
                }
            }
            else
            {
                // �Է��� ���� ��� Ÿ�̸� ����
                idleTimer += Time.deltaTime;

                // �Է��� ����, ���� �ð��� ������ ������Ʈ Ȱ��ȭ
                if (idleTimer >= idleTimeThreshold && !isObjectActive && activationCount < maxActivations)
                {
                    ActivateObjectBasedOnUserPreference(); // ����� ��ȣ�� ���� ������Ʈ Ȱ��ȭ
                }
            }

            // ������Ʈ�� Ȱ��ȭ�� ���¶�� Ȱ��ȭ�� �ð��� üũ
            if (isObjectActive && idleTimer >= idleTimeThreshold + activeDuration)
            {
                DisableObject(); // Ȱ��ȭ�� �ð��� ������ ��Ȱ��ȭ
            }
        }
    }

    // ����� ������ ���� �ٸ� ������Ʈ Ȱ��ȭ
    void ActivateObjectBasedOnUserPreference()
    {
        // PlayerCharacter: false->������ , true->����� 

        if (userPreference == false && dogObject != null)
        {
            dogObject.SetActive(true);
            soundClips = dogSounds;
        }
        else if (userPreference == true && catObject != null)
        {
            catObject.SetActive(true);
            soundClips = catSounds;
        }
        isObjectActive = true;

        //narrationAudioSource����
        int index = 0;
        // TextChangeOnTrigger ��ü�� null�� �ƴ��� Ȯ��
        if (TextChangeOnTrigger != null)
        {
            index = TextChangeOnTrigger.index; // index �� �Ҵ�
        }

        // soundClips �迭�� index�� ��ȿ���� Ȯ��
        if (soundClips != null && index >= 0 && index < soundClips.Length)
        {
            PlaySpecificSound(soundClips, index); // ���� ���
        }
        else
        {
            Debug.LogError("Sound clips or index out of bounds");
        }

        activationCount++; // Ȱ��ȭ Ƚ�� ����
    }

    // ���� ������Ʈ ��Ȱ��ȭ�ϴ� �Լ�
    void DisableObject()
    {
        if (dogObject != null && dogObject.activeSelf)
        {
            dogObject.SetActive(false);

        }

        if (catObject != null && catObject.activeSelf)
        {
            catObject.SetActive(false);

        }

        isObjectActive = false; // ������Ʈ�� ��Ȱ��ȭ�� ���¸� ���
        idleTimer = 0f; // Ÿ�̸� �ʱ�ȭ
    }

    // 1�� ��� �� PlaySpecificSound �Լ� ȣ��
    void PlaySpecificSound(AudioClip[] soundClips, int index)
    {

        if (soundClips.Length > 0 && index >= 0 && index < soundClips.Length && audioSource != null)
        {
            audioSource.clip = soundClips[index]; // �Ҹ� ����
            audioSource.Play(); // �Ҹ� ���
        }
        else
        {
            Debug.LogWarning("�߸��� �ε����̰ų� ����� �ҽ��� �������� �ʾҽ��ϴ�.");
        }
    }
}