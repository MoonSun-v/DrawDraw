using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestDotLine : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 MousePosition;

    // ���� ���� : 30
    private int TotalDot = 30;

    private float DotCount;          // �浹�� ���� ����
    private int Score;               // �� ����

    public GameObject CheckPopup;    // Ȯ�� �˾� â

    public Draw draw;


    private void Awake()
    {
        mainCamera = Camera.main;    // "Maincamera" �±׸� ������ �ִ� ������Ʈ Ž�� �� Camera ������Ʈ ���� ����
    }


    void Update()
    {
        // ���콺 ������ �ִ� ���� 
        if (Input.GetMouseButton(0))
        {
            MousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);             // ���콺 Ŭ�� ��ġ �� -> ���� ��ǥ ��ġ �� 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit)                                                                       // �浹�� �ִٸ� 
            {
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;            // �ش� ������Ʈ �ݸ��� ��Ȱ��ȭ (�浹 1���� �߻���Ű�� ����)

                DotCount += 1;

            }

        }
    }


    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }


    // �˾� : �����̾�
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }


    // �˾� : �ϼ��̾� 
    public void NextBtn()
    {
        print("�浹�� ���� ���� = " + DotCount);
        Score = (int)((DotCount / TotalDot) * 100);         // �Ҽ��� ���ϴ� ���� 
        print("�ۼ�Ʈ = " + Score + "%");

        SaveResults(Score);

        NextGame();
    }

    // [ ������ ���� ]
    void SaveResults(int _score)
    {
        if (_score == 0) { _score += 1; }

        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 4)
        {
            Debug.LogWarning("TestResults�� �� �̻� ������ �� �����ϴ�. �ִ� Ű ���� 4�Դϴ�.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        TestResultData currentData = GameData.instance.testdata.TestResults[currentKey];
        currentData.Game8Score = _score;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]�� DotLine ���� = {_score} ���� �Ϸ�");
    }

    void NextGame()
    {
        SceneManager.LoadScene("Test_ShapesClassifyScene");
    }


}
