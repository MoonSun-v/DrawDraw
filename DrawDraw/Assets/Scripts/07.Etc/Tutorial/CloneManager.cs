using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public static CloneManager Instance; // �̱��� ����
    private List<GameObject> clones = new List<GameObject>(); // ������ Ŭ���� �����ϴ� ����Ʈ

    void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Ŭ�� ���
    public void RegisterClone(GameObject clone)
    {
        clones.Add(clone);
    }

    // ��� Ŭ�� ��Ȱ��ȭ
    public void DeactivateAllClones()
    {
        foreach (GameObject clone in clones)
        {
            if (clone != null) // null üũ
            {
                clone.SetActive(false);
            }
        }
    }

    // ��� Ŭ�� Ȱ��ȭ
    public void ActivateAllClones()
    {
        foreach (GameObject clone in clones)
        {
            if (clone != null) // null üũ
            {
                clone.SetActive(true);
            }
        }

    }
}
