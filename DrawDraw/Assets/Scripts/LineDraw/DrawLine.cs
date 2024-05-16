using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab; // ���� �׸��� ���� ������
    public float lineWidth = 0.1f; // ���� �ʺ�

    private LineRenderer currentLineRenderer; // ���� �׷����� ���� ���� ������
    private Vector2 previousPosition; // ���� ��ġ


    void Update()
    {
        // ���콺 Ŭ�� �Ǵ� ��ġ�� ���۵Ǹ� ���ο� ���� �׸���.
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            CreateNewLine();
        }
        // ���� ���� �׸��� ���̰� ���콺�� Ŭ���Ǿ��ų� ��ġ�� �̵� ���� ���, ���ο� ���� ���� �߰��Ѵ�.
        else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 currentPosition = GetInputPosition();
            if (Vector2.Distance(currentPosition, previousPosition) > 0.1f) // ���� �׸��� ���� �ּ����� �Ÿ�
            {
                AddPointToLine(currentPosition);
            }
        }
        // ���콺 Ŭ�� �Ǵ� ��ġ�� ������ ���� ���� ����.
        else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            currentLineRenderer = null;
        }

        void CreateNewLine()
        {
            // ���ο� ���� �׸� GameObject ����
            GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            currentLineRenderer = newLine.GetComponent<LineRenderer>();

            // ���� ������ ����
            currentLineRenderer.startWidth = lineWidth;
            currentLineRenderer.endWidth = lineWidth;
            currentLineRenderer.positionCount = 0; // ���� ��ġ�� �ʱ�ȭ�մϴ�.

            // ���� ��ġ�� ���� ��ġ�� ����
            previousPosition = GetInputPosition();
            AddPointToLine(previousPosition);

        }

        void AddPointToLine(Vector2 newPoint)
        {
            // ���ο� ���� ���� �߰�
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, newPoint);

            // ���� ��ġ�� ���� ��ġ�� ����
            previousPosition = newPoint;
        }

        // ���콺 Ŭ�� �Ǵ� ��ġ�� ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� ��ȯ
        Vector2 GetInputPosition()
        {
            Vector2 inputPosition = Vector2.zero;
            if (Input.touchCount > 0)
            {
                inputPosition = Input.GetTouch(0).position;
            }
            else
            {
                inputPosition = Input.mousePosition;
            }

            return Camera.main.ScreenToWorldPoint(inputPosition);
        }
    }
}
