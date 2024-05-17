using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangram : MonoBehaviour
{
    public GameObject correctForm;
    private bool moving;

    private float startPosX;
    private float startPosY;

    private Vector3 correctPosition;
    private Vector3 correctScale;

    void Start()
    {
        // ������ ��ġ�� correctForm�� ��ġ�� ũ��� �����մϴ�.
        correctPosition = correctForm.transform.position;
        correctScale = correctForm.transform.localScale * 1.0f;
    }

    void Update()
    {
        if (moving)
        {
            Vector2 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;
    }

    public bool IsInCorrectPosition()
    {
        Vector3 currentPos = this.transform.position;
        float halfWidth = correctScale.x / 2f;
        float halfHeight = correctScale.y / 2f;

        // ���� ������ ������ ��ġ�� ���� �ȿ� �ִ��� Ȯ���մϴ�.
        bool withinX = currentPos.x >= correctPosition.x - halfWidth && currentPos.x <= correctPosition.x + halfWidth;
        bool withinY = currentPos.y >= correctPosition.y - halfHeight && currentPos.y <= correctPosition.y + halfHeight;

        return withinX && withinY;
    }
}
