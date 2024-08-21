using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectOnCollision : MonoBehaviour
{
    public string baseTag = "baseSquare";  // "base" �±׸� ���� ������Ʈ�� ã�� ���� ����� �±� �̸�

    void Update()
    {
        // ����1�� "base" �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        if (IsObjectAOnAnyBaseObject())
        {
            // Scale ���󺹱�
            Vector3 scale = transform.localScale;
            if (scale.y < 0)
            {
                scale.y = -Mathf.Abs(scale.y);  // y �� ����x���� -> ���
                transform.localScale = scale;
            }
            // ����1�� ���� ����� "base" �±׸� ���� ������Ʈ�� ��ġ�� �̵�
            MoveObjectAToClosestBaseObject(baseTag);
        }
    }

    // ����1�� "base" �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ���ϴ� �޼���
    bool IsObjectAOnAnyBaseObject()
    {

        Collider2D colliderA = GetComponent<Collider2D>();

        // "base" �±׸� ���� ��� ������Ʈ�� ã��
        GameObject[] baseObjects = GameObject.FindGameObjectsWithTag(baseTag);

        // �� "base" �±� ������Ʈ���� �浹 ���� Ȯ��
        foreach (GameObject baseObject in baseObjects)
        {
            Collider2D colliderB = baseObject.GetComponent<Collider2D>();
            if (colliderA != null && colliderB != null && colliderA.IsTouching(colliderB))
            {
                // �浹�� ��� �ֿܼ� �޽��� ���
                //Debug.Log("����1�� 'base' �±׸� ���� ������Ʈ�� �浹�߽��ϴ�: " + baseObject.name);
                return true;
            }
        }

        return false;
    }

        // �浹�� "base" �±� ������Ʈ �� ���� ����� ������Ʈ�� ��ġ�� ����1�� �̵��ϴ� �޼���
        void MoveObjectAToClosestBaseObject(string tag)
    {
        Collider2D colliderA = GetComponent<Collider2D>();
        Transform closestBaseObject = null;
        float closestDistance = float.MaxValue;

        // "base" �±׸� ���� ��� ������Ʈ�� ��ȸ�ϸ� ���� ����� �浹�� ������Ʈ ã��
        GameObject[] baseObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject baseObject in baseObjects)
        {
            Collider2D colliderB = baseObject.GetComponent<Collider2D>();
            if (colliderA != null && colliderB != null && colliderA.IsTouching(colliderB))
            {
                // ����1�� "base" ������Ʈ ������ �Ÿ� ���
                float distance = Vector2.Distance(transform.position, baseObject.transform.position);
                if (distance < closestDistance)
                {
                    // �� ����� "base" ������Ʈ�� ã���� ��� ������Ʈ
                    closestDistance = distance;
                    closestBaseObject = baseObject.transform;
                }
            }
        }

        // ���� ����� "base" ������Ʈ�� ��ġ�� ����1�� �̵�
        if (closestBaseObject != null)
        {
            transform.position = closestBaseObject.position;
        }
    }
}