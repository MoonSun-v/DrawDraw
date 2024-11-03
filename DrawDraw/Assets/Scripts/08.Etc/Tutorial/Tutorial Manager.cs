using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 참조
    public Button  TutorialButton; // 비활성화할 버튼 오브젝트

    public GameObject videoPlayerObject; // 영상 플레이어 오브젝트
    private VideoPlayer videoPlayer;

    public GameObject Input;

    public RawImage TutorialRawImage; // 활성화할 RawImage
    public Image TutorialBG;


    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayerObject != null)
        {
            videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
            //videoPlayerObject.SetActive(false); // 초기에는 비활성화

            // 영상이 끝날 때 이벤트 등록
            videoPlayer.loopPointReached += OnVideoEnd;

            // 초기에는 RawImage를 비활성화 상태로 설정
            TutorialRawImage.gameObject.SetActive(false);
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
        if (videoPlayer != null && Input != null)
        {
            videoPlayerObject.SetActive(true); // 영상 오브젝트 활성화
            videoPlayer.Play(); // 영상 재생
            Debug.Log("튜토리얼 재생");
            Input.SetActive(false);
        }

        

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 영상 재생이 끝나면 오브젝트 비활성화
        videoPlayerObject.SetActive(false);
        TutorialRawImage.gameObject.SetActive(false) ;
        TutorialBG.gameObject.SetActive(false);

        if (Input != null) { Input.SetActive(true); }
    }
}

