using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCircleMove : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset; // �巡�� �� ���콺/��ġ�� ���� ���� ��ġ ����
    private Rigidbody2D rb2D;
    private Camera mainCamera; // ī�޶� ������ ����

    // �巡�� ������ ������ square ������Ʈ
    public CircleCollider2D Collider; // �ﰢ�� �ݶ��̴�
    public GameObject squareObject;        // �������� �Ҵ�� �簢�� ������Ʈ
    private BoxCollider2D squareCollider;  // �簢�� ������Ʈ�� BoxCollider2D

    //// Y �������� ����� �� ����� offset�� size ��
    //private Vector2 positiveOffset = new Vector2(0f, (float)-0.3460994);
    //private Vector2 positiveSize = new Vector2(10, (float)9.707803);

    //// Y �������� ������ �� ����� offset�� size ��
    //private Vector2 negativeOffset = new Vector2(0f, (float)0.3096588);
    //private Vector2 negativeSize = new Vector2(10, (float)9.862097);

    private float lastTapTime; // ������ �Է� �ð�
    private const float doubleTapThreshold = 0.3f; // ���� Ŭ��/��ġ ���� �ð� ���� (��)

    public float angle;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // ī�޶� ����

        Collider = GetComponent<CircleCollider2D>();

        // �簢�� ������Ʈ�� BoxCollider2D ��������
        if (squareObject != null)
        {
            squareCollider = squareObject.GetComponent<BoxCollider2D>();

            if (squareCollider == null)
            {
                Debug.LogError("squareObject�� BoxCollider2D�� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("squareObject�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    void Update()
    {
        // ���콺 Ŭ�� �Ǵ� ��ġ �Է��� �ִ��� Ȯ��
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ

            Vector3 rayOrigin;
            // ���콺 Ŭ�� ��ġ���� Raycast�� �߻��Ͽ� Scene���� Ray�� �� �� �ְ� ��
            if (Input.touchCount > 0)
            {
                rayOrigin = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                rayOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            rayOrigin.z = 0f; // 2D������ z ���� 0���� ���� (z ���� ������� ����)

            // "shape" ���̾ �ش��ϴ� ���̾� ����ũ ����
            int layerMask = 1 << LayerMask.NameToLayer("shape");

            // Raycast�� Ư�� ���̾�� ����
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                //Debug.Log(hit.collider.gameObject);

                // �Է��� Ȯ���Ͽ� ���� Ŭ�� �Ǵ� ���� ��ġ ó��
                float currentTime = Time.time;
                if (currentTime - lastTapTime < doubleTapThreshold)
                {

                    if (angle < 0)
                    {
                        ToggleScale();
                    }
                    else
                    {
                        RotateDegrees(angle);
                    }

                }
                lastTapTime = currentTime;
                isDragging = true; // �巡�� ���·� ��ȯ
                offset = transform.position - mouseOrTouchPosition; // ���콺/��ġ�� ���� ���� ��ġ ���� ���
            }
        }

        // �巡�� ���� �� ���� ��ġ ������Ʈ
        else if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)) && isDragging)
        {

            Vector3 mouseOrTouchPosition = GetInputWorldPosition(); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 targetPosition = mouseOrTouchPosition + offset; // ��ǥ ��ġ ���

            // squareCollider�� ��� �������� �̵� �����ϵ��� ����
            Bounds bounds = squareCollider.bounds; // �簢�� �ݶ��̴��� ��� ��������

            // �ﰢ���� ������ ��������
            //Vector2[] vertices = Collider.points;

            // �ﰢ���� ������ ��ġ�� ����Ͽ� �̵� ���� ������ ����
            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    // �ﰢ�� �������� ���� ��ǥ�� ��ȯ
            //    Vector3 worldVertex = targetPosition + (Vector3)vertices[i];

            //    // X�� �̵� ����: �ﰢ�� �������� �簢�� ��踦 ���� �ʵ��� ����
            //    if (worldVertex.x < bounds.min.x)
            //    {
            //        targetPosition.x += bounds.min.x - worldVertex.x; // ���� ��踦 ���� �ʵ��� �̵�
            //    }
            //    else if (worldVertex.x > bounds.max.x)
            //    {
            //        targetPosition.x -= worldVertex.x - bounds.max.x; // ������ ��踦 ���� �ʵ��� �̵�
            //    }

            //    // Y�� �̵� ����: �ﰢ�� �������� �簢�� ��踦 ���� �ʵ��� ����
            //    if (worldVertex.y < bounds.min.y)
            //    {
            //        targetPosition.y += bounds.min.y - worldVertex.y; // �Ʒ��� ��踦 ���� �ʵ��� �̵�
            //    }
            //    else if (worldVertex.y > bounds.max.y)
            //    {
            //        targetPosition.y -= worldVertex.y - bounds.max.y; // ���� ��踦 ���� �ʵ��� �̵�
            //    }
            //}

            // Rigidbody2D�� ����Ͽ� ������ ��ġ�� �̵�
            rb2D.MovePosition(targetPosition);

        }

        // �巡�� ����
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
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

    private void ToggleScale()
    {
        // ���� ������ ���� ������
        Vector3 currentScale = transform.localScale;

        // ���� ��ġ�� ����
        Vector3 originalPosition = transform.position;

        // ������ ����
        if (currentScale.x > 0)
        {
            // y ������ ���� ������ ����
            //transform.localScale = new Vector3(currentScale.x, -Mathf.Abs(currentScale.y), currentScale.z);

            // x ������ ���� ������ ����
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);

        }
        else
        {
            // x ������ ���� ���� ����
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }

        // ��ġ ����: ������ ���� ���� ��ġ�� �ǵ���
        transform.position = originalPosition;
    }

    //void UpdateCollider()
    //{
    //    // ���� ������Ʈ�� Y ������ ���� Ȯ��
    //    if (transform.localScale.y > 0)
    //    {
    //        // Y �������� ����� ��, positiveOffset�� positiveSize�� ����
    //        squareCollider.GetComponent<BoxCollider2D>().offset = positiveOffset;
    //        squareCollider.GetComponent<BoxCollider2D>().size = positiveSize;
    //    }
    //    else
    //    {
    //        // Y �������� ������ ��, negativeOffset�� negativeSize�� ����
    //        squareCollider.GetComponent<BoxCollider2D>().offset = negativeOffset;
    //        squareCollider.GetComponent<BoxCollider2D>().size = negativeSize;
    //    }
    //}

    // Z������ 45�� �ݽð� ���� ȸ���ϴ� �Լ�
    void RotateDegrees(float angle)
    {
        transform.Rotate(0f, 0f, angle);
    }


}