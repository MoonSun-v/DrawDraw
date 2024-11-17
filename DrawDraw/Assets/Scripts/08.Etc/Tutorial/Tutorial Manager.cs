using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ����
    public Button  TutorialButton; // ��Ȱ��ȭ�� ��ư ������Ʈ

    public GameObject videoPlayerObject1; // ù ��° ���� �÷��̾� ������Ʈ
    public GameObject videoPlayerObject2; // �� ��° ���� �÷��̾� ������Ʈ

    private VideoPlayer videoPlayer;

    public GameObject Input;

    public RawImage TutorialRawImage; // Ȱ��ȭ�� RawImage
    public Image TutorialBG;

    public GameObject object1; // ù ��° ������Ʈ
    public GameObject object2; // �� ��° ������Ʈ

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

        // �ʱ� ���� ����
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
        // ������� ��� ���� �� ��ư ������Ʈ�� ��Ȱ��ȭ
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
            // RawImage�� Ȱ��ȭ
            TutorialRawImage.gameObject.SetActive(true);
            TutorialBG.gameObject.SetActive(true);
        }

        // ������Ʈ 1�� Ȱ��ȭ�� ���
        if (object1.activeSelf)
        {
            videoPlayer = videoPlayerObject1.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                // VideoPlayer�� RenderTexture�� RawImage�� texture�� ����
                TutorialRawImage.texture = videoPlayer.targetTexture;

                videoPlayerObject1.SetActive(true);
                videoPlayer.Play();
                Debug.Log("ù ��° ���� ���");
            }
        }
        // ������Ʈ 2�� Ȱ��ȭ�� ���
        else if (object2.activeSelf)
        {
            videoPlayer = videoPlayerObject2.GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                videoPlayerObject2.SetActive(true);
                // VideoPlayer�� RenderTexture�� RawImage�� texture�� ����
                TutorialRawImage.texture = videoPlayer.targetTexture;
                videoPlayer.Play();
                Debug.Log("�� ��° ���� ���");
            }
        }

        if (Input != null)
        {
            Input.SetActive(false);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ���� ����� ������ RawImage�� ���� ������Ʈ ��Ȱ��ȭ
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

