using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 참조
    public Button  TutorialButton; // 비활성화할 버튼 오브젝트

    public GameObject videoPlayerObject1; // 첫 번째 비디오 플레이어 오브젝트
    public GameObject videoPlayerObject2; // 두 번째 비디오 플레이어 오브젝트

    private VideoPlayer videoPlayer;

    public GameObject Input;

    public RawImage TutorialRawImage; // 활성화할 RawImage
    public Image TutorialBG;

    public GameObject object1; // 첫 번째 오브젝트
    public GameObject object2; // 두 번째 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayerObject1 != null)
        {
            var videoPlayer1 = videoPlayerObject1.GetComponent<VideoPlayer>();
            if (videoPlayer1 != null)
            {
                videoPlayer1.loopPointReached += OnVideoEnd;
            }
        }

        if (videoPlayerObject2 != null)
        {
            var videoPlayer2 = videoPlayerObject2.GetComponent<VideoPlayer>();
            if (videoPlayer2 != null)
            {
                videoPlayer2.loopPointReached += OnVideoEnd;
            }
        }

        // 초기 상태 설정
        if (TutorialRawImage != null)
        {
            TutorialRawImage.gameObject.SetActive(false);
        }

        if (TutorialBG != null)
        {
            TutorialBG.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 오디오가 재생 중일 때 버튼 오브젝트를 비활성화
        if (audioSource != null && TutorialButton != null)
        {
            if (audioSource.isPlaying)
            {
                TutorialButton.interactable = false;
            }
            else
            {
                TutorialButton.interactable = true;
            }
        }
    }

    public void PlayVideo()
    {
        if (TutorialRawImage != null)
        {
            // RawImage를 활성화
            TutorialRawImage.gameObject.SetActive(true);
            TutorialBG.gameObject.SetActive(true);
        }

        // 오브젝트 1이 활성화된 경우
        if (object1.activeSelf)
        {
            videoPlayer = videoPlayerObject1.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                // VideoPlayer의 RenderTexture를 RawImage의 texture로 설정
                TutorialRawImage.texture = videoPlayer.targetTexture;

                videoPlayerObject1.SetActive(true);
                videoPlayer.Play();
                Debug.Log("첫 번째 비디오 재생");
            }
        }
        // 오브젝트 2가 활성화된 경우
        else if (object2.activeSelf)
        {
            videoPlayer = videoPlayerObject2.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                videoPlayerObject2.SetActive(true);
                // VideoPlayer의 RenderTexture를 RawImage의 texture로 설정
                TutorialRawImage.texture = videoPlayer.targetTexture;
                videoPlayer.Play();
                Debug.Log("두 번째 비디오 재생");
            }
        }

        if (Input != null)
        {
            Input.SetActive(false);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 영상 재생이 끝나면 RawImage와 비디오 오브젝트 비활성화
        if (videoPlayerObject1 != null)
            videoPlayerObject1.SetActive(false);

        if (videoPlayerObject2 != null)
            videoPlayerObject2.SetActive(false);

        TutorialRawImage.gameObject.SetActive(false);
        TutorialBG.gameObject.SetActive(false);

        if (Input != null)
        {
            Input.SetActive(true);
        }
    }
}

