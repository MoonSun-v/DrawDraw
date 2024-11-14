using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSourceManager : MonoBehaviour
{
    public GameObject object1; // 첫 번째 오브젝트
    public GameObject object2; // 두 번째 오브젝트

    public VideoClip videoClip1; // 첫 번째 영상
    public VideoClip videoClip2; // 두 번째 영상

    public RenderTexture renderTexture1;  // 첫 번째 타겟 텍스처
    public RenderTexture renderTexture2;  // 두 번째 타겟 텍스처

    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트

    public RawImage rawImage;             // 비디오를 표시할 RawImage

    void Update()
    {
        if (videoPlayer != null)
        {
            // 매 프레임마다 오브젝트 활성화 상태 확인 후 비디오 클립과 텍스처 설정
            if (object1 != null && object1.activeSelf)
            {
                SetVideoSource(videoClip1, renderTexture1);
            }
            else if (object2 != null && object2.activeSelf)
            {
                SetVideoSource(videoClip2, renderTexture2);
            }
        }
    }

    private void SetVideoSource(VideoClip clip, RenderTexture targetTexture)
    {
        // 비디오 클립과 텍스처 할당
        if (videoPlayer.clip != clip || videoPlayer.targetTexture != targetTexture)
        {   
            videoPlayer.clip = clip;
            videoPlayer.targetTexture = targetTexture;

            // RawImage 텍스처 업데이트
            rawImage.texture = targetTexture;
        }
    }
}
