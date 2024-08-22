using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleColoring : MonoBehaviour
{
    public GameObject PuzzlePiece;
    private Color selectedColor; // ���� ���õ� ����

    public GameObject previousButton;                 // ������ Ŭ���� ��ư�� �����ϱ� ���� ����
    public Vector3 previousButtonOriginalPosition;    // ���� ��ư�� ���� ��ġ�� ����
    private int crayonMove = 90;

    public bool isSelectColor;

    private void Start()
    {
        // �ʱ�ȭ �۾��� �ʿ��� ��� ���⿡ �߰�
    }

    private void Update()
    {
        // ���� ������ ��ĥ�ϴ� ������ ���⿡ ���Ե� �� �ֽ��ϴ�.
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
        // ���� ��ư�� �ִٸ� ���� ��ġ�� �ǵ�����
        if (previousButton != null)
        {
            RectTransform prevRt = previousButton.GetComponent<RectTransform>();
            prevRt.localPosition = previousButtonOriginalPosition;
        }

        // ���� Ŭ���� ��ư�� ��ġ ����
        RectTransform rt = selectedButton.GetComponent<RectTransform>();
        previousButtonOriginalPosition = rt.localPosition; // ���� ��ư�� ���� ��ġ ����
        rt.localPosition = new Vector3(rt.localPosition.x - crayonMove, rt.localPosition.y, rt.localPosition.z);

        // ���� ��ư�� ���� ��ư���� ������Ʈ
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
