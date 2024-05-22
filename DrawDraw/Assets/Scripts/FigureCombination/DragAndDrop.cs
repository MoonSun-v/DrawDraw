using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop: MonoBehaviour
{
    private bool isDragging = false; // ���� �巡�� ������ ���θ� �����ϴ� ����
    private GameObject currentObject; // ���� �巡�� ���� ������Ʈ�� �����ϴ� ����
    public GameObject[] objectPrefabs; // ������ ������Ʈ�� ������ �迭
    private GameObject selectedPrefab; // ���õ� ������

    void Start()
    {
        if (objectPrefabs.Length > 0)
        {
            selectedPrefab = objectPrefabs[0]; // �⺻���� ù ��° �������� ����
        }
    }

    void Update()
    {
        // ���콺 �Է� ó��
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) // ���콺 ���� ��ư�� ������ UI ������Ʈ ���� �ƴ� ��
        {
            HandleInput(Input.mousePosition); // �Է� ó�� �޼��� ȣ��
        }

        if (Input.GetMouseButton(0) && isDragging) // ���콺 ���� ��ư�� ������ �巡�� ���� ��
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            mousePosition.z = 0; // z�� ���� 0���� ����
            currentObject.transform.position = mousePosition; // ���� �巡�� ���� ������Ʈ ��ġ�� ���콺 ��ġ�� ����
        }

        if (Input.GetMouseButtonUp(0) && isDragging) // ���콺 ���� ��ư�� ������ ��
        {
            isDragging = false; // �巡�� ���� ����
            currentObject = null; // ���� �巡�� ���� ������Ʈ ����
        }

        // ��ġ �Է� ó��
        if (Input.touchCount > 0 && IsPointerOverUIObject()) // �ϳ� �̻��� ��ġ�� �߻��߰� UI ������Ʈ ���� �ƴ� ��
        {
            Touch touch = Input.GetTouch(0); // ù ��° ��ġ ������ ������
            if (touch.phase == TouchPhase.Began) // ��ġ�� ���۵Ǿ��� ��
            {
                HandleInput(touch.position); // �Է� ó�� �޼��� ȣ��
            }

            if (touch.phase == TouchPhase.Moved && isDragging) // ��ġ �̵� ���̰� �巡�� ���� ��
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // ��ġ ��ġ�� ���� ��ǥ�� ��ȯ
                touchPosition.z = 0; // z�� ���� 0���� ����
                currentObject.transform.position = touchPosition; // ���� �巡�� ���� ������Ʈ ��ġ�� ��ġ ��ġ�� ����
            }

            if (touch.phase == TouchPhase.Ended && isDragging) // ��ġ�� ������ ��
            {
                isDragging = false; // �巡�� ���� ����
                currentObject = null; // ���� �巡�� ���� ������Ʈ ����
            }
        }
    }

    // �Է� ó���� ����ϴ� �޼���
    void HandleInput(Vector3 inputPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition); // �Է� ��ġ�� ���� ��ǥ�� ��ȯ
        worldPosition.z = 0; // z�� ���� 0���� ����

        if (currentObject == null) // ���� �巡�� ���� ������Ʈ�� ���� ��
        {
            currentObject = Instantiate(selectedPrefab, worldPosition, Quaternion.identity); // ���ο� ������Ʈ ����
            // ������Ʈ�� SpriteRenderer ��������
            SpriteRenderer spriteRenderer = currentObject.GetComponent<SpriteRenderer>();

            // Sorting Layer�� Order in Layer ����
            if (spriteRenderer != null)
            {
                //spriteRenderer.sortingLayerName = "Foreground";  // ���ϴ� Sorting Layer �̸�
                spriteRenderer.sortingOrder = 10;  // ���ϴ� Order in Layer �� (�������� �տ� ��������)
            }

        }

        Collider2D collider = Physics2D.OverlapPoint(worldPosition); // �Է� ��ġ�� �ִ� Collider2D�� Ž��
        if (collider != null && collider.gameObject == currentObject) // Ž���� Collider2D�� ���� �巡�� ���� ������Ʈ�� ��
        {
            isDragging = true; // �巡�� ���·� ����
        }
    }

    // ���õ� �������� �����ϴ� �޼���
    public void SetSelectedPrefab(int index)
    {
        if (index >= 0 && index < objectPrefabs.Length)
        {
            selectedPrefab = objectPrefabs[index]; // ���õ� ���������� ����
        }
        else
        {
            Debug.LogError("�߸��� ������ �ε����Դϴ�. �ùٸ� �ε����� �Է��� �ּ���.");
        }
    }

    // UI ������Ʈ ���� �ִ��� Ȯ���ϴ� �޼���
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    // ��ư Ŭ�� �� ���õ� �������� ���콺 ��ġ�� �����ϴ� �޼���
    public void CreateObjectAtMousePosition()
    {
        if (selectedPrefab != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            mousePosition.z = 0; // z�� ���� 0���� ����
            currentObject = Instantiate(selectedPrefab, mousePosition, Quaternion.identity); // ���õ� �������� ���콺 ��ġ�� ����
            
            // ������Ʈ�� SpriteRenderer ��������
            SpriteRenderer spriteRenderer = currentObject.GetComponent<SpriteRenderer>();

            // Sorting Layer�� Order in Layer ����
            if (spriteRenderer != null)
            {
                //spriteRenderer.sortingLayerName = "Foreground";  // ���ϴ� Sorting Layer �̸�
                spriteRenderer.sortingOrder = 10;  // ���ϴ� Order in Layer �� (�������� �տ� ��������)
            }

        }
        else
        {
            Debug.LogError("���õ� �������� null�Դϴ�. �������� ������ �ּ���.");
        }
    }
}