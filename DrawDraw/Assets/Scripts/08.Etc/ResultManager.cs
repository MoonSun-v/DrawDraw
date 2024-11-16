using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    // gameResult : ���� ��� �������� ��ũ���ͺ������Ʈ
    // StageNum   : �÷����� �������� ������ ���ڷ� ǥ���ϴ� ���� 
    //              �������� ������� 0��~18���� ���ڸ� ������. 
    //
    public GameResultSO gameResult;
    private int StageNum = 30;


    // ( �ӽ� ������ )
    //public Text scoreText;  // ������Ÿ�Կ����� ��� 
    private bool isClear;   // ���� Ŭ�����ߴ°�? 

    //���� ���� ĳ���� �̹���
    public Image characterImage;
    public Sprite[] CatImages;
    public Sprite[] DogImages;

    // ����̿� ������ �Ҹ� mp3 ���� ����Ʈ
    public AudioClip[] catSounds;  // ����� �Ҹ� ���
    public AudioClip[] dogSounds;  // ������ �Ҹ� ���
    public AudioSource audioSource; // ����� �ҽ�

    // �÷��̾� ĳ���� ���� �ҷ�����
    bool isDog = !GameData.instance.playerdata.PlayerCharacter;  // �������� true, ����̸� false



    // �� [ �� �Ʒ� ���Ӻ� �������� �̸� ]
    List<string> dotLineScenes = new List<string> { "DotLineScene1", "DotLineScene2", "DotLineScene3" };
    List<string> LineScenes = new List<string> { "1LineScene", "2LineScene", "3LineScene", "4LineScene", "5LineScene", "6LineScene" };
    List<string> ScratchScenes = new List<string> { "ScratchScene1", "ScratchScene2" };
    List<string> FigureCombiScenes = new List<string> { "1Pinwheel", "1Sun", "2Rocket", "2Ship", "3Person", "3TheTrain" };
    List<string> TangramScenes = new List<string> { "TangramScene_Lv1_boat","TangramScene_Lv1_duck", "TangramScene_Lv2", "TangramScene_Lv3" };
    List<string> PuzzleScenes = new List<string> { "PuzzleScene_1", "PuzzleScene_2" };


    // -----------------------------------------------------------------------------------------------------
    // �� [ �� �Ʒ� ���Ӻ�, ��� ���� ] ��  ----------------------------------------------------------------
    //
    void Start()
    {

        // 1. [ ���� ���� �׸��� ]
        // 50 % �̸�           : ����ġ X  ,���� ����
        // 50 % �̻� 60 % �̸� : ����ġ 5  ,���� ����
        // 60 % �̻�           : ����ġ 10 ,���� ����

        if (dotLineScenes.Contains(gameResult.previousScene))
        {
            #region previousScene�� ���� StageNum �Ҵ�

            if (gameResult.previousScene == "DotLineScene1") { StageNum = 0; }
            else if (gameResult.previousScene == "DotLineScene2") { StageNum = 2; }
            else if (gameResult.previousScene == "DotLineScene3") { StageNum = 4; }

            #endregion

            if (gameResult.score < 50) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 60) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }
        }



        // 2. [ �� ���� �׸��� ]
        // 6�� �̻��� �浹 : ���� ����
        // 60 % �̸�           : ����ġ X  ,���� ����
        // 60 % �̻� 80 % �̸� : ����ġ 5  ,���� ����
        // 80 % �̻�           : ����ġ 10 ,���� ����

        else if (LineScenes.Contains(gameResult.previousScene))
        {
            #region previousScene�� ���� StageNum �Ҵ�

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


        // 3. [ ��ũ��ġ ]
        // 60 % �̸�           : ����ġX   ,���� ����
        // 60 % �̻� 80 % �̸� : ����ġ 5  ,���� ����
        // 80 % �̻�           : ����ġ 10 ,���� ����

        else if (ScratchScenes.Contains(gameResult.previousScene))
        {
            #region previousScene�� ���� StageNum �Ҵ�

            if (gameResult.previousScene == "ScratchScene1") { StageNum = 6; }
            else if (gameResult.previousScene == "ScratchScene2") { StageNum = 12; }

            #endregion

            if (gameResult.score < 60) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }

        }


        // 4. [ �������� ]
        // ��ĥ�� ������ ���� )  -2�� �̻�   : ����ġX   ,���� ����
        //                       -1��        : ����ġ 5  ,���� ����
        //                       ��� ����   : ����ġ 10 ,���� ����

        // => ����ȭ �Ǿ��ִ� ��? �ϴ� < 60, 80 > �������� �����س��� 

        else if (FigureCombiScenes.Contains(gameResult.previousScene))
        {
            #region previousScene�� ���� StageNum �Ҵ�

            if (gameResult.previousScene == "1Pinwheel" || gameResult.previousScene == "1Sun") { StageNum = 7; }
            else if (gameResult.previousScene == "2Rocket" || gameResult.previousScene == "2Ship") { StageNum = 9; }
            else if (gameResult.previousScene == "3Person" || gameResult.previousScene == "3TheTrain") { StageNum = 13; }

            #endregion

            if (gameResult.score < 60) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score < 80) { isClear = true; SuccessSetting(StageNum); }
            else { isClear = true; ClearSetting(StageNum); }

        }


        // 5. [ ĥ�� ]
        // ���õ� ���� ��ġ�ϴ°� )  No  : ����ġX   ,���� ����
        //                             Yes : ����ġ 10 ,���� ����

        else if (TangramScenes.Contains(gameResult.previousScene))
        {
            #region previousScene�� ���� StageNum �Ҵ�

            if (gameResult.previousScene == "TangramScene_Lv1_boat" || gameResult.previousScene == "TangramScene_Lv1_duck") { StageNum = 10; }
            else if (gameResult.previousScene == "TangramScene_Lv2") { StageNum = 15; }
            else if (gameResult.previousScene == "TangramScene_Lv3") { StageNum = 18; }

            #endregion

            if (gameResult.score == 0) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score == 100) { isClear = true; ClearSetting(StageNum); }
            else { Debug.LogWarning("gameResult.score ���� �߸� �Ҵ�Ǿ����ϴ�. 0 �Ǵ� 100�� �Ҵ����ּ���"); }

        }


        // 6. [ ���� ]
        // ��� ������ �˸°� �������°� )  No  : ����ġX   ,���� ����
        //                                  Yes : ����ġ 10 ,���� ����

        else if (PuzzleScenes.Contains(gameResult.previousScene))
        {
            #region previousScene�� ���� StageNum �Ҵ�

            if (gameResult.previousScene == "PuzzleScene_1") { StageNum = 14; }
            else if (gameResult.previousScene == "PuzzleScene_2") { StageNum = 17; }

            #endregion

            if (gameResult.score == 0) { isClear = false; FailSetting(StageNum); }
            else if (gameResult.score == 100) { isClear = true; ClearSetting(StageNum); }
            else { Debug.LogWarning("gameResult.score ���� �߸� �Ҵ�Ǿ����ϴ�. 0 �Ǵ� 100�� �Ҵ����ּ���"); }

        }


        // ĳ���� ���� ����
        SetCharacterState(isClear, isDog);

        StartCoroutine(ChangeSceneAfterDelay(5f));

        GameData.instance.SavePlayerData();
        GameData.instance.SaveTrainingData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadTrainingData();

    }



    // �� [ �Ʒð��� ��� ���� �����ϴ� �޼ҵ� ] �� -------------------------------------------------
    //
    // 1. FailSetting()    : ����ġ X,  ���� ���� -> 3ȸ �̻� ���� ��, ��Ʈ �̺�Ʈ �۵� 
    // 2. SuccessSetting() : ����ġ 5,  ���� ���� 
    // 3. ClearSetting()   : ����ġ 10, ���� ����  
    // => �� �Լ��� ������ ���������� ���ڸ� �޾ƿ´�.=(int stagenum) 

    void FailSetting(int stagenum)
    {
        // GameData.instance.trainingdata.FailNum[stagenum] += 1;
        // print($"{stagenum}�� ���������� ���� Ƚ�� ���� �Ϸ�");
    }

    void SuccessSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 5;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;

        // print($"{stagenum}�� ���������� ����� ���� �Ϸ�");
    }

    void ClearSetting(int stagenum)
    {
        GameData.instance.playerdata.PlayerExp += 10;
        GameData.instance.trainingdata.ClearStage[stagenum] = true;

        // print($"{stagenum}�� ���������� ����� ���� �Ϸ�");
    }

    // �� [ �� �̵� ��ư ] -----------------------------------------------------------------------------
    //
    // Restart() : ���� ������ ������ ���ư���
    // End()     : ��ȭ������ ���ư���

    public void Restart() { SceneManager.LoadScene(gameResult.previousScene); }
    public void End() { SceneManager.LoadScene("MapScene"); }

    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("MapScene");
    }

    // �� [ ��� ȭ�� ����/���� �̹��� ����ϴ� �޼ҵ� ] ---------------------------------------------------
    //
    // isClear : ����(0,1,2) or ����(3,4,5)
    // isDog   : ����� ������
    public void SetCharacterState(bool isClear, bool isDog)
    {
        // ���� ���ο� ���� �ε��� ����
        int index = isClear ? Random.Range(0, 3) : Random.Range(3, 6);

        // ĳ���� ���¿� ���� �̹����� ���� ���� �� ���
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

        // ����� ���
        audioSource.Play();

        // ȿ�� Ȱ��ȭ
        Effect(isClear, index);
    }

    // �� [ ����/���� ����Ʈ�� Ȱ��ȭ�ϴ� �޼ҵ� ] ---------------------------------------------------
    //

    // ���� ������Ʈ���� �迭�� ����
    public GameObject[] successObjects;

    // ���� ������Ʈ
    public GameObject[] failObjects;

    // ���� ���θ� ó���ϴ� �޼���
    public void Effect(bool isClear, int index)
    {
        if (isClear)
        {
            // ��� ���� ������Ʈ�� Ȱ��ȭ
            foreach (GameObject obj in successObjects)
            {
                obj.SetActive(true);
            }

            // ��� ���� ������Ʈ�� ��Ȱ��ȭ
            foreach (GameObject obj in failObjects)
            {
                obj.SetActive(false);
            }

            // �ε����� ���� successObjects[0]�� successObjects[1] ��ġ ����
            switch (index)
            {
                case 0: // �����༭ ����
                    successObjects[0].transform.position = new Vector3(-2, 0.4f, -1); // ���ϴ� ��ġ ����
                    successObjects[1].transform.position = new Vector3(-2.5f, -2, -1); // ���ϴ� ��ġ ����
                    successObjects[2].transform.position = new Vector3(3.14f, -0.37f, -1); // ���ϴ� ��ġ ����
                    successObjects[3].transform.position = new Vector3(3, -2, -1); // ���ϴ� ��ġ ����
                    break;

                case 1: // ��� ����� �� ���� ���Ѵ�
                    successObjects[0].transform.position = new Vector3(-4, -1.5f, -1); // ���ϴ� ��ġ ����
                    successObjects[1].transform.position = new Vector3(-2.5f, 0.6f, -1); // ���ϴ� ��ġ ����
                    successObjects[2].transform.position = new Vector3(3.3f, 0.8f, -1); // ���ϴ� ��ġ ����
                    successObjects[3].transform.position = new Vector3(4, -2, -1); // ���ϴ� ��ġ ����
                    break;

                case 2: // �� ���� �ְ��
                    successObjects[0].transform.position = new Vector3(-3, -1.5f, -1); // ���ϴ� ��ġ ����
                    successObjects[1].transform.position = new Vector3(-2, 0.3f, -1); // ���ϴ� ��ġ ����
                    successObjects[2].transform.position = new Vector3(3.3f, 1, -1); // ���ϴ� ��ġ ����
                    successObjects[3].transform.position = new Vector3(4.5f, -1, -1); // ���ϴ� ��ġ ����
                    break;
                default:
                    // �ٸ� �ε����� ��� �⺻ ��ġ ����(�ʿ� �� �߰�)
                    break;
            }


        }
        else
        {
            // ��� ���� ������Ʈ�� ��Ȱ��ȭ
            foreach (GameObject obj in successObjects)
            {
                obj.SetActive(false);
            }
            // ���� ������Ʈ�� Ȱ��ȭ
            // index�� 3�� ��� failObjects[0] Ȱ��ȭ
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
