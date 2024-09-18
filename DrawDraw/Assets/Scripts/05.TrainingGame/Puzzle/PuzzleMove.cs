using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    public PuzzleManager PuzzleManager;

    public GameObject Puzzle;
    public GameObject Piece;

    private Vector3 correctPosition;
    private Vector3 correctScale;
    private Vector3 initialPosition;

    public GameObject correctForm;
    private bool isMoving;

    private float startPosX;
    private float startPosY;

    private bool isScaled = false;

    void Start()
    {
        correctPosition = correctForm.transform.position;
        correctScale = correctForm.transform.localScale * 1.0f;

        initialPosition = transform.position;
    }

    void Update()
    {
        if (PuzzleManager.status == 1)
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
    }

    private void OnMouseDown()
    {
        if (PuzzleManager.status == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isScaled)
                {
                    Puzzle.transform.localScale *= 2.47f;
                    Piece.transform.localScale *= 2.47f;
                    isScaled = true;
                }

                Vector2 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                startPosX = mousePos.x - this.transform.localPosition.x;
                startPosY = mousePos.y - this.transform.localPosition.y;

                isMoving = true;
            }
        }
    }

    private void OnMouseUp()
    {
        if (PuzzleManager.status == 1)
        {
            isMoving = false;
        }
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
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    public PuzzleManager PuzzleManager;

    public GameObject Puzzle;
    public GameObject Piece;

    private Vector3 correctPosition;
    private Vector3 correctScale;
    private Vector3 initialPosition;

    public SnapPoint[] snapPoints; // SnapPoint 배열
    public float snapThreshold = 0.5f;
    private bool isSnapped;

    public GameObject correctForm;
    private bool isMoving;

    private float startPosX;
    private float startPosY;

    private bool isScaled = false;

    void Start()
    {
        correctPosition = correctForm.transform.position;
        correctScale = correctForm.transform.localScale * 1.0f;

        initialPosition = transform.position;

        isSnapped = false;
    }

    void Update()
    {
        if (PuzzleManager.status == 1)
        {
            if (isMoving)
            {
                Vector2 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
                this.gameObject.transform.localPosition = newPosition;
                Puzzle.transform.localPosition = new Vector3(newPosition.x, newPosition.y, Puzzle.transform.localPosition.z);
            }
        }
    }

    private void OnMouseDown()
    {
        if (PuzzleManager.status == 1 && Input.GetMouseButtonDown(0))
        {
            if (!isScaled)
            {
                Puzzle.transform.localScale *= 2.47f;
                Piece.transform.localScale *= 2.47f;
                isScaled = true;
            }

            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isMoving = true;
        }
    }

    private void OnMouseUp()
    {
        if (PuzzleManager.status == 1)
        {
            isMoving = false;

            SnapPoint closestSnapPoint = GetClosestSnapPoint();

            if (closestSnapPoint != null)
            {
                if (!isSnapped)
                {
                    // 조각이 snap point에 붙으면
                    this.transform.position = closestSnapPoint.transform.position;
                    Puzzle.transform.position = closestSnapPoint.transform.position;
                    Piece.transform.position = closestSnapPoint.transform.position;

                    closestSnapPoint.SetOccupied(true);
                    isSnapped = true;
                }
                else
                {
                    // 조각이 snap point에서 떨어지면 snap point를 초기화
                    closestSnapPoint.SetOccupied(false);
                    isSnapped = false;

                    // 조각을 원래 위치로 되돌리기
                    this.transform.position = initialPosition;
                    Puzzle.transform.position = initialPosition;
                    Piece.transform.position = initialPosition;
                }
            }
        }
    }

    private SnapPoint GetClosestSnapPoint()
    {
        SnapPoint closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (SnapPoint snapPoint in snapPoints)
        {
            float distance = Vector3.Distance(this.transform.position, snapPoint.transform.position);

            if (distance < closestDistance && distance <= snapThreshold)
            {
                closestPoint = snapPoint;
                closestDistance = distance;
            }
        }

        return closestPoint;
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
}
*/