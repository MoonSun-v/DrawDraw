using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestColoring : MonoBehaviour
{

    public Color crayonColor; // 크레용 버튼의 색상
    public GameObject[] Shapes;

    private Color[] ShapesColors;

    public GameObject crayons;

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
        foreach (GameObject shape in Shapes)
        {
            SpriteRenderer shapeRenderer = shape.GetComponent<SpriteRenderer>();
            if (shapeRenderer != null)
            {
                // 도형의 색상을 초기화 (기본 색상 설정)
                shapeRenderer.color = Color.white; // 또는 초기 색상으로 설정
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
                foreach (GameObject shape in Shapes)
                {
                    if (hit.collider.gameObject == shape)
                    {
                        SpriteRenderer spriteRenderer = shape.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            // 선택한 크레용 색상 적용
                            Color newColor = crayonColor;
                            newColor.a = 1f; // 불투명도 설정
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
        // Shapes 배열에 있는 모든 도형들의 색상을 하얀색으로 변경
        foreach (GameObject shape in Shapes)
        {
            SpriteRenderer shapeRenderer = shape.GetComponent<SpriteRenderer>();
            if (shapeRenderer != null)
            {
                shapeRenderer.color = Color.white; // 색상을 하얀색으로 설정
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
        CheckAnswer();
        SceneManager.LoadScene("Test_DotLineScene");
    }

    private void CheckAnswer()
    {
        // 도형 색상 배열 초기화
        Color[] circleColors = new Color[3];
        Color[] triangleColors = new Color[3];
        Color[] squareColors = new Color[3];

        // 원 도형 색상 저장
        for (int i = 0; i < 3; i++)
        {
            circleColors[i] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // 삼각형 도형 색상 저장
        for (int i = 3; i < 6; i++)
        {
            triangleColors[i - 3] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // 사각형 도형 색상 저장
        for (int i = 6; i < 9; i++)
        {
            squareColors[i - 6] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // 색상 빈도 계산 함수
        int GetMostFrequentColorCount(Color[] colors)
        {
            Dictionary<Color, int> colorFrequency = new Dictionary<Color, int>();

            foreach (Color color in colors)
            {
                if (colorFrequency.ContainsKey(color))
                {
                    colorFrequency[color]++;
                }
                else
                {
                    colorFrequency[color] = 1;
                }
            }

            // 가장 많이 나타나는 색상 개수 반환
            int maxCount = 0;
            foreach (var entry in colorFrequency)
            {
                if (entry.Value > maxCount)
                {
                    maxCount = entry.Value;
                }
            }
            return maxCount;
        }

        // 점수 계산
        int circleScore = GetMostFrequentColorCount(circleColors);
        int triangleScore = GetMostFrequentColorCount(triangleColors);
        int squareScore = GetMostFrequentColorCount(squareColors);

        // 총 점수 계산
        int score = circleScore + triangleScore + squareScore;

        // 현재 점수 출력
        Debug.Log("Score: " + score);
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


    // 개별 크레용 버튼 클릭 이벤트
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, Color.red, 0); // 빨간색 스프라이트는 배열의 첫 번째 요소
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(1f, 0.5f, 0f), 1); // 주황색 스프라이트는 배열의 두 번째 요소
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, Color.yellow, 2); // 노란색 스프라이트는 배열의 세 번째 요소
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0f, 0.392f, 0f), 3); // 짙은 초록색 스프라이트는 배열의 네 번째 요소
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.529f, 0.808f, 0.922f), 4); // 하늘색 스프라이트는 배열의 다섯 번째 요소
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, Color.blue, 5); // 파란색 스프라이트는 배열의 여섯 번째 요소
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.859f, 0.439f, 0.576f), 6); // 보라색 스프라이트는 배열의 일곱 번째 요소
    }
}

/*
public class TestColoring : MonoBehaviour
{
    public Color crayonColor; // 크레용 버튼의 색상
    public GameObject[] Shapes;

    private Color[] ShapesColors;

    public GameObject crayons;

    public GameObject CheckPopup;

    // [ 색상 선택 관련 변수 ]
    public GameObject previousButton;                 // 이전에 클릭된 버튼을 추적하기 위한 변수
    public Vector3 previousButtonOriginalPosition;    // 이전 버튼의 원래 위치를 저장
    private const int CrayonMove = 90;                // 버튼 이동 거리

    private GameObject currentCrayon;                 // 현재 선택된 크레용

    void Start()
    {
        foreach (GameObject shape in Shapes)
        {
            SpriteRenderer shapeRenderer = shape.GetComponent<SpriteRenderer>();
            if (shapeRenderer != null)
            {
                // 도형의 색상을 초기화 (기본 색상 설정)
                shapeRenderer.color = Color.white; // 또는 초기 색상으로 설정
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
                foreach (GameObject shape in Shapes)
                {
                    if (hit.collider.gameObject == shape)
                    {
                        SpriteRenderer spriteRenderer = shape.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            // 선택한 크레용 색상 적용
                            Color newColor = crayonColor;
                            newColor.a = 1f; // 불투명도 설정
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
        CheckAnswer();
        SceneManager.LoadScene("Test_DotLineScene");
    }

    private void CheckAnswer()
    {
        // 도형 색상 배열 초기화
        Color[] circleColors = new Color[3];
        Color[] triangleColors = new Color[3];
        Color[] squareColors = new Color[3];

        // 원 도형 색상 저장
        for (int i = 0; i < 3; i++)
        {
            circleColors[i] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // 삼각형 도형 색상 저장
        for (int i = 3; i < 6; i++)
        {
            triangleColors[i - 3] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // 사각형 도형 색상 저장
        for (int i = 6; i < 9; i++)
        {
            squareColors[i - 6] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // 색상 빈도 계산 함수
        int GetMostFrequentColorCount(Color[] colors)
        {
            Dictionary<Color, int> colorFrequency = new Dictionary<Color, int>();

            foreach (Color color in colors)
            {
                if (colorFrequency.ContainsKey(color))
                {
                    colorFrequency[color]++;
                }
                else
                {
                    colorFrequency[color] = 1;
                }
            }

            // 가장 많이 나타나는 색상 개수 반환
            int maxCount = 0;
            foreach (var entry in colorFrequency)
            {
                if (entry.Value > maxCount)
                {
                    maxCount = entry.Value;
                }
            }
            return maxCount;
        }

        // 점수 계산
        int circleScore = GetMostFrequentColorCount(circleColors);
        int triangleScore = GetMostFrequentColorCount(triangleColors);
        int squareScore = GetMostFrequentColorCount(squareColors);

        // 총 점수 계산
        int score = circleScore + triangleScore + squareScore;

        // 현재 점수 출력
        Debug.Log("Score: " + score);
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
*/