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

        // "MapScene" 이름의 씬으로 이동
        SceneManager.LoadScene("MapScene");
    }
}
