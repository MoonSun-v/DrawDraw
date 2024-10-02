using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestBackToSelect : MonoBehaviour
{
    public GameObject CheckPopup;

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }

    // �˾� : �ƴ�
    public void NoBtn()
    {
        CheckPopup.SetActive(false);
    }

    // �˾� : ��
    public void YesBtn()
    {
        CheckPopup.SetActive(false);
        SceneManager.LoadScene("SelectScene");
    }
}
