using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioSource bgmSource;  // 배경음 오디오 소스
    public AudioClip[] bgmClips;  // 씬에 따른 배경음악 클립 배열

    public AudioSource[] sfxSources;  // 여러 개의 효과음 오디오 소스

    void Awake()
    {
        // AudioManager의 중복 생성을 방지
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);  // 파괴되지 않도록 설정
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 로드 이벤트 연결
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // 이벤트 연결 해제
    }

    // 씬이 로드될 때 호출되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name);  // 현재 씬 이름에 따라 배경음악 재생
    }

    // 특정 씬에 맞는 배경음악 재생
    void PlayBGMForScene(string sceneName)
    {
        AudioClip newClip = null;

        switch (sceneName)
        {
            //Test BGM
            case "Test_1Line":
                newClip = bgmClips[1];
                break;
            case "Test_2Line":
                newClip = bgmClips[1];
                break;
            case "Test_3Line":
                newClip = bgmClips[1];
                break;
            case "Test_4Line":
                newClip = bgmClips[1];
                break;
            case "Test_5Line":
                newClip = bgmClips[1];
                break;
            case "Test_6Line":
                newClip = bgmClips[1];
                break;
            case "Test_ColoringScene":
                newClip = bgmClips[1];
                break;
            case "Test_DotLineScene":
                newClip = bgmClips[1];
                break;
            case "Test_ShapesClassifyScene":
                newClip = bgmClips[1];
                break;
            case "Test_ColorClassifyScene":
                newClip = bgmClips[1];
                break;

            //Test Start Silent
            case "TestStartScene":
                bgmSource.Pause(); 
                break;

            //Dot Line BGM
            case "T_DotLineScene1":
                newClip = bgmClips[2];
                break;
            case "DotLineScene1":
                newClip = bgmClips[2];
                break;
            case "DotLineScene2":
                newClip = bgmClips[2];
                break;
            case "DotLineScene3":
                newClip = bgmClips[2];
                break;

            //Line BGM
            case "T_1LineScene":
                newClip = bgmClips[2];
                break;
            case "1LineScene":
                newClip = bgmClips[2];
                break;
            case "2LineScene":
                newClip = bgmClips[2];
                break;
            case "3LineScene":
                newClip = bgmClips[2];
                break;
            case "4LineScene":
                newClip = bgmClips[2];
                break;
            case "5LineScene":
                newClip = bgmClips[2];
                break;
            case "6LineScene":
                newClip = bgmClips[2];
                break;

            //Scratch BGM
            case "T_ScratchScene1":
                newClip = bgmClips[2];
                break;
            case "ScratchScene1":
                newClip = bgmClips[2];
                break;
            case "ScratchScene2":
                newClip = bgmClips[2];
                break;

            //Combination BGM
            case "1FigureSelect":
                newClip = bgmClips[2];
                break;
            case "T_1Pinwheel":
                newClip = bgmClips[2];
                break;
            case "1Pinwheel":
                newClip = bgmClips[2];
                break;
            case "T_1Sun":
                newClip = bgmClips[2];
                break;
            case "1Sun":
                newClip = bgmClips[2];
                break;
            case "2FigureSelect":
                newClip = bgmClips[2];
                break;
            case "2Rocket":
                newClip = bgmClips[2];
                break;
            case "2Ship":
                newClip = bgmClips[2];
                break;
            case "3FigureSelect":
                newClip = bgmClips[2];
                break;
            case "3Person":
                newClip = bgmClips[2];
                break;
            case "3TheTrain":
                newClip = bgmClips[2];
                break;

            //Tangram BGM
            case "1TangramSelect":
                newClip = bgmClips[2];
                break;
            case "T_TangramScene_Lv1_boat":
                newClip = bgmClips[2];
                break;
            case "TangramScene_Lv1_boat":
                newClip = bgmClips[2];
                break;
            case "T_TangramScene_Lv1_duck":
                newClip = bgmClips[2];
                break;
            case "TangramScene_Lv1_duck":
                newClip = bgmClips[2];
                break;
            case "TangramScene_Lv2":
                newClip = bgmClips[2];
                break;
            case "TangramScene_Lv3":
                newClip = bgmClips[2];
                break;

            //Puzzle BGM
            case "T_PuzzleScene_1":
                newClip = bgmClips[2];
                break;
            case "PuzzleScene1":
                newClip = bgmClips[2];
                break;
            case "PuzzleScene2":
                newClip = bgmClips[2];
                break;


            //Parents BGM
            case "ParentsScene":
                newClip = bgmClips[2];
                break;

            default:
                newClip = bgmClips[0];  // 기본 BGM
                break;
        }

        if (newClip != null && bgmSource.clip != newClip)
        {
            bgmSource.clip = newClip;
            bgmSource.Play();
        }
    }

    // 배경음 볼륨 조절 함수
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // 효과음 볼륨 조절 함수
    public void SetSFXVolume(float volume)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = volume;
        }
    }
}
