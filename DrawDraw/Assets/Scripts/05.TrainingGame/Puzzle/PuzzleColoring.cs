using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleColoring : MonoBehaviour
{
    public GameObject PuzzlePiece;
    private Color selectedColor; // 현재 선택된 색상

    public GameObject previousButton;                 // 이전에 클릭된 버튼을 추적하기 위한 변수
    public Vector3 previousButtonOriginalPosition;    // 이전 버튼의 원래 위치를 저장
    private int crayonMove = 90;

    public bool isSelectColor;

    private void Start()
    {
        // 초기화 작업이 필요한 경우 여기에 추가
    }

    private void Update()
    {
        // 퍼즐 조각을 색칠하는 로직이 여기에 포함될 수 있습니다.
        Coloring();
    }

    private void Coloring()
    {
        if (!isSelectColor || previousButton == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                PuzzlePiece piece = hit.collider.GetComponent<PuzzlePiece>();
                if (piece != null)
                {
                    piece.SetColor(selectedColor);
                }
            }
        }
    }

    public void SetColor(Color color)
    {
        selectedColor = color;

        if (!isSelectColor)
        {
            isSelectColor = true;
            Debug.Log("Color selected: " + color);
        }
    }

    private void UpdateButtonPosition(GameObject selectedButton)
    {
        // 이전 버튼이 있다면 원래 위치로 되돌리기
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // 현재 클릭된 버튼의 위치 조정
        RectTransform rt = selectedButton.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // 현재 버튼의 원래 위치 저장
        rt.localPosition = new Vector3(rt.localPosition.x - crayonMove, rt.localPosition.y, rt.localPosition.z);

        // 이전 버튼을 현재 버튼으로 업데이트
        previousButton = selectedButton;
    }

    public void ColorRedButton(GameObject redCrayon)
    {
        SetColor(Color.red);
        UpdateButtonPosition(redCrayon);
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SetColor(new Color(1f, 0.5f, 0f)); // Orange color
        UpdateButtonPosition(orangeCrayon);
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SetColor(Color.yellow);
        UpdateButtonPosition(yellowCrayon);
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SetColor(new Color(0f, 0.392f, 0f)); // Dark green color
        UpdateButtonPosition(greenCrayon);
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SetColor(new Color(0.529f, 0.808f, 0.922f)); // Sky blue color
        UpdateButtonPosition(skyBlueCrayon);
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SetColor(Color.blue);
        UpdateButtonPosition(blueCrayon);
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SetColor(new Color(0.859f, 0.439f, 0.576f)); // Purple color
        UpdateButtonPosition(purpleCrayon);
    }

    public void EraserButton(GameObject Eraser)
    {
        SetColor(color.white);
        UpdateButtonPosition(Eraser);
    }
}
