using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject squareObject; // squareObject를 여기서 할당받기
    private int objectCount = 0; // 생성된 오브젝트 수를 추적하는 변수

    // 오브젝트를 생성하는 메서드
    public void SpawnObject()
    {
        Vector2 spawnPos = new Vector2((float)2.8, (float)1.5); // 생성위치

        if (prefabToSpawn != null&&objectCount<15)
        {
            // 프리팹을 인스턴스화하여 새로운 오브젝트 생성
            GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            // 생성된 오브젝트의 이름을 고유하게 설정
            objectCount++; // 오브젝트 수 증가
            newObject.name = prefabToSpawn.name + "_Clone" + objectCount;

            // 생성된 오브젝트의 Rigidbody2D 컴포넌트를 가져와서 비활성화
            Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.bodyType = RigidbodyType2D.Kinematic;  // 물리가 적용되지 않도록 설정                                        
            }

            // Draggable 스크립트 가져오기
            //Draggable draggable = newObject.GetComponent<Draggable>();
            //ObjectOnCollision collision = newObject.GetComponent<ObjectOnCollision>();
            // 각 오브젝트가 독립적으로 드래그할 수 있도록 개별 인스턴스로 설정
            Draggable draggable = newObject.AddComponent<Draggable>();  // 필요한 경우 추가적인 Draggable 스크립트 인스턴스 생성
            newObject.AddComponent<ObjectOnCollision>();
            
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