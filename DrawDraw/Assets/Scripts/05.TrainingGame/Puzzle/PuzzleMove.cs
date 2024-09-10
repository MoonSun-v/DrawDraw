using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    public GameObject Puzzle;

    private Vector3 correctPosition;
    private Vector3 correctScale;
    private Vector3 initialPosition;

    public GameObject correctForm;
    private bool isMoving;

    private float startPosX;
    private float startPosY;

    void Start()
    {
        correctPosition = correctForm.transform.position;
        correctScale = correctForm.transform.localScale * 1.0f;

        initialPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            Vector2 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            this.gameObject.transform.localPosition = newPosition;
            Puzzle.transform.localPosition = new Vector3(newPosition.x, newPosition.y, Puzzle.transform.localPosition.z);
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

            isMoving = true;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;
    }

    public bool IsInCorrectPosition()
    {
        Vector3 currentPos = this.transform.position;
        float halfWidth = correctScale.x / 2.0f;
        float halfHeight = correctScale.y / 2.0f;

        bool withinX = currentPos.x >= correctPosition.x - halfWidth && currentPos.x <= correctPosition.x + halfWidth;
        bool withinY = currentPos.y >= correctPosition.y - halfHeight && currentPos.y <= correctPosition.y + halfHeight;

        return withinX && withinY;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
