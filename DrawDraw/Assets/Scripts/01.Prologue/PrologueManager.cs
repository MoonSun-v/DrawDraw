using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PrologueManager : MonoBehaviour
{
    public Button nextSceneButton; // 버튼 오브젝트를 할당
    public float delay = 3.0f;     // 지연 시간 (초 단위)

    public VideoPlayer videoPlayer;   // VideoPlayer 컴포넌트를 할당

    private void Start()
    {
        // 버튼을 처음에 비활성화
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // 지연 시간 후 버튼을 활성화하는 코루틴 시작
        StartCoroutine(ShowButtonAfterDelayCoroutine());

        // 비디오 재생 완료 시 호출될 이벤트 등록
        videoPlayer.loopPointReached += OnVideoEnd;
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
        SceneManager.LoadScene("Profile");
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 지정된 씬으로 이동
        SceneManager.LoadScene("Profile");
    }
}
