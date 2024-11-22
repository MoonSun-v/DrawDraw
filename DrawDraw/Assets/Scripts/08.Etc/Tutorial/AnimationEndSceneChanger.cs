using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AnimationEndSceneChanger : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트
    public string animationName; // 확인할 애니메이션 이름 (Inspector에서 설정)
    public string nextSceneName;    // 다음으로 이동할 씬 이름

    private bool animationFinished = false; // 애니메이션 종료 여부 플래그

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
        // 현재 Animator의 상태 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 현재 애니메이션이 설정된 이름과 일치하고, 완료되었는지 확인
        return stateInfo.normalizedTime >= 1 && stateInfo.IsName(animationName);
    }

    private void LoadNextScene()
    {
        // 다음 씬 로드
        SceneManager.LoadScene(nextSceneName);
    }
}