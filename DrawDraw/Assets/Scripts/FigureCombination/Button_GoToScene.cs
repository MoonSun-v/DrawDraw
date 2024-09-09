using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GoToScene : MonoBehaviour
{
    public void ChangeScene_waterDrop()
    {
        SceneManager.LoadScene("WaterDropScene");
        Debug.Log("����� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_snail()
    {
        SceneManager.LoadScene("SnailScene");
        Debug.Log("������ ���� ������ �����մϴ�.");
    }

    public void ChangeScene_map()
    {
        SceneManager.LoadScene("MapScene");
        Debug.Log("�� ȭ������ �̵��մϴ�.");
    }

    public void OnRestartButtonClick()
    {
        // ���� Ȱ��ȭ�� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("ó������ �ٽ� �����մϴ�.");
    }
}
