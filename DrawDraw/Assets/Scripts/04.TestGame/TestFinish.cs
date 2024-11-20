using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.SceneManagement;

public class TestFinish : MonoBehaviour
{

    // 플레이어 캐릭터 정보 불러오기
    bool isDog;

    public GameObject cat;
    public GameObject dog;

    void Start()
    {
        isDog = !GameData.instance.playerdata.PlayerCharacter;  // 강아지면 true, 고양이면 false
        if (isDog) { dog.SetActive(false); }
        else { cat.SetActive(true); }

        StartCoroutine(LoadMapSceneAfterDelay());
    }

    IEnumerator LoadMapSceneAfterDelay()
    {
        // 5초 대기
        yield return new WaitForSeconds(5f);
        /*
        if (GameData.instance.testdata.TestResults.Count > 5)
        {
            // 마지막 테스트라면 -> 엔딩씬으로 이동
            // ** 스크립트 추가 필요 **
            print("마지막 테스트였으므로, 엔딩씬으로 이동합니다.");
            SceneManager.LoadScene("EndingScene");

            // 엔딩씬 끝낼때 다음 코드 추가 필요 
            // GameData.instance.trainingdata.ClearStage[19] = true; // 맵의 스테이지 아이콘 완료처리 위해서..!
        }
        else
        {
            // 마지막 테스트가 아니라면 -> 맵화면으로 이동 
            SceneManager.LoadScene("MapScene");
        }
        */
        // "MapScene" 이름의 씬으로 이동
        SceneManager.LoadScene("MapScene");
    }
}
