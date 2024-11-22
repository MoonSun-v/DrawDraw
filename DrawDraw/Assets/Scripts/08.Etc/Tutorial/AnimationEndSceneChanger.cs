using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AnimationEndSceneChanger : MonoBehaviour
{
    public Animator animator; // Animator ������Ʈ
    public string animationName; // Ȯ���� �ִϸ��̼� �̸� (Inspector���� ����)
    public string nextSceneName;    // �������� �̵��� �� �̸�

    private bool animationFinished = false; // �ִϸ��̼� ���� ���� �÷���

    void Update()
    {
        if (!animationFinished && IsAnimationComplete())
        {
            animationFinished = true;
            LoadNextScene();
        }
    }

    private bool IsAnimationComplete()
    {
        // ���� Animator�� ���� ��������
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ���� �ִϸ��̼��� ������ �̸��� ��ġ�ϰ�, �Ϸ�Ǿ����� Ȯ��
        return stateInfo.normalizedTime >= 1 && stateInfo.IsName(animationName);
    }

    private void LoadNextScene()
    {
        // ���� �� �ε�
        SceneManager.LoadScene(nextSceneName);
    }
}