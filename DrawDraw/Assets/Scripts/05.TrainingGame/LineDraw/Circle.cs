using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{
    public int segments = 100;  // ���׸�Ʈ�� ��, ���� Ŭ���� �� �Ų����� ���� �˴ϴ�.
    public float xRadius = 5;   // X�� ������
    public float yRadius = 5;   // Y�� ������
    public Color gizmoColor = Color.green;  // Gizmo ����

    private LineRenderer line;
    // �ʱ�ȭ �� LineRenderer ����
    void Start()
    {
        // ���� ���� ������Ʈ�� �ִ� LineRenderer ������Ʈ�� ������
        line = gameObject.GetComponent<LineRenderer>();

        // LineRenderer�� �� ���� ����
        line.positionCount = segments + 1;

        // ���� ������ �������� �׸��� ���� (false�� �����Ͽ� ���� ���� �������� �׸�)
        line.useWorldSpace = false;

        // ���� ������ ����
        CreatePoints();
    }

    // ������ �׸� ������ �����ϴ� �Լ�
    void CreatePoints()
    {
        // �� ���� ���� (�� ����)
        float angle = 10f;

        // ���׸�Ʈ ����ŭ ���� �����Ͽ� LineRenderer�� ����
        for (int i = 0; i < (segments + 1); i++)
        {
            // ������ ���� X, Y ��ǥ ���
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;

            // LineRenderer�� ��ǥ ����
            line.SetPosition(i, new Vector3(x, y, 0));

            // ���� ���� ���� ���� ����
            angle += (360f / segments);
        }
    }

    // ������ ��忡�� ������ �׸��� �Լ� (Gizmos ���)
    void OnDrawGizmos()
    {
        // Gizmo�� ���� ����
        Gizmos.color = gizmoColor;

        // �� ���� ���� (�� ����)
        float angle = 10f;

        // ù ��° ���� ������ ���� ������ ����
        Vector3 firstPoint = Vector3.zero;
        Vector3 lastPoint = Vector3.zero;

        // ���׸�Ʈ ����ŭ ���� �����Ͽ� Gizmo�� ������ �׸�
        for (int i = 0; i < segments + 1; i++)
        {
            // ������ ���� X, Y ��ǥ ��� �� ���� ������Ʈ�� ��ġ�� ����
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;
            Vector3 point = new Vector3(x, y, 0) + transform.position; // ���� ������Ʈ�� ��ġ�� �ݿ�

            // ù �� ���ĺ��ʹ� ���� ���� ���� ���� �����ϴ� ���� �׸�
            if (i > 0)
            {
                Gizmos.DrawLine(lastPoint, point);
            }
            else
            {
                // ù ��° �� ����
                firstPoint = point;
            }

            // ������ �� ������Ʈ
            lastPoint = point;

            // ���� ���� ���� ���� ����
            angle += (360f / segments);
        }

        // ������ ���� ù ��° ���� �����Ͽ� ���� �ϼ�
        Gizmos.DrawLine(lastPoint, firstPoint);
    }
}