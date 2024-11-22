using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ����
    public Button  TutorialButton; // ��Ȱ��ȭ�� ��ư ������Ʈ

    public GameObject animationObject1; // ù ��° �ִϸ��̼� ������Ʈ
    public GameObject animationObject2; // �� ��° �ִϸ��̼� ������Ʈ

    public string animationName1; // ù ��° �ִϸ��̼� ���� �̸�
    public string animationName2; // �� ��° �ִϸ��̼� ���� �̸�

    private Animator animator;
    private AudioSource TurorialAudioSource;

    public GameObject Input;

    public Image TutorialBG;

    public GameObject object1; // ù ��° ������Ʈ
    public GameObject object2; // �� ��° ������Ʈ

    public Canvas canvas; // Canvas ������Ʈ
    public Camera mainCamera; // Camera ����

    public GameObject HideObject3;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ���� ����
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
                animator.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ
            }
        }

        if (animationObject2 != null)
        {
            animationObject2.SetActive(false);
            animator = animationObject2.GetComponent<Animator>();
            TurorialAudioSource = animationObject2.GetComponent<AudioSource>();
            if (animator != null)
            {
                animator.enabled = false; // �ʱ⿡�� ��Ȱ��ȭ
            }
        }

        // Canvas �⺻ ����
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay; // �⺻�� Overlay ���
        }

    }

    // Update is called once per frame
    void Update()
    {
        // ������� ��� ���� �� ��ư ������Ʈ�� ��Ȱ��ȭ
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
        // Canvas�� RenderMode�� Camera�� ����
        SetCanvasToCamera();

        // ������Ʈ 1�� Ȱ��ȭ�� ���
        if (object1.activeSelf)
        {
            TutorialBG.gameObject.SetActive(true);

            animationObject1.SetActive(true);
            animator = animationObject1.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true; // �ִϸ����� Ȱ��ȭ
                animator.Play(animationName1); // "Animation1"�� Animator ���� �̸�
                PlayAnimationWithAudio(animationObject1, animationName1);
                Debug.Log("ù ��° �ִϸ��̼� ���");
                StartCoroutine(DisableAfterAnimation(animator, animationObject1));
            }
        }

        // ������Ʈ 2�� Ȱ��ȭ�� ���
        else if (object2.activeSelf)
        {
            TutorialBG.gameObject.SetActive(true);

            animationObject2.SetActive(true);
            animator = animationObject2.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = true; // �ִϸ����� Ȱ��ȭ
                animator.Play(animationName2); // "Animation2"�� Animator ���� �̸�
                PlayAnimationWithAudio(animationObject2, animationName2);
                Debug.Log("�� ��° �ִϸ��̼� ���");
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
        // �ִϸ��̼��� ���̸� ������ ���
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        // �ִϸ��̼� ���� �� ó��
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

        // Canvas�� RenderMode�� Overlay�� ����
        SetCanvasToOverlay();

        Debug.Log("�ִϸ��̼� ���� �� ��Ȱ��ȭ");
    }

    private void PlayAnimationWithAudio(GameObject animationObject, string animationName)
    {
        animator = animationObject.GetComponent<Animator>();
        TurorialAudioSource = animationObject.GetComponent<AudioSource>();

        if (animator != null)
        {
            animator.enabled = true; // �ִϸ����� Ȱ��ȭ
            animator.Play(animationName); // �ִϸ��̼� ���
            Debug.Log($"{animationName} �ִϸ��̼� ���");
        }

        if (TurorialAudioSource != null)
        {
            TurorialAudioSource.Play(); // ����� ���
            Debug.Log("����� ���");
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
            Debug.Log("Canvas�� Camera ���� ����");
        }
    }

    private void SetCanvasToOverlay()
    {
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Debug.Log("Canvas�� Overlay ���� ����");
        }
    }
}

