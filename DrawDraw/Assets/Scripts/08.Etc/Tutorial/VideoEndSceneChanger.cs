using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndSceneChanger : MonoBehaviour
{
    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ
    public string nextSceneName;    // �������� �̵��� �� �̸�

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        if (videoPlayer != null)
        {
            // VideoPlayer�� loopPointReached �̺�Ʈ�� �޼��� ���
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ���� ����� ������ �� ȣ��˴ϴ�.
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ��� ����
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}