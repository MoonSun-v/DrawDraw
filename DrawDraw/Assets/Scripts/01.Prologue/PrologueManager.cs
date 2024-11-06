using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PrologueManager : MonoBehaviour
{
    public Button nextSceneButton; // ��ư ������Ʈ�� �Ҵ�
    public float delay = 3.0f;     // ���� �ð� (�� ����)

    public VideoPlayer videoPlayer;   // VideoPlayer ������Ʈ�� �Ҵ�

    private void Start()
    {
        // ��ư�� ó���� ��Ȱ��ȭ
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // ���� �ð� �� ��ư�� Ȱ��ȭ�ϴ� �ڷ�ƾ ����
        StartCoroutine(ShowButtonAfterDelayCoroutine());

        // ���� ��� �Ϸ� �� ȣ��� �̺�Ʈ ���
        videoPlayer.loopPointReached += OnVideoEnd;
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
        SceneManager.LoadScene("Profile");
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ������ �̵�
        SceneManager.LoadScene("Profile");
    }
}
