using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PrologueManager : MonoBehaviour
{
    public Button nextSceneButton; // 버튼 오브젝트를 할당
    public Animator animator;      // Animator 컴포넌트를 할당
    public string animationName;   // 끝을 감지할 애니메이션 이름

    private void Start()
    {
        // 버튼을 처음에 비활성화
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // 코루틴 시작 (애니메이션과 지연 시간 둘 중 하나 기준으로 실행)
        StartCoroutine(WaitForAnimationEnd());

        // 지연 시간 후 버튼을 활성화하는 코루틴 시작
        StartCoroutine(ShowButtonAfterDelayCoroutine());

    }

    private IEnumerator WaitForAnimationEnd()
    {
        // 애니메이터의 상태 정보를 가져옴
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션이 아직 끝나지 않았다면 대기
        while (stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // 애니메이션이 끝났으므로 씬 전환
        SceneManager.LoadScene("Profile");
    }

    private void OnNextSceneButtonClicked()
    {
        // 현재 씬의 다음 씬으로 이동
        SceneManager.LoadScene("Profile");
    }

    private IEnumerator ShowButtonAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        // 버튼 활성화
        nextSceneButton.gameObject.SetActive(true);
    }
}