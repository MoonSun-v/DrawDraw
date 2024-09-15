using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonMove : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // �巡�� �� ���콺/��ġ�� ���� ���� ��ġ ����
    private Rigidbody2D rb2D;
    private Camera mainCamera; // ī�޶� ������ ����

    // �巡�� ������ ������ square ������Ʈ�� Collider2D
    public GameObject squareObject;
    private Collider2D squareCollider;

    private Collider2D col2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // ī�޶� ����

        col2D = GetComponent<Collider2D>();

        if (col2D == null)
        {
            Debug.LogError("Collider2D is not attached to the game object.");
        }

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

        // ���콺 Ŭ�� �Ǵ� ��ġ �Է��� �ִ��� Ȯ��
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            //Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ

            // ���콺 Ŭ�� ��ġ���� Raycast�� �߻��Ͽ� Scene���� Ray�� �� �� �ְ� ��
            Vector3 rayOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            rayOrigin.z = 0f; // 2D������ z ���� 0���� ���� (z ���� ������� ����)

            // "shape" ���̾ �ش��ϴ� ���̾� ����ũ ����
            int layerMask = 1 << LayerMask.NameToLayer("shape");

            // Raycast�� Ư�� ���̾�� ����
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);


            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                // hitObject�� ���� ��ũ��Ʈ�� ����� gameObject�� ������ Ȯ��
                if (hitObject == gameObject)
                {
                    isDragging = true;
                    offset = transform.position - GetInputWorldPosition();
                }
                else
                {
                    //�θ� ������Ʈ�� ������
                    GameObject parentObject = hitObject.transform.parent?.gameObject;

                    if (parentObject == gameObject)
                    {
                        isDragging = true;
                        offset = transform.position - GetInputWorldPosition();
                    }
                }
            }
        }

        // �巡�� ���� �� ���� ��ġ ������Ʈ
        else if ((Input.GetMouseButton(0) || Input.touchCount > 0) && isDragging)
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 targetPosition = mouseOrTouchPosition + offset; // ��ǥ ��ġ ���

            //squareCollider�� ��� �������� �̵� �����ϵ��� ����
            Bounds bounds = squareCollider.bounds;
            targetPosition.x = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x); // x ��ǥ ����
            targetPosition.y = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y); // y ��ǥ ����

            rb2D.MovePosition(targetPosition); // Rigidbody2D�� ����Ͽ� ������ ��ġ�� �̵�

        }

        // �巡�� ����
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount == 0))
        {
            // �巡�� ���¸� �����ϰ� ���õ� ������Ʈ�� �ʱ�ȭ
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

}