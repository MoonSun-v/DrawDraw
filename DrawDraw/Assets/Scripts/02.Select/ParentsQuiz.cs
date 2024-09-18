using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsQuiz : MonoBehaviour
{
    public GameObject QuizCanvas;

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
    }
}
