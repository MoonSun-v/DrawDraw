using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // �巡�� �� ���콺/��ġ�� ���� ���� ��ġ ����
    private Rigidbody2D rb2D;
    private Camera mainCamera; // ī�޶� ������ ����

    // �巡�� ������ ������ square ������Ʈ�� Collider2D
    public GameObject squareObject;
    private Collider2D squareCollider;

    private float lastTapTime; // ������ �Է� �ð�
    private const float doubleTapThreshold = 0.3f; // ���� Ŭ��/��ġ ���� �ð� ���� (��)

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // ī�޶� ����

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

    void Update()
    {
        // �Է��� Ȯ���Ͽ� ���� Ŭ�� �Ǵ� ���� ��ġ ó��
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < doubleTapThreshold)
            {
                // ���� Ŭ��/��ġ�� ����
                ToggleScale();
            }
            lastTapTime = currentTime;
        }

        // ���콺 Ŭ�� �Ǵ� ��ġ �Է��� �ִ��� Ȯ��
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ

            if (squareCollider != null && IsPointerOverCollider(mouseOrTouchPosition))
            {
                isDragging = true; // �巡�� ���·� ��ȯ
                offset = transform.position - mouseOrTouchPosition; // ���콺/��ġ�� ���� ���� ��ġ ���� ���
            }
        }

        // �巡�� ���� �� ���� ��ġ ������Ʈ
        else if ((Input.GetMouseButton(0) || Input.touchCount > 0) && isDragging)
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 targetPosition = mouseOrTouchPosition + offset; // ��ǥ ��ġ ���

            // squareCollider�� ��� �������� �̵� �����ϵ��� ����
            Bounds bounds = squareCollider.bounds;
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x); // x ��ǥ ����
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y); // y ��ǥ ����
            
            rb2D.MovePosition(targetPosition); // Rigidbody2D�� ����Ͽ� ������ ��ġ�� �̵�

        }

        // �巡�� ����
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount == 0 && isDragging))
        {
            isDragging = false; // �巡�� ���¸� ����
        }
    }

    private Vector3 GetInputWorldPosition()
    {
        Vector3 inputPosition;

        if (Input.touchCount > 0)
        {
            // ��ġ �Է��� ���� ��� ��ġ ��ġ�� ������
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            // ���콺 �Է��� ���� ��� ���콺 ��ġ�� ������
            inputPosition = Input.mousePosition;
        }

        // z ���� ī�޶���� �Ÿ� ���� (2D������ z ���� ������� ����)
        inputPosition.z = -mainCamera.transform.position.z;

        return mainCamera.ScreenToWorldPoint(inputPosition); // ȭ�� ��ǥ�� ���� ��ǥ�� ��ȯ
    }

    private bool IsPointerOverCollider(Vector3 position)
    {
        // �Է� ��ġ�� squareCollider�� ��� ���� �ִ��� Ȯ��
        return squareCollider.OverlapPoint(position);
    }


    private void ToggleScale()
    {
        // ���� ������ ���� ������
        Vector3 currentScale = transform.localScale;

        if (currentScale.y > 0)
        {
            // y ������ ���� ������ ����
            transform.localScale = new Vector3(currentScale.x, -Mathf.Abs(currentScale.y), currentScale.z);
        }
        else
        {
            // y ������ ���� ���� ����
            transform.localScale = new Vector3(currentScale.x, Mathf.Abs(currentScale.y), currentScale.z);
        }
    }
}