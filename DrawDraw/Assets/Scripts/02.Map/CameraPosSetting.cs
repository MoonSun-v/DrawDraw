using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosSetting : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        // ����� ī�޶� ��ġ�� �̵�
        GameData.instance.SetCameraPosition(mainCamera);
    }
}
