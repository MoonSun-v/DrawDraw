using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    public PuzzleManager PuzzleManager;

    public GameObject Puzzle;  // 부모 오브젝트
    private Vector3 correctPosition;
    private Vector3 correctScale;
    private Vector3 initialPosition;

    public float x = 1.7f;
    public float y = 1.7f;

    public string layer = "1";

    public GameObject correctForm;
    private bool isMoving;

    private float startPosX;
    private float startPosY;

    private bool isStarted = true;
    private bool isScaled = false; // 크기 조정 여부

    public LayerMask parentLayerMask;

    void Start()
    {
        correctPosition = correctForm.transform.position;
        correctScale = correctForm.transform.localScale * 1.0f;
        initialPosition = transform.position;
    }

    void Update()
    {
        // 상태가 1일 때만 퍼즐 움직임 처리
        if (PuzzleManager.status == 1)
        {
            if (isStarted)
            {
                // 부모(Puzzle)의 크기를 0.15로 변경
                Puzzle.transform.localScale *= 0.5f;

                // 부모(Puzzle)의 위치를 새로 지정
                Vector3 newPosition = new Vector3(x, y, Puzzle.transform.position.z);
                Puzzle.transform.position = newPosition;
                isStarted = false;
            }

            if (isMoving)
            {
                Vector2 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, Puzzle.transform.localPosition.z);

                // 부모 오브젝트인 Puzzle을 움직임 (자식인 Piece도 자동으로 따라옴)
                Puzzle.transform.localPosition = newPosition;

                SpriteRenderer puzzleRenderer = Puzzle.GetComponent<SpriteRenderer>();
                puzzleRenderer.sortingLayerName = "0";

                foreach (Transform child in Puzzle.transform)
                {
                    SpriteRenderer pieceRenderer = child.GetComponent<SpriteRenderer>();
                    if (pieceRenderer != null)
                    {
                        pieceRenderer.sortingLayerName = puzzleRenderer.sortingLayerName;
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        // 상태가 1일 때만 동작
        if (PuzzleManager.status == 1 && Input.GetMouseButtonDown(0))
        {
            // Raycast를 통해 'Parent' 레이어만 인식, Ignore Raycast 레이어는 무시
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, parentLayerMask);

            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Parent"))
            {
                // 첫 번째 움직임일 때만 크기 변경
                if (!isScaled)
                {
                    // Puzzle과 자식인 Piece의 크기를 한 번만 변경
                    Puzzle.transform.localScale *= 2.47f;
                    isScaled = true;  // 크기가 변경되었음을 표시
                }

                Vector2 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                startPosX = mousePos.x - this.transform.localPosition.x;
                startPosY = mousePos.y - this.transform.localPosition.y;

                isMoving = true;
            }
        }
    }


    private void OnMouseUp()
    {
        // 퍼즐 이동 멈추기
        if (PuzzleManager.status == 1)
        {
            isMoving = false;

            SpriteRenderer puzzleRenderer = Puzzle.GetComponent<SpriteRenderer>();
            puzzleRenderer.sortingLayerName = layer;

            foreach (Transform child in Puzzle.transform)
            {
                SpriteRenderer pieceRenderer = child.GetComponent<SpriteRenderer>();
                if (pieceRenderer != null)
                {
                    pieceRenderer.sortingLayerName = puzzleRenderer.sortingLayerName;
                }
            }
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