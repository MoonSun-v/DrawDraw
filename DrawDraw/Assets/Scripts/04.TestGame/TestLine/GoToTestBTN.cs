using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTestBTN : MonoBehaviour
{
    public void GoToTest1Line() { SceneManager.LoadScene("Test_1Line"); }
    public void GoToTest2Line() { SceneManager.LoadScene("Test_2Line"); }
    public void GoToTest3Line() { SceneManager.LoadScene("Test_3Line"); }
    public void GoToTest4Line() { SceneManager.LoadScene("Test_4Line"); }
    public void GoToTest5Line() { SceneManager.LoadScene("Test_5Line"); }
    public void GoToTest6Line() { SceneManager.LoadScene("Test_6Line"); }
    public void GoToTest_ColoringScene() { SceneManager.LoadScene("Test_ColoringScene"); }

    public void OnRestartButtonClick()
    {
        // ���� Ȱ��ȭ�� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("ó������ �ٽ� �����մϴ�.");
    }
}
