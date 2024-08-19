using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;

    public void SpawnObject()
    {
        GameObject newObject = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);

        // ������ ������Ʈ�� Rigidbody2D ������Ʈ�� �����ͼ� ��Ȱ��ȭ
        Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.isKinematic = true; // ���� ������ ���� �ʵ��� ����
        }
    }
}