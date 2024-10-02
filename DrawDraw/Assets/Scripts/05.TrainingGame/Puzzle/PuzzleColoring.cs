using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Sprite[] selectedSprites; // 선택된 스프라이트를 저장할 배열
    public Sprite previousButtonOriginalSprite;       // 이전 버튼의 원래 스프라이트를 저장


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

    public void ResetBtn()
    {
        // Pieces 배열에 있는 모든 퍼즐 조각들의 색상을 하얀색으로 변경
        foreach (GameObject piece in Pieces)
        {
            SpriteRenderer pieceRenderer = piece.GetComponent<SpriteRenderer>();
            if (pieceRenderer != null)
            {
                pieceRenderer.color = Color.white; // 색상을 하얀색으로 설정
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

    public void SelectColor(GameObject crayon, Color selectedColor, int spriteIndex)
    {
        crayonColor = selectedColor;

        // 이전에 선택한 버튼이 있다면 원래 이미지로 복구
        if (previousButton != null)
        {
            Image prevImage = previousButton.GetComponent<Image>();
            prevImage.sprite = previousButtonOriginalSprite;  // 이전 버튼 스프라이트 복구
            prevImage.SetNativeSize(); // 이전 버튼의 크기를 원래 이미지 크기로 복구
        }

        // 현재 선택된 크레용의 이미지를 변경
        Image currentImage = crayon.GetComponent<Image>();
        previousButtonOriginalSprite = currentImage.sprite;  // 현재 스프라이트 저장
        currentImage.sprite = selectedSprites[spriteIndex];  // 선택된 스프라이트로 변경
        currentImage.SetNativeSize(); // 선택된 크레용 버튼의 크기를 원래 이미지 크기로 설정

        previousButton = crayon;  // 선택된 버튼을 추적
    }

    // 개별 페인트 버튼 클릭 이벤트
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, new Color(0.8901961f, 0.01568628f, 0.01960784f), 0); // 빨간색 스프라이트는 배열의 첫 번째 요소
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(0.9411765f, 0.5294118f, 0.04705882f), 1); // 주황색 스프라이트는 배열의 두 번째 요소
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, new Color(0.945098f, 0.8431373f, 0.07058824f), 2); // 노란색 스프라이트는 배열의 세 번째 요소
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0.2313726f, 0.6117647f, 0f), 3); // 짙은 초록색 스프라이트는 배열의 네 번째 요소
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.007843138f, 0.5215687f, 0.9960784f), 4); // 하늘색 스프라이트는 배열의 다섯 번째 요소
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, new Color(0.1803922f, 0.2f, 0.8431373f), 5); // 파란색 스프라이트는 배열의 여섯 번째 요소
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.3529412f, 0.0627451f, 0.7333333f), 6); // 보라색 스프라이트는 배열의 일곱 번째 요소
    }
}
