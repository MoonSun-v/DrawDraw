using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndSceneChanger : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트
    public string nextSceneName;    // 다음으로 이동할 씬 이름

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (videoPlayer != null)
        {
            // VideoPlayer의 loopPointReached 이벤트에 메서드 등록
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer가 할당되지 않았습니다!");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 영상 재생이 끝났을 때 호출됩니다.
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnDestroy()
    {
        // 이벤트 등록 해제
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}