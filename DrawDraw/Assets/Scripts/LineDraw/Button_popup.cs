using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject popup;
    public void OnClick_close() // 'â �ݱ�' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        popup.transform.gameObject.SetActive(false);
    }

    public void OnClick_next() // '���� ��������' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        //SceneManager.LoadScene("NextScene"); 
        Debug.Log("���� �������� �Ѿ�ϴ�.");
    }
}

