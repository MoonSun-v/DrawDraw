using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    // ��  ������ ������ ���� ������ �Ѿ �� -> ���� �ڵ� �߰� ���ּ��� �� 
    // GameData.instance.trainingdata.ClearStage[19] = true; // : ��ȭ�� ������ �Ϸ���� ó�� ���ؼ�..!

    public Button nextSceneButton; // ��ư ������Ʈ�� �Ҵ�
    private float delay = 5.0f;     // ���� �ð� (�� ����)

    public GameObject dogObject;  // ������ ������Ʈ
    public GameObject catObject;  // ����� ������Ʈ

    private bool userPreference = false; // ����� ������ ������� ���� ("dog" �Ǵ� "cat")

    private Animator currentAnimator;

    private void Start()
    {
        // ��ư�� ó���� ��Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // ���� �ð� �� ��ư�� Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        StartCoroutine(ShowButtonAfterDelayCoroutine());

        // userPreference �� ���� (�⺻��: false)
        if (GameData.instance.playerdata.PlayerCharacter)
        {
            userPreference = GameData.instance.playerdata.PlayerCharacter;
        }
        else
        {
            userPreference = false;
        }

        // �ִϸ��̼� ���� �� Animator ���� ����
        PlaySelectedAnimation(userPreference);
        StartCoroutine(CheckAnimationEndCoroutine());
    }

    void LoadNextScene()
    {
        // ���� ������ �̵�
        SceneManager.LoadScene("StartScene");
    }
    private IEnumerator ShowButtonAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);

        // ��ư Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(true);
    }

    private void OnNextSceneButtonClicked()
    {
        // ���� ���� ���� ������ �̵�
        GameData.instance.trainingdata.ClearStage[19] = true; // : ��ȭ�� ������ �Ϸ���� ó�� ���ؼ�..!
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadTrainingData();
        SceneManager.LoadScene("StartScene");
        // SceneManager.LoadScene("MapScene");
    }

    public void PlaySelectedAnimation(bool userPreference)
    {
        // ��� ������Ʈ�� Animator�� �ʱ�ȭ
        ResetAllAnimations();

        // PlayerCharacter: false->������ , true->����� 
        if (userPreference == false) // ������ �ִϸ��̼� ����
        {
            if (dogObject != null)
            {
                currentAnimator = dogObject.GetComponent<Animator>();
                if (currentAnimator != null)
                {
                    currentAnimator.Play("DogEnding Animation"); // ������ �ִϸ��̼� �̸�
                }
            }
        }
        else // ����� �ִϸ��̼� ����
        {
            if (catObject != null)
            {
                currentAnimator = catObject.GetComponent<Animator>();
                if (currentAnimator != null)
                {
                    currentAnimator.Play("CatEnding Animation"); // ����� �ִϸ��̼� �̸�
                }
            }
        }
    }

    private void ResetAllAnimations()
    {
        // ������ ������Ʈ �ִϸ��̼� ����
        if (dogObject != null)
        {
            Animator dogAnimator = dogObject.GetComponent<Animator>();
            if (dogAnimator != null)
            {
                dogAnimator.Rebind();
                dogAnimator.Update(0);
            }
        }

        // ����� ������Ʈ �ִϸ��̼� ����
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

    private IEnumerator CheckAnimationEndCoroutine()
    {
        // �ִϸ��̼� ���� ����
        if (currentAnimator != null)
        {
            AnimatorStateInfo stateInfo = currentAnimator.GetCurrentAnimatorStateInfo(0);
            while (stateInfo.normalizedTime < 1.0f || stateInfo.loop)
            {
                stateInfo = currentAnimator.GetCurrentAnimatorStateInfo(0);
                yield return null; // ���� �����ӱ��� ���
            }

            // �ִϸ��̼��� ����Ǹ� ���� ������ �̵�
            LoadNextScene();
        }
    }
}