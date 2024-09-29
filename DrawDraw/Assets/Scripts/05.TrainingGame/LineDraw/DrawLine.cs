using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab; // ���� �׸��� ���� ������
    public float lineWidth = 0.1f; // ���� �ʺ�

    private LineRenderer currentLineRenderer; // ���� �׷����� ���� ���� ������
    private Vector2 previousPosition; // ���� ��ġ
    private Vector2 previousPosition2; // ������ ��ġ

    private List<GameObject> lines = new List<GameObject>();

    [SerializeField]
    private MonoBehaviour LineDrawManager; // Ȱ��ȭ�� �Ǵ��� ��ũ��Ʈ

    public GameObject finishButton;
    //public Sprite defaultSprite;
    public Sprite activeSprite;

    private bool isDrawing = false;
    private float timer = 0f;
    public float timeLimit = 5f; // ���� �׷����� ���� �� ���� ������ �Ǵ� �ð� (�� ����)

    public GameObject timeChar;
    public GameObject check; // ������ Ȯ��â �˾�
    public GameObject finish; // ������ ���â �˾�

    public ColorButtonManager ColorButtonManager; // ColorManager ��ũ��Ʈ ���� (colorCode �� ��������)
    private Color lineColor;


    void Update()
    {
        // Ÿ�̸� ������Ʈ
        timer += Time.deltaTime;
        if (!isDrawing)
        {
            if (timer >= timeLimit)
            {
                //Debug.Log("5�� ����");
                if (check.activeSelf == true || finish.activeSelf == true)
                {
                    timeChar.SetActive(false);
                    timer = 0f;
                }
                else
                {
                    timeChar.SetActive(true);
                    Invoke("timeEffect", 3f);
                }
            }
        }
        else
        {
            // ���� �׷����� ���� ���� Ÿ�̸Ӹ� �ʱ�ȭ
            timer = 0f;
        }


        // �׸��� ���� �ȿ� �־�� �׸��� ����
        if (LineDrawManager.GetComponent<LineDrawManager>().DrawActivate)
        { // ���콺 Ŭ�� �Ǵ� ��ġ�� ���۵Ǹ� ���ο� ���� �׸���.
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CreateNewLine(lineColor);
            }
            // ���� ���� �׸��� ���̰� ���콺�� Ŭ���Ǿ��ų� ��ġ�� �̵� ���� ���, ���ο� ���� ���� �߰��Ѵ�.
            else if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                Vector2 currentPosition = GetInputPosition();

                if (Vector2.Distance(currentPosition, previousPosition) > 0.1f) // ���� �׸��� ���� �ּ����� �Ÿ�
                {
                    if (currentLineRenderer == null)
                    {
                        CreateNewLine(lineColor);
                    }
                    AddPointToLine(currentPosition);

                    // ���� �׷����� ��ư �̹��� ����
                    finishButton.GetComponent<Image>().sprite = activeSprite;
                    finishButton.GetComponent<Image>().preserveAspect = true; // ���� ����
                                                                              // ��ư �̹����� �θ� UI ������Ʈ�� RectTransform�� �����ɴϴ�.
                    RectTransform buttonRectTransform = finishButton.GetComponent<RectTransform>();

                    // ���ϴ� �����Ϸ� ��ư �̹����� ũ�⸦ �����մϴ�.
                    float desiredScaleX = 1.67f; // x�� ������
                    float desiredScaleY = 1.67f; // y�� ������
                    buttonRectTransform.localScale = new Vector2(desiredScaleX, desiredScaleY);
                }
            }
            // ���콺 Ŭ�� �Ǵ� ��ġ�� ������ ���� ���� ����.
            else if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                currentLineRenderer = null; // ���� �׸��� �� ����
                isDrawing = false;
            }
        }

        else //���� ���� ���
        {
            previousPosition = previousPosition2; // ���� ��ġ �ʱ�ȭ
        }
    }
    //*********************************************************************************************************************

    void CreateNewLine(Color newColor)
    {
        isDrawing = true;

        // ���ο� ���� �׸� GameObject ����
        GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLineRenderer = newLine.GetComponent<LineRenderer>();

        // ������ ���� ����Ʈ�� �߰�
        lines.Add(newLine);

        // ���� ������ ����
        // colorManager�� colorCode ���� �����ͼ� LineRenderer�� ������ ����
        if (ColorButtonManager.isActive)
        {
            lineColor = ColorButtonManager.selectedColor;
        }
        else
        {
            lineColor = Color.black;
        }

        ChangeLineColor(lineColor);

        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.positionCount = 0; // ���� ��ġ�� �ʱ�ȭ�մϴ�.

        // ���� ��ġ�� ���� ��ġ�� ����
        previousPosition = GetInputPosition();
        AddPointToLine(previousPosition);
    }

    void AddPointToLine(Vector2 newPoint)
    {
        // ���ο� ���� ���� �߰�
        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, newPoint);

        // ���� ��ġ�� ���� ��ġ�� ����
        previousPosition2 = previousPosition;
        previousPosition = newPoint;

    }

    public void ClearAllLines()
    {
        // ����Ʈ�� ����� ��� ��(GameObject)�� ����
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }

        // ����Ʈ�� �ʱ�ȭ
        lines.Clear();
    }

    // ���콺 Ŭ�� �Ǵ� ��ġ�� ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� ��ȯ
    Vector2 GetInputPosition()
    {
        Vector2 inputPosition = Vector2.zero;
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }

        return Camera.main.ScreenToWorldPoint(inputPosition);
    }

    private void timeEffect()
    {
        timeChar.SetActive(false);
        timer = 0f;
    }

    // LineRenderer�� ������ �����ϴ� �Լ�
    void ChangeLineColor(Color newColor)
    {
        if (currentLineRenderer != null)
        {            
            // LineRenderer�� ������ ����
            currentLineRenderer.startColor = newColor;
            currentLineRenderer.endColor = newColor;

            // �߰��� LineRenderer�� material ���� ����
            if (currentLineRenderer.material != null)
            {
                currentLineRenderer.material.color = newColor;
            }
            else
            {
                Debug.LogWarning("LineRenderer�� Material�� �������� �ʾҽ��ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("LineRenderer�� �������� �ʾҽ��ϴ�.");
        }
    }
}
