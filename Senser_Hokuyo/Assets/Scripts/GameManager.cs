using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[] SensorState;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        SensorState = new bool[System.Enum.GetValues(typeof(SensorEnum)).Length];
        for(int i = 0; i < SensorState.Length; i++)
            SensorState[i] = false;
    }
}
