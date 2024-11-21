using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PrologueManager : MonoBehaviour
{
    public Button nextSceneButton; // ��ư ������Ʈ�� �Ҵ�
    public Animator animator;      // Animator ������Ʈ�� �Ҵ�
    public string animationName;   // ���� ������ �ִϸ��̼� �̸�

    private void Start()
    {
        // ��ư�� ó���� ��Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // �ڷ�ƾ ���� (�ִϸ��̼ǰ� ���� �ð� �� �� �ϳ� �������� ����)
        StartCoroutine(WaitForAnimationEnd());

        // ���� �ð� �� ��ư�� Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        StartCoroutine(ShowButtonAfterDelayCoroutine());

    }

    private IEnumerator WaitForAnimationEnd()
    {
        // �ִϸ������� ���� ������ ������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼��� ���� ������ �ʾҴٸ� ���
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // �ִϸ��̼��� �������Ƿ� �� ��ȯ
        SceneManager.LoadScene("Profile");
    }

    private void OnNextSceneButtonClicked()
    {
        // ���� ���� ���� ������ �̵�
        SceneManager.LoadScene("Profile");
    }

    private IEnumerator ShowButtonAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        // ��ư Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(true);
    }
}