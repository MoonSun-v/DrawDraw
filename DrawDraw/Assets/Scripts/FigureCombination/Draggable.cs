using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D rb2D;

    // square ������Ʈ�� Collider2D�� ����
    public GameObject squareObject;
    private Collider2D squareCollider;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        if (squareObject != null)
        {
            squareCollider = squareObject.GetComponent<Collider2D>();

            if (squareCollider == null)
            {
                Debug.LogError("Square object does not have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogError("Square object is not assigned.");
        }
    }

    void OnMouseDown()
    {
        // �巡�� ����
        isDragging = true;

        // ������Ʈ�� ���콺 ������ ���� ���̸� ���
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        // �巡�� ����
        isDragging = false;
    }

    void Update()
    {
        // �巡�� ���� ���� ������Ʈ ��ġ ������Ʈ
        if (isDragging && squareCollider != null)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + offset;

            // squareCollider�� ��� �������� �̵� �����ϵ��� ����
            Bounds bounds = squareCollider.bounds;

            // x ��ǥ ����
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);

            // y ��ǥ ����
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

            rb2D.MovePosition(targetPosition);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // ���콺 ��ġ�� ȭ�� ��ǥ���� ���� ��ǥ�� ��ȯ
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.transform.position.z * -1; // 2D������ z ���� ������� ����

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}