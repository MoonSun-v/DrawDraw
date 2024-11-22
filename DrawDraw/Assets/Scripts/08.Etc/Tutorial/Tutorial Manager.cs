using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 참조
    public Button  TutorialButton; // 비활성화할 버튼 오브젝트

    public GameObject animationObject1; // 첫 번째 애니메이션 오브젝트
    public GameObject animationObject2; // 두 번째 애니메이션 오브젝트

    public string animationName1; // 첫 번째 애니메이션 상태 이름
    public string animationName2; // 두 번째 애니메이션 상태 이름

    private Animator animator;
    private AudioSource TurorialAudioSource;

    public GameObject Input;

    public Image TutorialBG;

    public GameObject object1; // 첫 번째 오브젝트
    public GameObject object2; // 두 번째 오브젝트

    public Canvas canvas; // Canvas 오브젝트
    public Camera mainCamera; // Camera 참조

    public GameObject HideObject3;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 상태 설정
        if (TutorialBG != null)
        {
            TutorialBG.gameObject.SetActive(false);
        }

        if (animationObject1 != null)
        {
            animationObject1.SetActive(false);
            animator = animationObject1.GetComponent<Animator>();
            TurorialAudioSource = animationObject1.GetComponent<AudioSource>();
            if (animator != null)
            {
                animator.enabled = false; // 초기에는 비활성화
            }
        }

        if (animationObject2 != null)
        {
            animationObject2.SetActive(false);
            animator = animationObject2.GetComponent<Animator>();
            TurorialAudioSource = animationObject2.GetComponent<AudioSource>();
            if (animator != null)
            {
                animator.enabled = false; // 초기에는 비활성화
            }
        }

        // Canvas 기본 설정
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // 기본은 Overlay 모드
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 오디오가 재생 중일 때 버튼 오브젝트를 비활성화
        if (audioSource != null && TutorialButton != null)
        {
            if (audioSource.isPlaying)
            {
                TutorialButton.interactable = false;
            }
            else
            {
                TutorialButton.interactable = true;
            }
        }
    }

    public void PlayAnimation()
    {
        // Canvas의 RenderMode를 Camera로 변경
        SetCanvasToCamera();

        // 오브젝트 1이 활성화된 경우
        if (object1.activeSelf)
        {
            TutorialBG.gameObject.SetActive(true);

            animationObject1.SetActive(true);
            animator = animationObject1.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true; // 애니메이터 활성화
                animator.Play(animationName1); // "Animation1"은 Animator 상태 이름
                PlayAnimationWithAudio(animationObject1, animationName1);
                Debug.Log("첫 번째 애니메이션 재생");
                StartCoroutine(DisableAfterAnimation(animator, animationObject1));
            }
        }

        // 오브젝트 2가 활성화된 경우
        else if (object2.activeSelf)
        {
            TutorialBG.gameObject.SetActive(true);

            animationObject2.SetActive(true);
            animator = animationObject2.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true; // 애니메이터 활성화
                animator.Play(animationName2); // "Animation2"는 Animator 상태 이름
                PlayAnimationWithAudio(animationObject2, animationName2);
                Debug.Log("두 번째 애니메이션 재생");
                StartCoroutine(DisableAfterAnimation(animator, animationObject2));
            }
        }

        if (Input != null)
        {
            Input.SetActive(false);
        }

        if(HideObject3 != null)
        {
            HideObject3.SetActive(false);
        }
    }

    private IEnumerator DisableAfterAnimation(Animator animator, GameObject animationObject)
    {
        // 애니메이션의 길이를 가져와 대기
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        // 애니메이션 종료 후 처리
        animator.enabled = false;
        animationObject.SetActive(false);

        if (TutorialBG != null)
        {
            TutorialBG.gameObject.SetActive(false);
        }

        if (Input != null)
        {
            Input.SetActive(true);
        }

        if (HideObject3 != null)
        {
            HideObject3.SetActive(true);
        }

        // Canvas의 RenderMode를 Overlay로 복원
        SetCanvasToOverlay();

        Debug.Log("애니메이션 종료 및 비활성화");
    }

    private void PlayAnimationWithAudio(GameObject animationObject, string animationName)
    {
        animator = animationObject.GetComponent<Animator>();
        TurorialAudioSource = animationObject.GetComponent<AudioSource>();

        if (animator != null)
        {
            animator.enabled = true; // 애니메이터 활성화
            animator.Play(animationName); // 애니메이션 재생
            Debug.Log($"{animationName} 애니메이션 재생");
        }

        if (TurorialAudioSource != null)
        {
            TurorialAudioSource.Play(); // 오디오 재생
            Debug.Log("오디오 재생");
        }

        StartCoroutine(DisableAfterAnimation(animator, animationObject));
    }

    private void SetCanvasToCamera()
    {
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = mainCamera;
            canvas.planeDistance = 1;
            Debug.Log("Canvas를 Camera 모드로 설정");
        }
    }

    private void SetCanvasToOverlay()
    {
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Debug.Log("Canvas를 Overlay 모드로 복원");
        }
    }
}

