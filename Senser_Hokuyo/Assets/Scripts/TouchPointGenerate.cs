using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchPointGenerate : MonoBehaviour
{
    public SensorEnum sensorEnum;
    private GameManager gameManager;
    private List<GameObject> TouchPoints = new List<GameObject>();

    [SerializeField] private SensorManager sensorManager;
    [SerializeField] private GameObject TouchPoint;

    [SerializeField] private Transform TouchPointBasket;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.SensorState[((int)sensorEnum)])
        {
            if(TouchPoint != null)
            {
                if(sensorManager.getSensorVector().Count > TouchPoints.Count)
                {
                    for (int i = TouchPoints.Count; i < sensorManager.getSensorVector().Count; i++)
                    {
                        TouchPoints.Add(Instantiate(TouchPoint, TouchPointBasket));
                    }
                }
                else if(sensorManager.getSensorVector().Count < TouchPoints.Count)
                {
                    for(int i = sensorManager.getSensorVector().Count; i < TouchPoints.Count; i++)
                    {
                        TouchPoints[i].SetActive(false);
                    }
                }
                for(int i = 0; i < sensorManager.getSensorVector().Count; i++)
                {
                    TouchPoints[i].SetActive(true);
                    TouchPoints[i].transform.localPosition = sensorManager.getSensorVector()[i];
                }
            }
        }
        else
        {
            for(int i = 0; i < TouchPoints.Count; i++)
            {
                TouchPoints[i].SetActive(false);
            }
        }
    }
}