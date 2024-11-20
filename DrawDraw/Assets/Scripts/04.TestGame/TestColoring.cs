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


    public Image Fighting;
    public Sprite DogFighting;
    private bool isDog;

    void Start()
    {
        isDog = !GameData.instance.playerdata.PlayerCharacter;  // �������� true, ����̸� false
        if (isDog) { Fighting.sprite = DogFighting; }

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
    // ���� ���� 
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

        // ���� ���� 
        SaveResults(score);
    }


    // [ ������ ���� ] : ���� ���� 9�� 
    //
    void SaveResults(int _score)
    {
        _score += 1; _score *= 10; // ���� 100���� ���� 
        if (_score == 0) { _score += 1; }

        int currentKey = GameData.instance.GetKeyWithIncompleteData();
        if (currentKey > 5)
        {
            Debug.LogWarning("TestResults�� �� �̻� ������ �� �����ϴ�. �ִ� Ű ���� 5�Դϴ�.");
            return;
        }

        if (!GameData.instance.testdata.TestResults.ContainsKey(currentKey))
        {
            GameData.instance.testdata.TestResults[currentKey] = new TestResultData();
        }

        TestResultData currentData = GameData.instance.testdata.TestResults[currentKey];
        currentData.Game7Score = _score;

        GameData.instance.SaveTestData();
        GameData.instance.LoadTestData();

        print($"TestResults[{currentKey}]�� Coloring ���� = {_score} ���� �Ϸ�");
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


    // ���� ����Ʈ ��ư Ŭ�� �̺�Ʈ
    public void ColorRedButton(GameObject redCrayon)
    {
        SelectColor(redCrayon, new Color(0.8901961f, 0.01568628f, 0.01960784f), 0); // ������ ��������Ʈ�� �迭�� ù ��° ���
    }

    public void ColorOrangeButton(GameObject orangeCrayon)
    {
        SelectColor(orangeCrayon, new Color(0.9411765f, 0.5294118f, 0.04705882f), 1); // ��Ȳ�� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorYellowButton(GameObject yellowCrayon)
    {
        SelectColor(yellowCrayon, new Color(0.945098f, 0.8431373f, 0.07058824f), 2); // ����� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorGreenButton(GameObject greenCrayon)
    {
        SelectColor(greenCrayon, new Color(0.2313726f, 0.6117647f, 0f), 3); // £�� �ʷϻ� ��������Ʈ�� �迭�� �� ��° ���
    }

    public void ColorSkyBlueButton(GameObject skyBlueCrayon)
    {
        SelectColor(skyBlueCrayon, new Color(0.007843138f, 0.5215687f, 0.9960784f), 4); // �ϴû� ��������Ʈ�� �迭�� �ټ� ��° ���
    }

    public void ColorBlueButton(GameObject blueCrayon)
    {
        SelectColor(blueCrayon, new Color(0.1803922f, 0.2f, 0.8431373f), 5); // �Ķ��� ��������Ʈ�� �迭�� ���� ��° ���
    }

    public void ColorPurpleButton(GameObject purpleCrayon)
    {
        SelectColor(purpleCrayon, new Color(0.3529412f, 0.0627451f, 0.7333333f), 6); // ����� ��������Ʈ�� �迭�� �ϰ� ��° ���
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