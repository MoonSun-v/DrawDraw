using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IdleEventTrigger : MonoBehaviour
{
    public GameObject dogObject; // 강아지일 때 활성화할 게임 오브젝트
    public GameObject catObject; // 고양이일 때 활성화할 게임 오브젝트

    public GameObject triggerObject1; // 첫 번째 트리거 오브젝트
    public GameObject triggerObject2; // 두 번째 트리거 오브젝트

    private float idleTimeThreshold = 10f; // 입력이 없을 때 오브젝트가 활성화되는 시간 (초)
    private float activeDuration = 5f; // 오브젝트가 활성화된 후 자동으로 비활성화되는 시간 (초)
    private float idleTimer = 0f;
    private bool isObjectActive = false;
    private int activationCount = 0; // 오브젝트가 활성화된 횟수
    private int maxActivations = 3; // 최대 활성화 횟수

    public string userPreference = "dog"; // 사용자 정보를 기반으로 결정 ("dog" 또는 "cat")

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
                    ActivateObjectBasedOnUserPreference();
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
        if (userPreference == "dog" && dogObject != null)
        {
            dogObject.SetActive(true);
            Debug.Log("강아지 오브젝트가 활성화되었습니다.");
        }
        else if (userPreference == "cat" && catObject != null)
        {
            catObject.SetActive(true);
            Debug.Log("고양이 오브젝트가 활성화되었습니다.");
        }
        isObjectActive = true;
        activationCount++; // 활성화 횟수 증가
    }

    // 게임 오브젝트 비활성화하는 함수
    void DisableObject()
    {
        if (dogObject != null && dogObject.activeSelf)
        {
            dogObject.SetActive(false);
            Debug.Log("강아지 오브젝트가 비활성화되었습니다.");
        }

        if (catObject != null && catObject.activeSelf)
        {
            catObject.SetActive(false);
            Debug.Log("고양이 오브젝트가 비활성화되었습니다.");
        }

        isObjectActive = false; // 오브젝트가 비활성화된 상태를 기록
        idleTimer = 0f; // 타이머 초기화
    }
}