using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class IdleEventTrigger : MonoBehaviour
{
    public GameObject dogObject; // 강아지일 때 활성화할 게임 오브젝트
    public GameObject catObject; // 고양이일 때 활성화할 게임 오브젝트

    public GameObject triggerObject1; // 첫 번째 트리거 오브젝트
    public GameObject triggerObject2; // 두 번째 트리거 오브젝트

    private float idleTimeThreshold = 10f; // 입력이 없을 때 오브젝트가 활성화되는 시간 (초)
    private float activeDuration = 3f; // 오브젝트가 활성화된 후 자동으로 비활성화되는 시간 (초)
    private float idleTimer = 0f;
    private bool isObjectActive = false;
    private int activationCount = 0; // 오브젝트가 활성화된 횟수
    private int maxActivations = 3; // 최대 활성화 횟수

    private bool userPreference = false; // 사용자 정보를 기반으로 결정 ("dog" 또는 "cat")

    // 고양이와 강아지 소리 mp3 파일 리스트
    public AudioClip[] catSounds;  // 고양이 소리 목록
    public AudioClip[] dogSounds;  // 강아지 소리 목록
    private AudioClip[] soundClips;
    public AudioSource audioSource; // 오디오 소스

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
        // triggerObject가 둘 중 하나라도 null이 아니고 활성화되어 있으면 실행
        if ((triggerObject1 == null || triggerObject1.activeSelf) || (triggerObject2 == null || triggerObject2.activeSelf))
        {
            // 터치 입력 감지 (모바일)
            bool touchInputDetected = Input.touchCount > 0;

            // 마우스 입력 감지 (PC)
            bool mouseInputDetected = Input.GetMouseButton(0); // 0은 왼쪽 클릭을 의미

            // 입력이 있을 경우 타이머 초기화
            if (touchInputDetected || mouseInputDetected)
            {
                idleTimer = 0f;
                if (isObjectActive)
                {
                    DisableObject(); // 입력이 있으면 오브젝트 비활성화
                }
            }
            else
            {
                // 입력이 없을 경우 타이머 증가
                idleTimer += Time.deltaTime;

                // 입력이 없고, 일정 시간이 지나면 오브젝트 활성화
                if (idleTimer >= idleTimeThreshold && !isObjectActive && activationCount < maxActivations)
                {
                    ActivateObjectBasedOnUserPreference(); // 사용자 선호에 따라 오브젝트 활성화
                }
            }

            // 오브젝트가 활성화된 상태라면 활성화된 시간을 체크
            if (isObjectActive && idleTimer >= idleTimeThreshold + activeDuration)
            {
                DisableObject(); // 활성화된 시간이 지나면 비활성화
            }
        }
    }

    // 사용자 정보에 따라 다른 오브젝트 활성화
    void ActivateObjectBasedOnUserPreference()
    {
        // PlayerCharacter: false->강아지 , true->고양이 

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

        //narrationAudioSource실행
        int index = 0;
        // TextChangeOnTrigger 객체가 null이 아닌지 확인
        if (TextChangeOnTrigger != null)
        {
            index = TextChangeOnTrigger.index; // index 값 할당
        }

        // soundClips 배열과 index가 유효한지 확인
        if (soundClips != null && index >= 0 && index < soundClips.Length)
        {
            PlaySpecificSound(soundClips, index); // 사운드 재생
        }
        else
        {
            Debug.LogError("Sound clips or index out of bounds");
        }

        activationCount++; // 활성화 횟수 증가
    }

    // 게임 오브젝트 비활성화하는 함수
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

        isObjectActive = false; // 오브젝트가 비활성화된 상태를 기록
        idleTimer = 0f; // 타이머 초기화
    }

    // 1초 대기 후 PlaySpecificSound 함수 호출
    void PlaySpecificSound(AudioClip[] soundClips, int index)
    {

        if (soundClips.Length > 0 && index >= 0 && index < soundClips.Length && audioSource != null)
        {
            audioSource.clip = soundClips[index]; // 소리 설정
            audioSource.Play(); // 소리 재생
        }
        else
        {
            Debug.LogWarning("잘못된 인덱스이거나 오디오 소스가 설정되지 않았습니다.");
        }
    }
}