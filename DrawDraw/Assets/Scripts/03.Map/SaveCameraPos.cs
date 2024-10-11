using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCameraPos : MonoBehaviour
{
    public Camera mainCamera;

    public void SavePosition()
    {
        // 카메라의 현재 위치 저장
        GameData.instance.SaveCameraPosition(mainCamera.transform.position);
    }
}
