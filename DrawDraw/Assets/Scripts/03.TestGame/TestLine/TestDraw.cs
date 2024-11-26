using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDraw : MonoBehaviour
{
    public GameObject linePrefab; // ���� �׸��� ���� ������
    public float lineWidth = 0.15f; // ���� �ʺ�

    public GameObject TestDrawManager;

    private LineRenderer currentLineRenderer; // ���� �׷����� ���� ���� ������
    private Vector2 previousPosition; // ���� ��ġ
    private Vector2 previousPosition2; // ������ ��ġ

    private List<GameObject> lines = new List<GameObject>();

    private bool isDrawing = false;

    public void Update()
    {
        if (TestDrawManager == null)
        {
            Debug.LogError("TestDrawManager is not assigned!");
            return;
        }

        // �׸��� ���� �ȿ� �־�� �׸��� ����
        if (TestDrawManager.GetComponent<TestDrawManager>().DrawActivate)
        {
            // ���콺 Ŭ�� �Ǵ� ��ġ�� ���۵Ǹ� ���ο� ���� �׸���.
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CreateNewLine();
                isDrawing = true; // �� �׸��� ����
            }
            // ���� �׸��� ���� �� �ൿ
            else if (isDrawing && (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)))
            {
                Vector2 currentPosition = GetInputPosition();

                if (Vector2.Distance(currentPosition, previousPosition) > 0.1f) // ���� �׸��� ���� �ּ����� �Ÿ�
                {
                    AddPointToLine(currentPosition);
                }
            }
            // ���콺 Ŭ�� �Ǵ� ��ġ�� ������ �׸��� ����
            else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                currentLineRenderer = null; // ���� �׸��� �� ����
                isDrawing = false; // �� �׸��� ����
            }
        }
        else
        {
            previousPosition = previousPosition2; // ���� ��ġ �ʱ�ȭ
        }
    }

    void CreateNewLine()
    {
        // ���ο� ���� �׸� GameObject ����
        GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLineRenderer = newLine.GetComponent<LineRenderer>();
        currentLineRenderer.sortingOrder = 1;

        // ������ ���� ����Ʈ�� �߰�
        lines.Add(newLine);

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
        previousPosition2 = previousPosition;
        previousPosition = newPoint;

    }

    public void ClearAllLines()
    {
        // ����Ʈ�� ����� ��� ��(GameObject)�� ����
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        // ����Ʈ�� �ʱ�ȭ
        lines.Clear();
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
