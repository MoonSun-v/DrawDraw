using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangram : MonoBehaviour
{
    public GameObject correctForm;
    private bool isMoving;

    private float startPosX;
    private float startPosY;

    private Vector3 correctPosition;
    private Vector3 correctScale;

    private Vector3 initialPosition;

    public Vector2 startAreaMin;
    public Vector2 startAreaMax;
    public GameObject excludedAreaObject;
    private Rect excludedArea;

    private static List<Rect> placedPieces = new List<Rect>();
    public float minDistance = 1.0f;

    private Vector3 GetRandomStartPosition()
    {
        /*
        float randomX = Random.Range(startAreaMin.x, startAreaMax.x);
        float randomY = Random.Range(startAreaMin.y, startAreaMax.y);
        return new Vector3(randomX, randomY, this.transform.position.z);
        */
        Vector3 randomPosition;
        Rect pieceRect;

        do
        {
            float randomX = Random.Range(startAreaMin.x, startAreaMax.x);
            float randomY = Random.Range(startAreaMin.y, startAreaMax.y);
            randomPosition = new Vector3(randomX, randomY, this.transform.position.z);
            pieceRect = new Rect(randomPosition.x, randomPosition.y, this.transform.localScale.x, this.transform.localScale.y);
        } while (IsOverlapping(pieceRect) || excludedArea.Overlaps(pieceRect));

        return randomPosition;
    }

    void Start()
    {
        correctPosition = correctForm.transform.position;
        correctScale = correctForm.transform.localScale * 1.0f;

        if (excludedAreaObject != null)
        {
            Vector3 excludedPosition = excludedAreaObject.transform.position;
            Vector3 excludedScale = excludedAreaObject.transform.localScale;
            excludedArea = new Rect(excludedPosition.x - excludedScale.x / 2, excludedPosition.y - excludedScale.y / 2, excludedScale.x, excludedScale.y);
        }

        initialPosition = GetRandomStartPosition();
        this.transform.position = initialPosition;
        
        placedPieces.Add(new Rect(this.transform.position.x, this.transform.position.y, this.transform.localScale.x, this.transform.localScale.y));
    }

    void Update()
    {
        if (isMoving)
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
        this.transform.position = initialPosition;
    }

    private bool IsOverlapping(Rect pieceRect)
    {
        foreach (Rect placedPiece in placedPieces)
        {
            if (placedPiece.Overlaps(pieceRect))
            {
                return true;
            }

            Vector2 placedCenter = new Vector2(placedPiece.x + placedPiece.width / 2, placedPiece.y + placedPiece.height / 2);
            Vector2 pieceCenter = new Vector2(pieceRect.x + pieceRect.width / 2, pieceRect.y + pieceRect.height / 2);
            if (Vector2.Distance(placedCenter, pieceCenter) < minDistance)
            {
                return true;
            }
        }

        return false;
    }
}
