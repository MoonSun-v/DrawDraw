using System.Collections.Generic;
using UnityEngine;

public class OSCManager : MonoBehaviour
{
    public OSC _isOSC;

    public SensorDataFormat[] SensorData;

    private GameManager gameManager;
    // Start is called before the first frame update

    private void Start()
    {
        SetOSC_Event();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SensorData =  new SensorDataFormat[System.Enum.GetValues(typeof(SensorEnum)).Length];
        for (int i = 0; i < SensorData.Length; i++)
            SensorData[i] = new SensorDataFormat();
    }

    #region FrontSensor
    public void getFrontStartMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Front)].RectSize = new Vector2(message.GetFloat(0), message.GetFloat(1));
        SensorData[((int)SensorEnum.Front)].Position.Clear();

        if (!gameManager.SensorState[((int)SensorEnum.Front)])
        {
            gameManager.SensorState[((int)SensorEnum.Front)] = true;
            Debug.Log("정면 센서 연결");
        }
    }

    public void getFrontSensorMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Front)].Position.Add(new Vector3(message.GetFloat(0), message.GetFloat(1), 0));
    }

    public void getFrontStopMessage(OscMessage message)
    {
    }

    public void FrontSensorQuit(OscMessage message)
    {
        gameManager.SensorState[((int)SensorEnum.Front)] = false;
        Debug.Log("정면 센서 종료");
    }
    #endregion

    #region BackSensor
    public void getBackStartMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Back)].RectSize = new Vector2(message.GetFloat(0), message.GetFloat(1));
        SensorData[((int)SensorEnum.Back)].Position.Clear();

        if (!gameManager.SensorState[((int)SensorEnum.Back)])
        {
            gameManager.SensorState[((int)SensorEnum.Back)] = true;
            Debug.Log("후면 센서 연결");
        }
    }

    public void getBackSensorMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Back)].Position.Add(new Vector3(message.GetFloat(0), message.GetFloat(1), 0));
        //Debug.Log(SensorData.Position.Count);
    }

    public void getBackStopMessage(OscMessage message)
    {
        //Debug.Log(message.GetInt(0));
    }

    public void BackSensorQuit(OscMessage message)
    {
        gameManager.SensorState[((int)SensorEnum.Back)] = false;
        Debug.Log("후면 센서 종료");
    }
    #endregion

    #region RightSensor
    public void getRightStartMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Right)].RectSize = new Vector2(message.GetFloat(0), message.GetFloat(1));
        SensorData[((int)SensorEnum.Right)].Position.Clear();

        if (!gameManager.SensorState[((int)SensorEnum.Right)])
        {
            gameManager.SensorState[((int)SensorEnum.Right)] = true;
            Debug.Log("우면 센서 연결");
        }
    }

    public void getRightSensorMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Right)].Position.Add(new Vector3(message.GetFloat(0), message.GetFloat(1), 0));
        //Debug.Log(SensorData.Position.Count);
    }

    public void getRightStopMessage(OscMessage message)
    {
        //Debug.Log(message.GetInt(0));
    }

    public void RightSensorQuit(OscMessage message)
    {
        gameManager.SensorState[((int)SensorEnum.Right)] = false;
        Debug.Log("우면 센서 종료");
    }
    #endregion

    #region LeftSensor
    public void getLeftStartMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Left)].RectSize = new Vector2(message.GetFloat(0), message.GetFloat(1));
        SensorData[((int)SensorEnum.Left)].Position.Clear();

        if (!gameManager.SensorState[((int)SensorEnum.Left)])
        {
            gameManager.SensorState[((int)SensorEnum.Left)] = true;
            Debug.Log("좌면 센서 연결");
        }
    }

    public void getLeftSensorMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Left)].Position.Add(new Vector3(message.GetFloat(0), message.GetFloat(1), 0));
        //Debug.Log(SensorData.Position.Count);
    }

    public void getLeftStopMessage(OscMessage message)
    {
        //Debug.Log(message.GetInt(0));
    }

    public void LeftSensorQuit(OscMessage message)
    {
        gameManager.SensorState[((int)SensorEnum.Left)] = false;
        Debug.Log("좌면 센서 종료");
    }
    #endregion

    #region DownSensor
    public void getDownStartMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Down)].RectSize = new Vector2(message.GetFloat(0), message.GetFloat(1));
        SensorData[((int)SensorEnum.Down)].Position.Clear();

        if (!gameManager.SensorState[((int)SensorEnum.Down)])
        {
            gameManager.SensorState[((int)SensorEnum.Down)] = true;
            Debug.Log("바닥 센서 연결");
        }
    }

    public void getDownSensorMessage(OscMessage message)
    {
        SensorData[((int)SensorEnum.Down)].Position.Add(new Vector3(message.GetFloat(0), message.GetFloat(1), 0));
    }

    public void getDownStopMessage(OscMessage message)
    {
    }

    public void DownSensorQuit(OscMessage message)
    {
        gameManager.SensorState[((int)SensorEnum.Down)] = false;
        Debug.Log("바닥 센서 종료");
    }
    #endregion

    void SetOSC_Event()
    {
        _isOSC.SetAddressHandler("/Front/Start", getFrontStartMessage);
        _isOSC.SetAddressHandler("/Front/Data", getFrontSensorMessage);
        _isOSC.SetAddressHandler("/Front/End", getFrontStopMessage);
        _isOSC.SetAddressHandler("/Front/Quit", FrontSensorQuit);
        _isOSC.SetAddressHandler("/Back/Start", getBackStartMessage);
        _isOSC.SetAddressHandler("/Back/Data", getBackSensorMessage);
        _isOSC.SetAddressHandler("/Back/End", getBackStopMessage);
        _isOSC.SetAddressHandler("/Back/Quit", BackSensorQuit);
        _isOSC.SetAddressHandler("/Left/Start", getLeftStartMessage);
        _isOSC.SetAddressHandler("/Left/Data", getLeftSensorMessage);
        _isOSC.SetAddressHandler("/Left/End", getLeftStopMessage);
        _isOSC.SetAddressHandler("/Left/Quit", LeftSensorQuit);
        _isOSC.SetAddressHandler("/Right/Start", getRightStartMessage);
        _isOSC.SetAddressHandler("/Right/Data", getRightSensorMessage);
        _isOSC.SetAddressHandler("/Right/End", getRightStopMessage);
        _isOSC.SetAddressHandler("/Right/Quit", RightSensorQuit);
        _isOSC.SetAddressHandler("/Down/Start", getDownStartMessage);
        _isOSC.SetAddressHandler("/Down/Data", getDownSensorMessage);
        _isOSC.SetAddressHandler("/Down/End", getDownStopMessage);
        _isOSC.SetAddressHandler("/Down/Quit", DownSensorQuit);
    }
}
