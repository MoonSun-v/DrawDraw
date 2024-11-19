using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    // ��  ������ ������ ���� ������ �Ѿ �� -> ���� �ڵ� �߰� ���ּ��� �� 
    // GameData.instance.trainingdata.ClearStage[19] = true; // : ��ȭ�� ������ �Ϸ���� ó�� ���ؼ�..!

    public Button nextSceneButton; // ��ư ������Ʈ�� �Ҵ�
    public float delay = 3.0f;     // ���� �ð� (�� ����)

    public VideoPlayer videoPlayer;   // VideoPlayer ������Ʈ�� �Ҵ�

    private bool userPreference = false; // ����� ������ ������� ���� ("dog" �Ǵ� "cat")

    public VideoClip dogVideoClip; // ù ��° ���� �÷��̾�
    public VideoClip catVideoClip; // �� ��° ���� �÷��̾�

    private void Start()
    {
        // ��ư�� ó���� ��Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // ���� �ð� �� ��ư�� Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        StartCoroutine(ShowButtonAfterDelayCoroutine());

        // ���� ��� �Ϸ� �� ȣ��� �̺�Ʈ ���
        videoPlayer.loopPointReached += OnVideoEnd;

        //userPreference = GameData.instance.playerdata.PlayerCharacter;

        if (GameData.instance.playerdata.PlayerCharacter)
        {
            userPreference = GameData.instance.playerdata.PlayerCharacter;

        }
        else
        {
            userPreference = false;
        }

        PlaySelectedVideo();
    }

    private IEnumerator ShowButtonAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delay);

        // ��ư Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(true);
    }

    private void OnNextSceneButtonClicked()
    {
        // ���� ���� ���� ������ �̵�
        GameData.instance.trainingdata.ClearStage[19] = true; // : ��ȭ�� ������ �Ϸ���� ó�� ���ؼ�..!
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadTrainingData();
        SceneManager.LoadScene("StartScene");
        // SceneManager.LoadScene("MapScene");
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ������ �̵�
        GameData.instance.trainingdata.ClearStage[19] = true; // : ��ȭ�� ������ �Ϸ���� ó�� ���ؼ�..!
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadTrainingData();
        SceneManager.LoadScene("StartScene");
        // SceneManager.LoadScene("MapScene");
    }

    public void PlaySelectedVideo()
    {
        // ���� ��� ���� ���� ����
        videoPlayer.Stop();

        // bool ���� ���� ���� ����
        // PlayerCharacter: false->������ , true->����� 
        if (userPreference == false && dogVideoClip != null)
        {
            videoPlayer.clip = dogVideoClip;
        }
        else if (userPreference == true && catVideoClip != null)
        {
            videoPlayer.clip = catVideoClip;
        }

        // ������ ���� ���
        videoPlayer.Play();
    }
}
