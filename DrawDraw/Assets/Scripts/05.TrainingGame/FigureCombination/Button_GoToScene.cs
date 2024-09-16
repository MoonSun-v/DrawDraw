using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GoToScene : MonoBehaviour
{
    // ����
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
    // 1�ܰ�
    public void ChangeScene_Sun()
    {
        SceneManager.LoadScene("1Sun");
        Debug.Log("�� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_Pinwheel()
    {
        SceneManager.LoadScene("1Pinwheel");
        Debug.Log("�ٶ����� ���� ������ �����մϴ�.");
    }

    //2�ܰ�
    public void ChangeScene_Rocket()
    {
        SceneManager.LoadScene("2Rocket");
        Debug.Log("���� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_Ship()
    {
        SceneManager.LoadScene("2Ship");
        Debug.Log("�� ���� ������ �����մϴ�.");
    }

    //3�ܰ�
    public void ChangeScene_Person()
    {
        SceneManager.LoadScene("3Person");
        Debug.Log("��� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_TheTrain()
    {
        SceneManager.LoadScene("3TheTrain");
        Debug.Log("���� ���� ������ �����մϴ�.");
    }
}
