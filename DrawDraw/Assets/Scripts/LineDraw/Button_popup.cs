using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject current_popup; 
    public resultPopupManager result_popup; // PopupManager ��ũ��Ʈ�� ������ ����

    //int Score; // �� �׸��� ���ӿ����� ���� ���� 

    public void OnClick_close() // 'â �ݱ�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        current_popup.transform.gameObject.SetActive(false);
    }

   
}

