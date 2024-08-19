using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject squareObject; // squareObject를 여기서 할당받기

    public void SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            GameObject newObject = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);

            // 생성된 오브젝트의 Rigidbody2D 컴포넌트를 가져와서 비활성화
            Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.isKinematic = true; // 물리 영향을 받지 않도록 설정
            }

            // Draggable 스크립트 가져오기
            Draggable draggable = newObject.GetComponent<Draggable>();
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