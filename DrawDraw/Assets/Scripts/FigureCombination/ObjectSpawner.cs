using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject squareObject; // squareObject를 여기서 할당받기

    public void SpawnObject()
    {
        Vector2 spawnPos = new Vector2((float)2.8, (float)1.5);
        if (prefabToSpawn != null)
        {
            GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            // 생성된 오브젝트의 Rigidbody2D 컴포넌트를 가져와서 비활성화
            Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.bodyType = RigidbodyType2D.Kinematic;  // 물리가 적용되지 않도록 설정
                                                          
            }

            // Draggable 스크립트 가져오기
            Draggable draggable = newObject.GetComponent<Draggable>();
            ObjectOnCollision collision = newObject.GetComponent<ObjectOnCollision>();
            if (draggable != null)
            {
                draggable.squareObject = squareObject; // squareObject를 설정
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