using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public static CloneManager Instance; // 싱글톤 패턴
    private List<GameObject> clones = new List<GameObject>(); // 생성된 클론을 관리하는 리스트

    void Awake()
    {
        // 싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 클론 등록
    public void RegisterClone(GameObject clone)
    {
        clones.Add(clone);
    }

    // 모든 클론 비활성화
    public void DeactivateAllClones()
    {
        foreach (GameObject clone in clones)
        {
            if (clone != null) // null 체크
            {
                clone.SetActive(false);
            }
        }
    }

    // 모든 클론 활성화
    public void ActivateAllClones()
    {
        foreach (GameObject clone in clones)
        {
            if (clone != null) // null 체크
            {
                clone.SetActive(true);
            }
        }

    }
}
