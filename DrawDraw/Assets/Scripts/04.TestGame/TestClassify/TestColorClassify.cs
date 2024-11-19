using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestColorClassify : MonoBehaviour
{
    public GameObject[] Shapes;
    public GameObject[] AnswerShapes;

    public GameObject CheckPopup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        CheckPopup.SetActive(false);
        CheckAnswer();
        GoToNextScene();
        // SceneManager.LoadScene("SelectScene");
    }

    private void CheckAnswer()
    {
        int score = 0;  // ������ 0���� �ʱ�ȭ

        for (int i = 0; i < Shapes.Length; i++)
        {
            if (i >= AnswerShapes.Length)
            {
                Debug.LogWarning("AnswerShapes �迭�� ��Ұ� �����մϴ�.");
                return;
            }

            BoxCollider2D answerCollider = AnswerShapes[i].GetComponent<BoxCollider2D>();
            if (answerCollider == null)
            {
                Debug.LogWarning("AnswerShapes[" + i + "]�� BoxCollider2D ������Ʈ�� �����ϴ�.");
                return;
            }

            if (IsWithinCollider(Shapes[i], answerCollider))
            {
                Debug.Log("Shape " + i + "��(��) �ùٸ� �ڽ� �ݶ��̴� �ȿ� �ֽ��ϴ�.");
                score++;  // �ùٸ��� ��ġ�� �������� 1�� �߰�
            }
            else
            {
                Debug.Log("Shape " + i + "��(��) �ùٸ� �ڽ� �ݶ��̴� �ȿ� �����ϴ�.");
            }
        }

        Debug.Log("���� ����: " + score);  // ���� ���� ���

        // ���� ���� 
        SaveResults(score);

    }

    // [ ������ ���� ] : ���� ���� 4��
    //
    void SaveResults(int _score)
    {
        _score *= 25; // ���� 100���� ���� 
        if(_score == 0) { _score += 1; }

        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 5)
        {
            Debug.LogWarning("TestResults�� �� �̻� ������ �� �����ϴ�. �ִ� Ű ���� 5�Դϴ�.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        TestResultData currentData = GameData.instance.testdata.TestResults[currentKey];
        currentData.Game10Score = _score;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]�� ColorClassify ���� = {_score} ���� �Ϸ�");
    }

    private bool IsWithinCollider(GameObject shape, BoxCollider2D collider)
    {
        Bounds shapeBounds = shape.GetComponent<Collider2D>().bounds;
        return collider.bounds.Intersects(shapeBounds);
    }

    private void GoToNextScene()
    {
        if( GameData.instance.testdata.TestResults.Count > 5 )
        {
            // ������ �׽�Ʈ��� -> ���������� �̵�
            // ** ��ũ��Ʈ �߰� �ʿ� **
            print("������ �׽�Ʈ�����Ƿ�, ���������� �̵��մϴ�.");
            SceneManager.LoadScene("EndingScene");

            // ������ ������ ���� �ڵ� �߰� �ʿ� 
            // GameData.instance.trainingdata.ClearStage[19] = true; // ���� �������� ������ �Ϸ�ó�� ���ؼ�..!
        }
        else
        {
            // ������ �׽�Ʈ�� �ƴ϶�� -> ����ȭ������ �̵� 
            SceneManager.LoadScene("MapScene");
        }
    }
}
