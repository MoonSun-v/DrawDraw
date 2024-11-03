using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSourceManager : MonoBehaviour
{
    public GameObject object1; // ù ��° ������Ʈ
    public GameObject object2; // �� ��° ������Ʈ

    public VideoClip videoClip1; // ù ��° ����
    public VideoClip videoClip2; // �� ��° ����

    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ

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

        // ������Ʈ1�� Ȱ��ȭ�Ǿ��� �� 1�� ������ �Ҵ� �� ���
        if (object1 != null && object1.activeSelf)
        {
            videoPlayer.clip = videoClip1;

        }
        // ������Ʈ2�� Ȱ��ȭ�Ǿ��� �� 2�� ������ �Ҵ� �� ���
        if (object2 != null && object2.activeSelf)
        {
            videoPlayer.clip = videoClip2;
        }
    }
}
