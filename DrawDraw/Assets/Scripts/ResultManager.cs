using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        // 이전 게임의 씬으로 돌아가기 

    }

    public void End()
    {
        // 맵화면으로 돌아가기
        SceneManager.LoadScene("MapScene");
    }
}
