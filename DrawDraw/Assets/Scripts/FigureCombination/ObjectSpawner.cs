using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject squareObject; // squareObject�� ���⼭ �Ҵ�ޱ�

    public void SpawnObject()
    {
        Vector2 spawnPos = new Vector2((float)2.8, (float)1.5);
        if (prefabToSpawn != null)
        {
            GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            // ������ ������Ʈ�� Rigidbody2D ������Ʈ�� �����ͼ� ��Ȱ��ȭ
            Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.bodyType = RigidbodyType2D.Kinematic;  // ������ ������� �ʵ��� ����
                                                          
            }

            // Draggable ��ũ��Ʈ ��������
            Draggable draggable = newObject.GetComponent<Draggable>();
            ObjectOnCollision collision = newObject.GetComponent<ObjectOnCollision>();
            if (draggable != null)
            {
                draggable.squareObject = squareObject; // squareObject�� ����
            }
            else
            {
                Debug.LogWarning("Draggable component not found on the instantiated object.");
            }
        }
        else
        {
            Debug.LogError("Prefab to spawn is not assigned.");
        }
    }
}