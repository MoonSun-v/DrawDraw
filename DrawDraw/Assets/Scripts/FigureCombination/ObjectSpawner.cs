using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;

    public void SpawnObject()
    {
        GameObject newObject = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);

        // 생성된 오브젝트의 Rigidbody2D 컴포넌트를 가져와서 비활성화
        Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.isKinematic = true; // 물리 영향을 받지 않도록 설정
        }
    }
}