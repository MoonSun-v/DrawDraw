using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject squareObject; // squareObject�� ���⼭ �Ҵ�ޱ�
    private int objectCount = 0; // ������ ������Ʈ ���� �����ϴ� ����

    // ������Ʈ�� �����ϴ� �޼���
    public void SpawnObject_triangle()
    {
        Vector2 spawnPos = new Vector2((float)2.8, (float)1.5); // ������ġ

        if (prefabToSpawn != null && objectCount < 15)
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
                //draggable.squareObject = squareObject; // squareObject�� ����
                draggable.angle = 45;
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

    //public void SpawnObject_Parallelogram()
    //{
    //    Vector2 spawnPos = new Vector2((float)1.85, (float)1.7); // ������ġ

    //    if (prefabToSpawn != null && objectCount < 15)
    //    {
    //        // �������� �ν��Ͻ�ȭ�Ͽ� ���ο� ������Ʈ ����
    //        GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

    //        // ������ ������Ʈ�� �̸��� �����ϰ� ����
    //        objectCount++; // ������Ʈ �� ����
    //        newObject.name = prefabToSpawn.name + "_Clone" + objectCount;

    //        // ������ ������Ʈ�� Rigidbody2D ������Ʈ�� �����ͼ� ��Ȱ��ȭ
    //        Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
    //        if (rb2D != null)
    //        {
    //            rb2D.bodyType = RigidbodyType2D.Kinematic;  // ������ ������� �ʵ��� ����                                        
    //        }

    //        // Draggable ��ũ��Ʈ ��������
    //        // �� ������Ʈ�� ���������� �巡���� �� �ֵ��� ���� �ν��Ͻ��� ����
    //        //Draggable draggable = newObject.AddComponent<Draggable>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����

    //        ParallelogramMovement parallelogramMovement = newObject.AddComponent<ParallelogramMovement>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����
    //        //newObject.AddComponent<ObjectOnCollision>();

    //        if (parallelogramMovement != null)
    //        {
    //            parallelogramMovement.squareObject = squareObject; // squareObject�� ����
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Draggable component not found on the instantiated object.");
    //        }

    //    }
    //    else
    //    {
    //        Debug.LogError("Prefab to spawn is not assigned.");
    //    }
    //}

    //public void SpawnObject_Hexagon()
    //{
    //    Vector3 spawnPos = new Vector3((float)1.021548, (float)1.377955); // ������ġ

    //    if (prefabToSpawn != null && objectCount < 5)
    //    {
    //        // �������� �ν��Ͻ�ȭ�Ͽ� ���ο� ������Ʈ ����
    //        GameObject newObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

    //        // ������ ������Ʈ�� �̸��� �����ϰ� ����
    //        objectCount++; // ������Ʈ �� ����
    //        newObject.name = prefabToSpawn.name + "_Clone" + objectCount;

    //        // ������ ������Ʈ�� Rigidbody2D ������Ʈ�� �����ͼ� ��Ȱ��ȭ
    //        Rigidbody2D rb2D = newObject.GetComponent<Rigidbody2D>();
    //        if (rb2D != null)
    //        {
    //            rb2D.bodyType = RigidbodyType2D.Kinematic;  // ������ ������� �ʵ��� ����                                        
    //        }

    //        // Draggable ��ũ��Ʈ ��������
    //        //Draggable draggable = newObject.GetComponent<Draggable>();
    //        //ObjectOnCollision collision = newObject.GetComponent<ObjectOnCollision>();
    //        // �� ������Ʈ�� ���������� �巡���� �� �ֵ��� ���� �ν��Ͻ��� ����
    //        HexagonMove hexagonMove = newObject.AddComponent<HexagonMove>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����;

    //        if (hexagonMove != null)
    //        {
    //            hexagonMove.squareObject = squareObject; // squareObject�� ����
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Draggable component not found on the instantiated object.");
    //        }
    //    }
    //}

    public void SpawnObject_circle()
    {
        Vector2 spawnPos = new Vector2((float)-1.28, (float)-0.82); // ������ġ

        if (prefabToSpawn != null && objectCount < 1)
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
            //CircleMove CircleMove = newObject.AddComponent<CircleMove>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����
            //newObject.AddComponent<ObjectOnCollision>();

            //if (CircleMove != null)
            //{
            //    CircleMove.squareObject = squareObject; // squareObject�� ����
            //}
            //else
            //{
            //    Debug.LogWarning("Draggable component not found on the instantiated object.");
            //}

        }
        else
        {
            Debug.LogError("Prefab to spawn is not assigned.");
        }
    }

    public void SpawnObject_RightTriangle()
    {
        Vector2 spawnPos = new Vector2((float)-4.5, (float)-2.5); // ������ġ

        if (prefabToSpawn != null && objectCount < 10)
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
                //draggable.squareObject = squareObject; // squareObject�� ����
                draggable.angle = 90;
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

    public void SpawnObject_RightTriangle2()
    {
        Vector2 spawnPos = new Vector2((float)-5.0, (float)-2.5); // ������ġ

        if (prefabToSpawn != null && objectCount < 15)
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
                //draggable.squareObject = squareObject; // squareObject�� ����
                draggable.angle = -1;
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
    public void SpawnObject_circle2()
    {
        Vector2 spawnPos = new Vector2((float)3.0, (float)2.0); // ������ġ

        if (prefabToSpawn != null && objectCount < 5)
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
            CircleMove CircleMove = newObject.AddComponent<CircleMove>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����
            newObject.AddComponent<ObjectOnCollision>();

            if (CircleMove != null)
            {
                CircleMove.squareObject = squareObject; // squareObject�� ����
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

    public void SpawnObject_Halfcircle()
    {
        Vector2 spawnPos = new Vector2((float)3.0, (float)2.0); // ������ġ

        if (prefabToSpawn != null && objectCount < 5)
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
            HalfCircleMove CircleMove = newObject.AddComponent<HalfCircleMove>();  // �ʿ��� ��� �߰����� Draggable ��ũ��Ʈ �ν��Ͻ� ����
            newObject.AddComponent<ObjectOnCollision>();

            if (CircleMove != null)
            {
                CircleMove.squareObject = squareObject; // squareObject�� ����
                CircleMove.angle = 90;
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