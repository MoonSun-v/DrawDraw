using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    // gameResult : 게임 결과 가져오는 스크립터블오브젝트
    // StageNum   : 플레이한 스테이지 정보를 숫자로 표기하는 변수 
    //              스테이지 순서대로 0번~18번의 숫자를 가진다. 
    //
    public GameResultSO gameResult;
    private int StageNum = 30;


    // ( 임시 변수들 )
    //public Text scoreText;  // 프로토타입에서만 사용 
    private bool isClear;   // 게임 클리어했는가? 

    //성공 실패 캐릭터 이미지
    public Image characterImage;
    public Sprite[] CatImages;
    public Sprite[] DogImages;

    // 고양이와 강아지 소리 mp3 파일 리스트
    public AudioClip[] catSounds;  // 고양이 소리 목록
    public AudioClip[] dogSounds;  // 강아지 소리 목록
    public AudioSource audioSource; // 오디오 소스

    // 플레이어 캐릭터 정보 불러오기
    bool isDog = !GameData.instance.playerdata.PlayerCharacter;  // 강아지면 true, 고양이면 false



    // ★ [ 각 훈련 게임별 스테이지 이름 ]
    List<string> dotLineScenes = new List<string> { "DotLineScene1", "DotLineScene2", "DotLineScene3" };
    List<string> LineScenes = new List<string> { "1LineScene", "2LineScene", "3LineScene", "4LineScene", "5LineScene", "6LineScene" };
    List<string> ScratchScenes = new List<string> { "ScratchScene1", "ScratchScene2" };
    List<string> FigureCombiScenes = new List<string> { "1Pinwheel", "1Sun", "2Rocket", "2Ship", "3Person", "3TheTrain" };
    List<string> TangramScenes = new List<string> { "TangramScene_Lv1_boat","TangramScene_Lv1_duck", "TangramScene_Lv2", "TangramScene_Lv3" };
    List<string> PuzzleScenes = new List<string> { "PuzzleScene_1", "PuzzleScene_2" };


    // -----------------------------------------------------------------------------------------------------
    // ★ [ 각 훈련 게임별, 결과 세팅 ] ★  ----------------------------------------------------------------
    //
    void Start()
    {

        // 1. [ 점선 따라 그리기 ]
        // 50 % 미만           : 경험치 X  ,게임 실패
        // 50 % 이상 60 % 미만 : 경험치 5  ,게임 성공
        // 60 % 이상           : 경험치 10 ,게임 성공

        if (dotLineScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "DotLineScene1") { StageNum = 0; }
            else if (gameResult.previousScene == "DotLineScene2") { StageNum = 2; }
            else if (gameResult.previousScene == "DotLineScene3") { StageNum = 4; }

            #endregion

            if (gameResult.score < 50) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 60) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }
        }



        // 2. [ 선 따라 그리기 ]
        // 6번 이상의 충돌 : 게임 오버
        // 60 % 미만           : 경험치 X  ,게임 실패
        // 60 % 이상 80 % 미만 : 경험치 5  ,게임 성공
        // 80 % 이상           : 경험치 10 ,게임 성공

        else if (LineScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "1LineScene") { StageNum = 1; }
            else if (gameResult.previousScene == "2LineScene") { StageNum = 3; }
            else if (gameResult.previousScene == "3LineScene") { StageNum = 5; }
            else if (gameResult.previousScene == "4LineScene") { StageNum = 8; }
            else if (gameResult.previousScene == "5LineScene") { StageNum = 11; }
            else if (gameResult.previousScene == "6LineScene") { StageNum = 16; }

            #endregion

            if (gameResult.score < 60) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }
        }


        // 3. [ 스크래치 ]
        // 60 % 미만           : 경험치X   ,게임 실패
        // 60 % 이상 80 % 미만 : 경험치 5  ,게임 성공
        // 80 % 이상           : 경험치 10 ,게임 성공

        else if (ScratchScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "ScratchScene1") { StageNum = 6; }
            else if (gameResult.previousScene == "ScratchScene2") { StageNum = 12; }

            #endregion

            if (gameResult.score < 60) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }

        }


        // 4. [ 도형조합 ]
        // 색칠된 도형의 개수 )  -2개 이상   : 경험치X   ,게임 실패
        //                       -1개        : 경험치 5  ,게임 성공
        //                       모든 도형   : 경험치 10 ,게임 성공

        // => 점수화 되어있는 듯? 일단 < 60, 80 > 기준으로 세팅해놓음 

        else if (FigureCombiScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "1Pinwheel" || gameResult.previousScene == "1Sun") { StageNum = 7; }
            else if (gameResult.previousScene == "2Rocket" || gameResult.previousScene == "2Ship") { StageNum = 9; }
            else if (gameResult.previousScene == "3Person" || gameResult.previousScene == "3TheTrain") { StageNum = 13; }

            #endregion

            if (gameResult.score < 60) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }

        }


        // 5. [ 칠교 ]
        // 제시된 모양과 일치하는가 )  No  : 경험치X   ,게임 실패
        //                             Yes : 경험치 10 ,게임 성공

        else if (TangramScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "TangramScene_Lv1_boat" || gameResult.previousScene == "TangramScene_Lv1_duck") { StageNum = 10; }
            else if (gameResult.previousScene == "TangramScene_Lv2") { StageNum = 15; }
            else if (gameResult.previousScene == "TangramScene_Lv3") { StageNum = 18; }

            #endregion

            if (gameResult.score == 0) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score == 100) { isClear = true; ClearSetting(StageNum); }
            else { Debug.LogWarning("gameResult.score 값이 잘못 할당되었습니다. 0 또는 100을 할당해주세요"); }

        }


        // 6. [ 퍼즐 ]
        // 모든 퍼즐이 알맞게 맞춰졌는가 )  No  : 경험치X   ,게임 실패
        //                                  Yes : 경험치 10 ,게임 성공

        else if (PuzzleScenes.Contains(gameResult.previousScene))
        {
            #region previousScene에 따라 StageNum 할당

            if (gameResult.previousScene == "PuzzleScene_1") { StageNum = 14; }
            else if (gameResult.previousScene == "PuzzleScene_2") { StageNum = 17; }

            #endregion

            if (gameResult.score == 0) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score == 100) { isClear = true; ClearSetting(StageNum); }
            else { Debug.LogWarning("gameResult.score 값이 잘못 할당되었습니다. 0 또는 100을 할당해주세요"); }

        }


        // 캐릭터 상태 세팅
        SetCharacterState(isClear, isDog);

        StartCoroutine(ChangeSceneAfterDelay(5f));

        GameData.instance.SavePlayerData();
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();

    }



    // ★ [ 훈련게임 결과 정보 저장하는 메소드 ] ★ -------------------------------------------------
    //
    // 1. FailSetting()    : 경험치 X,  게임 실패 -> 3회 이상 실패 시, 힌트 이벤트 작동 
    // 2. SuccessSetting() : 경험치 5,  게임 성공 
    // 3. ClearSetting()   : 경험치 10, 게임 성공  
    // => 각 함수는 지정된 스테이지의 숫자를 받아온다.=(int stagenum) 

    void FailSetting(int stagenum)
    {
        // GameData.instance.trainingdata.FailNum[stagenum] += 1;
        // print($"{stagenum}번 스테이지의 실패 횟수 저장 완료");
    }

    void SuccessSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 5;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;

        // print($"{stagenum}번 스테이지의 결과값 저장 완료");
    }

    void ClearSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 10;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;

        // print($"{stagenum}번 스테이지의 결과값 저장 완료");
    }

    // ★ [ 씬 이동 버튼 ] -----------------------------------------------------------------------------
    //
    // Restart() : 이전 게임의 씬으로 돌아가기
    // End()     : 맵화면으로 돌아가기

    public void Restart() { SceneManager.LoadScene(gameResult.previousScene); }
    public void End() { SceneManager.LoadScene("MapScene"); }

    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("MapScene");
    }

    // ★ [ 결과 화면 성공/실패 이미지 출력하는 메소드 ] ---------------------------------------------------
    //
    // isClear : 성공(0,1,2) or 실패(3,4,5)
    // isDog   : 사용자 프로필
    public void SetCharacterState(bool isClear, bool isDog)
    {
        // 성공 여부에 따라 인덱스 설정
        int index = isClear ? Random.Range(0, 3) : Random.Range(3, 6);

        // 캐릭터 상태에 따라 이미지와 사운드 설정 및 재생
        if (isDog)
        {
            characterImage.sprite = DogImages[index];
            audioSource.clip = dogSounds[index];
        }
        else
        {
            characterImage.sprite = CatImages[index];
            audioSource.clip = catSounds[index];
        }

        // 오디오 재생
        audioSource.Play();

        // 효과 활성화
        Effect(isClear, index);
    }

    // ★ [ 성공/실패 이펙트를 활성화하는 메소드 ] ---------------------------------------------------
    //

    // 성공 오브젝트들을 배열로 선언
    public GameObject[] successObjects;

    // 실패 오브젝트
    public GameObject[] failObjects;

    // 성공 여부를 처리하는 메서드
    public void Effect(bool isClear, int index)
    {
        if (isClear)
        {
            // 모든 성공 오브젝트를 활성화
            foreach (GameObject obj in successObjects)
            {
                obj.SetActive(true);
            }

            // 모든 실패 오브젝트를 비활성화
            foreach (GameObject obj in failObjects)
            {
                obj.SetActive(false);
            }

            // 인덱스에 따라 successObjects[0]과 successObjects[1] 위치 설정
            switch (index)
            {
                case 0: // 잘해줘서 고마워
                    successObjects[0].transform.position = new Vector3(-2, 0.4f, -1); // 원하는 위치 설정
                    successObjects[1].transform.position = new Vector3(-2.5f, -2, -1); // 원하는 위치 설정
                    successObjects[2].transform.position = new Vector3(3.14f, -0.37f, -1); // 원하는 위치 설정
                    successObjects[3].transform.position = new Vector3(3, -2, -1); // 원하는 위치 설정
                    break;

                case 1: // 우와 대단해 너 정말 잘한다
                    successObjects[0].transform.position = new Vector3(-4, -1.5f, -1); // 원하는 위치 설정
                    successObjects[1].transform.position = new Vector3(-2.5f, 0.6f, -1); // 원하는 위치 설정
                    successObjects[2].transform.position = new Vector3(3.3f, 0.8f, -1); // 원하는 위치 설정
                    successObjects[3].transform.position = new Vector3(4, -2, -1); // 원하는 위치 설정
                    break;

                case 2: // 넌 정말 최고야
                    successObjects[0].transform.position = new Vector3(-3, -1.5f, -1); // 원하는 위치 설정
                    successObjects[1].transform.position = new Vector3(-2, 0.3f, -1); // 원하는 위치 설정
                    successObjects[2].transform.position = new Vector3(3.3f, 1, -1); // 원하는 위치 설정
                    successObjects[3].transform.position = new Vector3(4.5f, -1, -1); // 원하는 위치 설정
                    break;
                default:
                    // 다른 인덱스일 경우 기본 위치 설정(필요 시 추가)
                    break;
            }


        }
        else
        {
            // 모든 성공 오브젝트를 비활성화
            foreach (GameObject obj in successObjects)
            {
                obj.SetActive(false);
            }
            // 실패 오브젝트를 활성화
            // index가 3인 경우 failObjects[0] 활성화
            if (index == 3)
            {
                failObjects[1].SetActive(true);
            }
            else
            {
                failObjects[0].SetActive(true);
            }

        }
    }
}
