using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParentsQuiz : MonoBehaviour
{
    public GameObject QuizCanvas;

    public Text answerText;
    public Text quizText;
    private string currentText = "";
    public string answer = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showParentsQuiz()
    {
        QuizCanvas.SetActive(true);
        QuizRand();
    }

    public void cancelParentsQuiz()
    {
        QuizCanvas.SetActive(false);
        currentText = "";
        answerText.text = "?";
    }

    public void QuizRand()
    {
        int a = Random.Range(0, 10);
        int b = Random.Range(0, 10);
        int c = Random.Range(0, 10);

        // 문제 텍스트를 생성
        if (quizText != null)  // quizText가 null인지 확인
        {
            quizText.text = $"{a} * {b} + {c} =";
        }
        else
        {
            Debug.LogError("quizText is not assigned in the Inspector!");
        }

        // 정답 계산 후 문자열로 저장
        int result = a * b + c;
        answer = result.ToString();
    }


    public void OnNumberButtonClick(string number)
    {
        if (answerText.text == "?" || currentText.Length == 0)
        {
            currentText = number;
            answerText.text = currentText;
        }
        else if (currentText.Length == 1)
        {
            currentText += number;
            answerText.text = currentText;
        }
    }

    public void OnFinishButtonClick()
    {
        if (currentText == answer)
        {
            StartCoroutine(CorrectAnswerCoroutine());
        }
        else
        {
            StartCoroutine(WrongAnswerCoroutine());
        }
    }

    private IEnumerator CorrectAnswerCoroutine()
    {
        answerText.text = "정답";
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("ParentsScene");
    }

    private IEnumerator WrongAnswerCoroutine()
    {
        answerText.text = "오답";
        yield return new WaitForSeconds(2.0f);
        answerText.text = "?";
    }

    public void ClearText()
    {
        if (currentText.Length > 0)
        {
            // 마지막 문자 하나를 삭제
            currentText = currentText.Substring(0, currentText.Length - 1);
            answerText.text = currentText; // 텍스트 업데이트
        }
    }
}
