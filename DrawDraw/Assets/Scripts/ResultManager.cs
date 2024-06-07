using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameResultSO gameResult;
    public Text scoreText; // ������Ÿ�Կ����� ��� 

    private bool isClear; // ���� Ŭ�����ߴ°�?

    // Start is called before the first frame update
    void Start()
    {
        // �� ���Ӻ�, ��� ����

        // ��ũ��ġ 
        // 60 % �̸�: ���� ����
        // 60 % �̻� 80 % �̸� : ����ġ 5
        // 80 % �̻� : ����ġ 10
        if (gameResult.previousScene == "ScratchScene")
        {
            if(gameResult.score < 60) // ���� ����
            {
                isClear = false;
            }
            else // ���� Ŭ����
            {
                isClear = true;
            }
            
        }
        
        if(isClear)
        {
            scoreText.text = "�����ϰ� �ִ� ĳ����";
        }
        else
        {
            scoreText.text = "�ƽ����ϴ� ĳ����";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        // ���� ������ ������ ���ư��� 
        SceneManager.LoadScene(gameResult.previousScene);
    }

    public void End()
    {
        // ��ȭ������ ���ư���
        SceneManager.LoadScene("MapScene");
    }
}
