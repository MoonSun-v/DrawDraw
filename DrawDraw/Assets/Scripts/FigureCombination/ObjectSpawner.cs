using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject squareObject; // squareObject�� ���⼭ �Ҵ�ޱ�
    private int objectCount = 0; // ������ ������Ʈ ���� �����ϴ� ����

    // ������Ʈ�� �����ϴ� �޼���
    public void SpawnObject()
    {
        Vector2 spawnPos = new Vector2((float)2.8, (float)1.5); // ������ġ

        if (prefabToSpawn != null&&objectCount<15)
        {
            // �������� �ν��Ͻ�ȭ�Ͽ� ���ο� ������Ʈ ����
            GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            // ������ ������Ʈ�� �̸��� �����ϰ� ����
            objectCount++; // ������Ʈ �� ����
            newObject.name = prefabToSpawn.name + "_Clone" + objectCount;

            // ������ ������Ʈ�� Rigidbody2D ������Ʈ�� �����ͼ� ��Ȱ��ȭ
            Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.bodyType = RigidbodyType2D.Kinematic;  // ������ ������� �ʵ��� ����                                        
            }

            // Draggable ��ũ��Ʈ ��������
            //Draggable draggable = newObject.GetComponent<Draggable>();
            //ObjectOnCollision collision = newObject.GetComponent<ObjectOnCollision>();
            // �� ������Ʈ�� ���������� �巡���� �� �ֵ��� ���� �ν��Ͻ��� ����
            Draggable draggable = newObject.AddComponent<Draggable>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����
            newObject.AddComponent<ObjectOnCollision>();
            
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