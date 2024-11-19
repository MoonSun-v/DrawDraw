using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    // ★  엔딩씬 끝나서 다음 씬으로 넘어갈 때 -> 다음 코드 추가 해주세요 ★ 
    // GameData.instance.trainingdata.ClearStage[19] = true; // : 맵화면 아이콘 완료버전 처리 위해서..!

    public Button nextSceneButton; // 버튼 오브젝트를 할당
    public float delay = 3.0f;     // 지연 시간 (초 단위)

    public VideoPlayer videoPlayer;   // VideoPlayer 컴포넌트를 할당

    private bool userPreference = false; // 사용자 정보를 기반으로 결정 ("dog" 또는 "cat")

    public VideoClip dogVideoClip; // 첫 번째 비디오 플레이어
    public VideoClip catVideoClip; // 두 번째 비디오 플레이어

    private void Start()
    {
        // 버튼을 처음에 비활성화
        nextSceneButton.gameObject.SetActive(false);
        nextSceneButton.onClick.AddListener(OnNextSceneButtonClicked);

        // 지연 시간 후 버튼을 활성화하는 코루틴 시작
        StartCoroutine(ShowButtonAfterDelayCoroutine());

        // 비디오 재생 완료 시 호출될 이벤트 등록
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

        // 버튼 활성화
        nextSceneButton.gameObject.SetActive(true);
    }

    private void OnNextSceneButtonClicked()
    {
        // 현재 씬의 다음 씬으로 이동
        GameData.instance.trainingdata.ClearStage[19] = true; // : 맵화면 아이콘 완료버전 처리 위해서..!
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadTrainingData();
        SceneManager.LoadScene("StartScene");
        // SceneManager.LoadScene("MapScene");
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 지정된 씬으로 이동
        GameData.instance.trainingdata.ClearStage[19] = true; // : 맵화면 아이콘 완료버전 처리 위해서..!
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadTrainingData();
        SceneManager.LoadScene("StartScene");
        // SceneManager.LoadScene("MapScene");
    }

    public void PlaySelectedVideo()
    {
        // 현재 재생 중인 비디오 정지
        videoPlayer.Stop();

        // bool 값에 따라 비디오 선택
        // PlayerCharacter: false->강아지 , true->고양이 
        if (userPreference == false && dogVideoClip != null)
        {
            videoPlayer.clip = dogVideoClip;
        }
        else if (userPreference == true && catVideoClip != null)
        {
            videoPlayer.clip = catVideoClip;
        }

        // 선택한 비디오 재생
        videoPlayer.Play();
    }
}
