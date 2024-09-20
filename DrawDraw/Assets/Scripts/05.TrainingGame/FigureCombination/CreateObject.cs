using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject prefab; // ������ ������
    private GameObject spawnedObject; // ������ ������

    private bool isDragging = false; // �巡�� ���¸� ��Ÿ��
    private float startPosX; // ���콺�� ������ ������ X �Ÿ�
    private float startPosY; // ���콺�� ������ ������ Y �Ÿ�
    public BoxCollider2D targetCollider; // �������� ���� �ϴ� BoxCollider
    public BoxCollider2D DragCollider; // �������� �巡���� �� �ִ� ���� ������ BoxCollider

    private Collider2D prefabCollider; // ������ �������� Collider

    public int angle = 0;
    void Update()
    {
        // ���콺 ��ư�� ������ �ִ� ���� �������� �巡��
        if (isDragging && spawnedObject != null)
        {
            DragPrefab();
        }
    }

    // ��ư�� ������ �� �������� ���� (IPointerDownHandler �������̽� ���)
    public void OnPointerDown(PointerEventData eventData)
    {
        // ���콺 Ŭ�� ��ġ���� ������ ����
        Vector3 spawnPosition = Input.mousePosition;
        spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, 10f));

        // �������� Canvas ������ ���� (�ʿ� ��)
        spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);

        // ������ �������� Collider�� ����
        prefabCollider = spawnedObject.GetComponent<Collider2D>();

        // ������ ������Ʈ�� Rigidbody2D ������Ʈ�� �����ͼ� ��Ȱ��ȭ
        Rigidbody2D rb2D = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Kinematic;  // ������ ������� �ʵ��� ����                                        
        }

        // ���콺�� ������ ������ �ʱ� ��ġ ���̸� ����
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosX = mousePos.x - spawnedObject.transform.position.x;
        startPosY = mousePos.y - spawnedObject.transform.position.y;

        // �巡�� ����
        isDragging = true;
    }

    // ���콺�� ���� �巡�׸� ���߰�, �������� BoxCollider �ȿ� ������ ���� (IPointerUpHandler �������̽� ���)
    public void OnPointerUp(PointerEventData eventData)
    {
        // �巡�� ���� ����
        isDragging = false;

        if (spawnedObject != null)
        {
            // �������� ��ġ�� BoxCollider �ȿ� �ִ��� Ȯ��
            if (!IsPrefabInsideTarget())
            {
                // BoxCollider �ȿ� ���� ������ ������Ʈ ����
                Destroy(spawnedObject);
            }

            else
            {
                Draggable draggable = spawnedObject.AddComponent<Draggable>();
                spawnedObject.AddComponent<ObjectOnCollision>();

                if (draggable != null)
                {
                    draggable.DragCollider = targetCollider; // squareObject�� ����
                    draggable.angle = angle;
                }
                else
                {
                    Debug.LogWarning("Draggable component not found on the instantiated object.");
                }
            }
        }
    }

    // �������� ���콺 ��ġ�� �巡��
    private void DragPrefab()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        // ������ ��ġ�� ���콺�� ���� �����ϱ� ���� ���ѵ� ���� �������� �巡�׵ǵ��� ó��
        Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, spawnedObject.transform.position.z);

        // ���ѵ� ���� �������� �巡�׵ǵ��� ��ġ�� Clamp
        newPosition = ClampPositionToTarget(newPosition);

        // �������� ��ġ�� ���ѵ� ���� �������� ������Ʈ
        spawnedObject.transform.position = newPosition;
    }

    // �������� targetCollider �ȿ� �ִ��� Ȯ���ϴ� �Լ�
    private bool IsPrefabInsideTarget()
    {
        // �������� ���� ��ġ
        Vector3 prefabPosition = spawnedObject.transform.position;

        // targetCollider�� ��踦 �������� ��ǥ�� Ȯ��
        if (targetCollider != null)
        {
            Bounds bounds = targetCollider.bounds;

            // �������� ��ġ�� BoxCollider�� ��� �ȿ� �ִ��� Ȯ��
            if (prefabPosition.x > bounds.min.x && prefabPosition.x < bounds.max.x &&
                prefabPosition.y > bounds.min.y && prefabPosition.y < bounds.max.y)
            {
                return true; // �������� Collider �ȿ� ����
            }
        }

        return false; // �������� Collider �ȿ� ����
    }

    // �������� ��ġ�� DragCollider ���� ���� �����ϴ� �Լ�
    private Vector3 ClampPositionToTarget(Vector3 position)
    {
        if (DragCollider != null && prefabCollider != null)
        {
            Bounds targetBounds = DragCollider.bounds;
            Bounds prefabBounds = prefabCollider.bounds;

            // X, Y ��ǥ�� DragCollider ��� ���� ����
            // �������� �ݶ��̴� ũ�⸦ ����Ͽ� ����� �ʵ��� ����
            float clampedX = Mathf.Clamp(position.x, targetBounds.min.x + prefabBounds.extents.x, targetBounds.max.x - prefabBounds.extents.x);
            float clampedY = Mathf.Clamp(position.y, targetBounds.min.y + prefabBounds.extents.y, targetBounds.max.y - prefabBounds.extents.y);

            return new Vector3(clampedX, clampedY, position.z);
        }

        return position; // DragCollider ������ �������� ����
    }
}
