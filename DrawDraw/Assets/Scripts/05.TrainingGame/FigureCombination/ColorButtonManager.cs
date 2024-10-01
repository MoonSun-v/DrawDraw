using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButtonManager : MonoBehaviour
{
    private ColorButtonMover[] colorButtonMovers;  // ��� ��ĥ ��ư�� �����ϱ� ���� �迭
    public Color selectedColor; // ���õ� ����
    private int shapeLayer; // "Shape" ���̾��� �ε����� ����

    public bool isActive = false; // Ȱ��ȭ ����

    // ������ ������ ���� ������ ������ ����
    private int changedShapeCount = 0;

    // ���� ���� ���θ� �����ϴ� Dictionary
    private Dictionary<GameObject, bool> colorChangedMap = new Dictionary<GameObject, bool>();

    void Start()
    {
        // ��� ColorButtonMover�� ã���ϴ�.
        colorButtonMovers = FindObjectsOfType<ColorButtonMover>();

        // "Shape" ���̾��� �ε����� �����ɴϴ�.
        shapeLayer = LayerMask.NameToLayer("shape");
        isActive = true;  // ��ư�� �������� ǥ��

        // Dictionary �ʱ�ȭ
        //colorChangedMap = new System.Collections.Generic.Dictionary<GameObject, bool>();

    }

    // Update is called once per frame
    void Update()
    {
        // ��ư�� Ŭ���� �Ŀ��� �� �ڵ尡 �����
        if (isActive && Input.GetMouseButtonDown(0))
        {
            ExecuteRaycast();
        }
    }

    // Raycast ���� ������ �� �Լ��� �и�
    void ExecuteRaycast()
    {
        Vector3 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rayOrigin.z = 0f; // 2D������ z ���� 0���� ���� (z ���� ������� ����)

        // "shape" ���̾ �ش��ϴ� ���̾� ����ũ ����
        int layerMask = 1 << LayerMask.NameToLayer("shape");

        // Raycast�� Ư�� ���̾�� ����
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layerMask);

        if (hit.collider != null)
        {
                SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && selectedColor != new Color(0, 0, 0, 0))
                {
                    // ������ �� ���� ������� �ʾҴٸ� ������ �����ϰ� ī���� ����
                    if (!colorChangedMap.ContainsKey(hit.transform.gameObject))
                    {
                        // ó�� �����ϴ� ���
                        spriteRenderer.color = selectedColor;

                        // ������ �������� ǥ���ϰ� ī���� ����
                        colorChangedMap[hit.transform.gameObject] = true;
                        changedShapeCount++;
                        //Debug.Log($"������ ����� ���� ����: {changedShapeCount}");
                    }
                    else
                    {
                        // �̹� ������ ����� ���� �ִ� ��쿡�� ���� ����
                        spriteRenderer.color = selectedColor;
                        //Debug.Log($"{hit.transform.name}�� ������ {selectedColor}�� �ٽ� ����Ǿ����ϴ�.");
                    }

                }
            //}
        }
        else
        {
            //Debug.Log("�浹�� ������Ʈ�� �����ϴ�.");
        }
    }

    public void OnButtonClicked(ColorButtonMover clickedButton)
    {
        // Ŭ���� ��ư�� ������ ��� ��ư�� �̹����� ���� �̹����� �ǵ����ϴ�.
        foreach (ColorButtonMover buttonMover in colorButtonMovers)
        {
            if (buttonMover != clickedButton)
            {
                buttonMover.ResetImage();  // ��ġ�� �����ϴ� ��� �̹����� �����ϴ� �޼��带 ȣ���մϴ�.
            }
        }
    }

    public void SetSelectedButtonID(int buttonID)
    {
        // Ŭ���� ��ư�� ID�� ���� ���� �ڵ带 �����մϴ�.
        string colorCode = "#FFFFFF"; // �⺻ ���� (���)

        // Ŭ���� ��ư�� ID�� ���� ������ �����մϴ�.
        switch (buttonID)
        {
            case 1:
                colorCode = "#E30204";
                break;
            case 2:
                colorCode = "#F0870C";
                break;
            case 3:
                colorCode = "#F1D712";
                break;
            case 4:
                colorCode = "#3B9C00";
                break;
            case 5:
                colorCode = "#0085FE";
                break;
            case 6:
                colorCode = "#2E33D7";
                break;
            case 7:
                colorCode = "#5A10BB";
                break;
            // �߰� ��ư�� ���� ������ ������ �� �ֽ��ϴ�.
            default:

                break;
        }
        // HEX ���� �ڵ带 Color�� ��ȯ�մϴ�.
        if (ColorUtility.TryParseHtmlString(colorCode, out selectedColor))
        {

        }
        else
        {
            Debug.LogError("���� �ڵ� ��ȯ�� �����߽��ϴ�.");
        }
    }

    // Getter �޼���: changedShapeCount ���� �ٸ� ��ũ��Ʈ���� ������ �� ����
    public int GetChangedShapeCount()
    {
        return changedShapeCount;
    }
}
