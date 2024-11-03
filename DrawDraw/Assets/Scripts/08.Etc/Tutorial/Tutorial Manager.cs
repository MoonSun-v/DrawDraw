using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ����
    public Button  TutorialButton; // ��Ȱ��ȭ�� ��ư ������Ʈ

    public GameObject videoPlayerObject; // ���� �÷��̾� ������Ʈ
    private VideoPlayer videoPlayer;

    public GameObject Input;

    public RawImage TutorialRawImage; // Ȱ��ȭ�� RawImage
    public Image TutorialBG;


    // Start is called before the first frame update
    void Start()
    {
        if (videoPlayerObject != null)
        {
            videoPlayer = videoPlayerObject.GetComponent<VideoPlayer>();
            //videoPlayerObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ

            // ������ ���� �� �̺�Ʈ ���
            videoPlayer.loopPointReached += OnVideoEnd;

            // �ʱ⿡�� RawImage�� ��Ȱ��ȭ ���·� ����
            TutorialRawImage.gameObject.SetActive(false);
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
        if (videoPlayer != null && Input != null)
        {
            videoPlayerObject.SetActive(true); // ���� ������Ʈ Ȱ��ȭ
            videoPlayer.Play(); // ���� ���
            Debug.Log("Ʃ�丮�� ���");
            Input.SetActive(false);
        }

        

    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ���� ����� ������ ������Ʈ ��Ȱ��ȭ
        videoPlayerObject.SetActive(false);
        TutorialRawImage.gameObject.SetActive(false) ;
        TutorialBG.gameObject.SetActive(false);

        if (Input != null) { Input.SetActive(true); }
    }
}

