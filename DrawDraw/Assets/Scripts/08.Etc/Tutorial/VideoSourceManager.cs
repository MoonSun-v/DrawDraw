using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSourceManager : MonoBehaviour
{
    public GameObject object1; // 첫 번째 오브젝트
    public GameObject object2; // 두 번째 오브젝트

    public VideoClip videoClip1; // 첫 번째 영상
    public VideoClip videoClip2; // 두 번째 영상

    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트

    void Update()
    {
        if (videoPlayer != null)
        {
            AssignAndPlayVideo();
        }
    }

    private void AssignAndPlayVideo()
    {
        if (videoPlayer == null) return;

        // 오브젝트1이 활성화되었을 때 1번 영상을 할당 및 재생
        if (object1 != null && object1.activeSelf)
        {
            videoPlayer.clip = videoClip1;

        }
        // 오브젝트2가 활성화되었을 때 2번 영상을 할당 및 재생
        if (object2 != null && object2.activeSelf)
        {
            videoPlayer.clip = videoClip2;
        }
    }
}
