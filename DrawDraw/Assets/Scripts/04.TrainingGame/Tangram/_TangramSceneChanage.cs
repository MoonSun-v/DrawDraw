using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _TangramSceneChanage : MonoBehaviour
{
    // ��ư Ŭ�� �� ȣ���� �Լ�
    public void BoatTutorialScene()
    {
        // SceneManager�� ����� �� �̵�
        SceneManager.LoadScene("T_TangramScene_Lv1_boat");
    }

    public void duckTutorialScene()
    {
        // SceneManager�� ����� �� �̵�
        SceneManager.LoadScene("T_TangramScene_Lv1_duck");
    }
}
