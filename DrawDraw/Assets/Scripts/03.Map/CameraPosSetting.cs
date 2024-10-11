using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosSetting : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        // 저장된 카메라 위치로 이동
        GameData.instance.SetCameraPosition(mainCamera);
    }
}
