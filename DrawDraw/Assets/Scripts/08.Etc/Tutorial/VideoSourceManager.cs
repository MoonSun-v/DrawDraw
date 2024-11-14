using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSourceManager : MonoBehaviour
{
    public GameObject object1; // ù ��° ������Ʈ
    public GameObject object2; // �� ��° ������Ʈ

    public VideoClip videoClip1; // ù ��° ����
    public VideoClip videoClip2; // �� ��° ����

    public RenderTexture renderTexture1;  // ù ��° Ÿ�� �ؽ�ó
    public RenderTexture renderTexture2;  // �� ��° Ÿ�� �ؽ�ó

    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ

    public RawImage rawImage;             // ������ ǥ���� RawImage

    void Update()
    {
        if (videoPlayer != null)
        {
            // �� �����Ӹ��� ������Ʈ Ȱ��ȭ ���� Ȯ�� �� ���� Ŭ���� �ؽ�ó ����
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
        // ���� Ŭ���� �ؽ�ó �Ҵ�
        if (videoPlayer.clip != clip || videoPlayer.targetTexture != targetTexture)
        {   
            videoPlayer.clip = clip;
            videoPlayer.targetTexture = targetTexture;

            // RawImage �ؽ�ó ������Ʈ
            rawImage.texture = targetTexture;
        }
    }
}
