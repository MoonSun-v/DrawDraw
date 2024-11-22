using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    // ★  엔딩씬 끝나서 다음 씬으로 넘어갈 때 -> 다음 코드 추가 해주세요 ★ 
    // GameData.instance.trainingdata.ClearStage[19] = true; // : 맵화면 아이콘 완료버전 처리 위해서..!

    public Button nextSceneButton; // 버튼 오브젝트를 할당
    private float delay = 5.0f;     // 지연 시간 (초 단위)

    public GameObject dogObject;  // 강아지 오브젝트
    public GameObject catObject;  // 고양이 오브젝트

    private bool userPreference = false; // 사용자 정보를 기반으로 결정 ("dog" 또는 "cat")

    private void Start()
    {
        // 버튼을 처음에 비활성화
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // 지연 시간 후 버튼을 활성화하는 코루틴 시작
        StartCoroutine(ShowButtonAfterDelayCoroutine());

        // userPreference 값 설정 (기본값: false)
        if (GameData.instance.playerdata.PlayerCharacter)
        {
            userPreference = GameData.instance.playerdata.PlayerCharacter;
        }
        else
        {
            userPreference = false;
        }

        PlaySelectedAnimation();
    }

    private IEnumerator ShowButtonAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);

        // 버튼 활성화
        nextSceneButton.gameObject.SetActive(true);
    }

    private void OnNextSceneButtonClicked()
    {
        // 현재 씬의 다음 씬으로 이동
        GameData.instance.trainingdata.ClearStage[19] = true; // : 맵화면 아이콘 완료버전 처리 위해서..!
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadTrainingData();
        SceneManager.LoadScene("StartScene");
        // SceneManager.LoadScene("MapScene");
    }

    public void PlaySelectedAnimation()
    {
        // 모든 오브젝트의 Animator를 초기화
        ResetAllAnimations();

        if (userPreference == false) // 강아지 애니메이션 실행
        {
            if (dogObject != null)
            {
                Animator dogAnimator = dogObject.GetComponent<Animator>();
                if (dogAnimator != null)
                {
                    dogAnimator.Play("DogEnding Animation"); // 강아지 애니메이션 이름
                }
            }
        }
        else // 고양이 애니메이션 실행
        {
            if (catObject != null)
            {
                Animator catAnimator = catObject.GetComponent<Animator>();
                if (catAnimator != null)
                {
                    catAnimator.Play("CatEnding Animation"); // 고양이 애니메이션 이름
                }
            }
        }
    }

    private void ResetAllAnimations()
    {
        // 강아지 오브젝트 애니메이션 정지
        if (dogObject != null)
        {
            Animator dogAnimator = dogObject.GetComponent<Animator>();
            if (dogAnimator != null)
            {
                dogAnimator.Rebind();
                dogAnimator.Update(0);
            }
        }

        // 고양이 오브젝트 애니메이션 정지
        if (catObject != null)
        {
            Animator catAnimator = catObject.GetComponent<Animator>();
            if (catAnimator != null)
            {
                catAnimator.Rebind();
                catAnimator.Update(0);
            }
        }
    }
}