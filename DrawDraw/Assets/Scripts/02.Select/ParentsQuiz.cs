using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParentsQuiz : MonoBehaviour
{
    public GameObject QuizCanvas;

    public Text answerText;
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
    }

    public void cancelParentsQuiz()
    {
        QuizCanvas.SetActive(false);
        ClearText();
    }

    public void OnNumberButtonClick(string number)
    {
        ClearText();
        if (currentText != answer)
        {
            currentText += number;
            answerText.text = currentText;
        }
        else
        {
            SceneManager.LoadScene("ParentsScene");
        }
    }

    public void ClearText()
    {
        currentText = "";
        answerText.text = currentText;
    }
}
