using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestColoring : MonoBehaviour
{

    public Color crayonColor; // ũ���� ��ư�� ����
    public GameObject[] Shapes;

    private Color[] ShapesColors;

    public GameObject crayons;

    public GameObject CheckPopup;

    // [ ���� ���� ���� ���� ]
    public GameObject previousButton;                 // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition;    // ���� ��ư�� ���� ��ġ�� ����
    private const int CrayonMove = 90;                // ��ư �̵� �Ÿ�

    private GameObject currentCrayon;                 // ���� ���õ� ũ����



    public Sprite[] selectedSprites; // ���õ� ��������Ʈ�� ������ �迭
    public Sprite previousButtonOriginalSprite;       // ���� ��ư�� ���� ��������Ʈ�� ����


    void Start()
    {
        foreach (GameObject shape in Shapes)
        {
            SpriteRenderer shapeRenderer = shape.GetComponent<SpriteRenderer>();
            if (shapeRenderer != null)
            {
                // ������ ������ �ʱ�ȭ (�⺻ ���� ����)
                shapeRenderer.color = Color.white; // �Ǵ� �ʱ� �������� ����
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
                            // ������ ũ���� ���� ����
                            Color newColor = crayonColor;
                            newColor.a = 1f; // ������ ����
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
        // Shapes �迭�� �ִ� ��� �������� ������ �Ͼ������ ����
        foreach (GameObject shape in Shapes)
        {
            SpriteRenderer shapeRenderer = shape.GetComponent<SpriteRenderer>();
            if (shapeRenderer != null)
            {
                shapeRenderer.color = Color.white; // ������ �Ͼ������ ����
            }
        }
    }

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }

    // �˾� : �ϼ��̾�
    public void NextBtn()
    {
        CheckPopup.SetActive(false);
        CheckAnswer();
        SceneManager.LoadScene("Test_DotLineScene");
    }

    private void CheckAnswer()
    {
        // ���� ���� �迭 �ʱ�ȭ
        Color[] circleColors = new Color[3];
        Color[] triangleColors = new Color[3];
        Color[] squareColors = new Color[3];

        // �� ���� ���� ����
        for (int i = 0; i < 3; i++)
        {
            circleColors[i] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // �ﰢ�� ���� ���� ����
        for (int i = 3; i < 6; i++)
        {
            triangleColors[i - 3] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // �簢�� ���� ���� ����
        for (int i = 6; i < 9; i++)
        {
            squareColors[i - 6] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // ���� �� ��� �Լ�
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

            // ���� ���� ��Ÿ���� ���� ���� ��ȯ
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

        // ���� ���
        int circleScore = GetMostFrequentColorCount(circleColors);
        int triangleScore = GetMostFrequentColorCount(triangleColors);
        int squareScore = GetMostFrequentColorCount(squareColors);

        // �� ���� ���
        int score = circleScore + triangleScore + squareScore;

        // ���� ���� ���
        Debug.Log("Score: " + score);
    }

    public void SelectColor(GameObject crayon, Color selectedColor, int spriteIndex)
    {
        crayonColor = selectedColor;

        // ������ ������ ��ư�� �ִٸ� ���� �̹����� ����
        if (previousButton != null)
        {
            Image prevImage = previousButton.GetComponent<Image>();
            prevImage.sprite = previousButtonOriginalSprite;  // ���� ��ư ��������Ʈ ����
            prevImage.SetNativeSize(); // ���� ��ư�� ũ�⸦ ���� �̹��� ũ��� ����
        }

        // ���� ���õ� ũ������ �̹����� ����
        Image currentImage = crayon.GetComponent<Image>();
        previousButtonOriginalSprite = currentImage.sprite;  // ���� ��������Ʈ ����
        currentImage.sprite = selectedSprites[spriteIndex];  // ���õ� ��������Ʈ�� ����
        currentImage.SetNativeSize(); // ���õ� ũ���� ��ư�� ũ�⸦ ���� �̹��� ũ��� ����

        previousButton = crayon;  // ���õ� ��ư�� ����
    }


    // ���� ũ���� ��ư Ŭ�� �̺�Ʈ
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, Color.red, 0); // ������ ��������Ʈ�� �迭�� ù ��° ���
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(1f, 0.5f, 0f), 1); // ��Ȳ�� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, Color.yellow, 2); // ����� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0f, 0.392f, 0f), 3); // £�� �ʷϻ� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.529f, 0.808f, 0.922f), 4); // �ϴû� ��������Ʈ�� �迭�� �ټ� ��° ���
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, Color.blue, 5); // �Ķ��� ��������Ʈ�� �迭�� ���� ��° ���
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.859f, 0.439f, 0.576f), 6); // ����� ��������Ʈ�� �迭�� �ϰ� ��° ���
    }
}

/*
public class TestColoring : MonoBehaviour
{
    public Color crayonColor; // ũ���� ��ư�� ����
    public GameObject[] Shapes;

    private Color[] ShapesColors;

    public GameObject crayons;

    public GameObject CheckPopup;

    // [ ���� ���� ���� ���� ]
    public GameObject previousButton;                 // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition;    // ���� ��ư�� ���� ��ġ�� ����
    private const int CrayonMove = 90;                // ��ư �̵� �Ÿ�

    private GameObject currentCrayon;                 // ���� ���õ� ũ����

    void Start()
    {
        foreach (GameObject shape in Shapes)
        {
            SpriteRenderer shapeRenderer = shape.GetComponent<SpriteRenderer>();
            if (shapeRenderer != null)
            {
                // ������ ������ �ʱ�ȭ (�⺻ ���� ����)
                shapeRenderer.color = Color.white; // �Ǵ� �ʱ� �������� ����
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
                            // ������ ũ���� ���� ����
                            Color newColor = crayonColor;
                            newColor.a = 1f; // ������ ����
                            spriteRenderer.color = newColor;
                        }
                        break;
                    }
                }
            }
        }
    }

    // �Ϸ� Ȯ�� �˾�
    public void CheckPopUp()
    {
        CheckPopup.SetActive(true);
    }

    // �˾� : �����̾�
    public void PreviousBtn()
    {
        CheckPopup.SetActive(false);
    }

    // �˾� : �ϼ��̾�
    public void NextBtn()
    {
        CheckPopup.SetActive(false);
        CheckAnswer();
        SceneManager.LoadScene("Test_DotLineScene");
    }

    private void CheckAnswer()
    {
        // ���� ���� �迭 �ʱ�ȭ
        Color[] circleColors = new Color[3];
        Color[] triangleColors = new Color[3];
        Color[] squareColors = new Color[3];

        // �� ���� ���� ����
        for (int i = 0; i < 3; i++)
        {
            circleColors[i] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // �ﰢ�� ���� ���� ����
        for (int i = 3; i < 6; i++)
        {
            triangleColors[i - 3] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // �簢�� ���� ���� ����
        for (int i = 6; i < 9; i++)
        {
            squareColors[i - 6] = Shapes[i].GetComponent<SpriteRenderer>().color;
        }

        // ���� �� ��� �Լ�
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

            // ���� ���� ��Ÿ���� ���� ���� ��ȯ
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

        // ���� ���
        int circleScore = GetMostFrequentColorCount(circleColors);
        int triangleScore = GetMostFrequentColorCount(triangleColors);
        int squareScore = GetMostFrequentColorCount(squareColors);

        // �� ���� ���
        int score = circleScore + triangleScore + squareScore;

        // ���� ���� ���
        Debug.Log("Score: " + score);
    }

    // ���� ��ư Ŭ�� �Լ� (��� ��ư�� ���� ���������� ��� ����)
    public void SelectColor(GameObject crayon, Color selectedColor)
    {
        crayonColor = selectedColor;

        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;  // ���� ��ư ��ġ ����
        }

        RectTransform rt = crayon.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition;
        rt.localPosition = new Vector3(rt.localPosition.x - CrayonMove, rt.localPosition.y, rt.localPosition.z);

        previousButton = crayon;
    }

    // ���� ���� ��ư Ŭ�� �̺�Ʈ
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, Color.red);
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(1f, 0.5f, 0f)); // ��Ȳ��
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, Color.yellow);
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0f, 0.392f, 0f)); // £�� �ʷϻ�
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.529f, 0.808f, 0.922f)); // �ϴû�
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, Color.blue);
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.859f, 0.439f, 0.576f)); // �����
    }

    public void EraserButton(GameObject eraser)
    {
        SelectColor(eraser, Color.white); // ���찳�� ���
    }
}
*/