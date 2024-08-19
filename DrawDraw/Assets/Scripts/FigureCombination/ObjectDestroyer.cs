using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // Ư�� ������ �����ϴ� ���� ������Ʈ (��� ����)
    public GameObject boundaryObject;
    private Collider2D boundaryCollider;

    void Start()
    {
        // boundaryObject�κ��� Collider2D ������Ʈ�� ������
        if (boundaryObject != null)
        {
            boundaryCollider = boundaryObject.GetComponent<Collider2D>();

            // Collider2D�� �������� ������ ��� �޽��� ���
            if (boundaryCollider == null)
            {
                Debug.LogWarning("Boundary Object does not have a Collider2D component.");
            }
        }
        else
        {
            Debug.LogWarning("Boundary Object is not assigned.");
        }
    }

    void Update()
    {
        if (boundaryCollider != null)
        {
            // ���� ������Ʈ�� Collider2D ��������
            Collider2D objectCollider = GetComponent<Collider2D>();

            if (objectCollider != null)
            {
                // ������Ʈ�� ��� �ݶ��̴� ���� �ִ��� Ȯ��
                if (!boundaryCollider.bounds.Contains(objectCollider.bounds.min) ||
                    !boundaryCollider.bounds.Contains(objectCollider.bounds.max))
                {
                    Destroy(gameObject); // ��� ������ ������ ������Ʈ ����
                }
            }
        }
    }
}
