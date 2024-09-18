using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsManager : MonoBehaviour
{
    public GameObject StatisticsCanvas;
    public GameObject SoundCanvas;
    public GameObject ExplainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Statistics()
    {
        StatisticsCanvas.SetActive(true);
        SoundCanvas.SetActive(false);
        ExplainCanvas.SetActive(false);
    }

    public void Sound()
    {
        StatisticsCanvas.SetActive(false);
        SoundCanvas.SetActive(true);
        ExplainCanvas.SetActive(false);
    }

    public void Explain()
    {
        StatisticsCanvas.SetActive(false);
        SoundCanvas.SetActive(false);
        ExplainCanvas.SetActive(true);
    }
}
