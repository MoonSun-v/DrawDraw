using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleColoring : MonoBehaviour
{
    public Color crayonColor; // 크레용 버튼의 색상
    public GameObject[] Pieces;
    public GameObject[] Puzzles;

    private Color[] pieceColors;

    public GameObject colorboard;
    public GameObject puzzleboard;
    public GameObject crayons;

    public PuzzleManager PuzzleManager;
    public Color puzzleColor = new Color(0.8f, 0.8f, 0.8f);

    public GameObject CheckPopup;

    // [ 색상 선택 관련 변수 ]
    public GameObject previousButton;                 // 이전에 클릭된 버튼을 추적하기 위한 변수
    public Vector3 previousButtonOriginalPosition;    // 이전 버튼의 원래 위치를 저장
    private const int CrayonMove = 90;                // 버튼 이동 거리

    private GameObject currentCrayon;                 // 현재 선택된 크레용

    void Start()
    {
        pieceColors = new Color[Pieces.Length];

        foreach (GameObject piece in Pieces)
        {
            if (piece.GetComponent<Collider2D>() == null)
            {
                piece.AddComponent<PolygonCollider2D>();
            }
        }

        for (int i = 0; i < Puzzles.Length; i++)
        {
            SpriteRenderer puzzleRenderer = Puzzles[i].GetComponent<SpriteRenderer>();
            if (puzzleRenderer != null)
            {
                puzzleRenderer.color = puzzleColor;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                foreach (GameObject piece in Pieces)
                {
                    if (hit.collider.gameObject == piece)
                    {
                        SpriteRenderer spriteRenderer = piece.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            // 선택한 크레용 색상 적용
                            Color newColor = crayonColor;
                            newColor.a = 1f;
                            spriteRenderer.color = newColor;
                        }
                        break;
                    }
                }
            }
        }
    }

    // 완료 확인 팝업
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }

    // 팝업 : 아직이야
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }

    // 팝업 : 완성이야
    public void NextBtn()
    {
        CheckPopup.SetActive(false);

        if (PuzzleManager.status == 0)
        {
            // 퍼즐 조각 색상 저장
            for (int i = 0; i < Pieces.Length; i++)
            {
                SpriteRenderer spriteRenderer = Pieces[i].GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    pieceColors[i] = spriteRenderer.color;
                }
            }

            float m = 1.7f;

            // 퍼즐 조각 위치와 크기 변경
            for (int i = 0; i < Pieces.Length; i++)
            {
                Pieces[i].transform.localScale *= 0.5f;
                Pieces[i].transform.position = new Vector3(5.4f, m, Pieces[i].transform.position.z);
                m -= 1.7f;
            }

            float n = 1.7f;

            for (int i = 0; i < Puzzles.Length; i++)
            {
                Puzzles[i].transform.localScale *= 0.5f;
                Puzzles[i].transform.position = new Vector3(5.4f, n, Puzzles[i].transform.position.z);

                SpriteRenderer puzzleRenderer = Puzzles[i].GetComponent<SpriteRenderer>();
                if (puzzleRenderer != null)
                {
                    puzzleRenderer.color = Color.white;
                }

                n -= 1.7f;
            }

            colorboard.SetActive(false);
            crayons.SetActive(false);
            puzzleboard.SetActive(true);

            PuzzleManager.status = 1;
        }
    }

    // 색상 버튼 클릭 함수 (모든 버튼에 대해 공통적으로 사용 가능)
    public void SelectColor(GameObject crayon, Color selectedColor)
    {
        crayonColor = selectedColor;

        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;  // 이전 버튼 위치 복구
        }

        RectTransform rt = crayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition;
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = crayon;
    }

    // 개별 색상 버튼 클릭 이벤트
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, Color.red);
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(1f, 0.5f, 0f)); // 주황색
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, Color.yellow);
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0f, 0.392f, 0f)); // 짙은 초록색
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.529f, 0.808f, 0.922f)); // 하늘색
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, Color.blue);
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.859f, 0.439f, 0.576f)); // 보라색
    }

    public void EraserButton(GameObject eraser)
    {
        SelectColor(eraser, Color.white); // 지우개는 흰색
    }
}
