using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCameraPos : MonoBehaviour
{
    public Camera mainCamera;

    public void SavePosition()
    {
        // ī�޶��� ���� ��ġ ����
        GameData.instance.SaveCameraPosition(mainCamera.transform.position);
    }
}
