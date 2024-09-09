using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButtonManager : MonoBehaviour
{
    private ColorButtonMover[] colorButtonMovers;  // ��� ��ĥ ��ư�� �����ϱ� ���� �迭
    private Color selectedColor; // ���õ� ����
    private int shapeLayer; // "Shape" ���̾��� �ε����� ����

    private bool isActive = false; // Ȱ��ȭ ����

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
            Debug.Log($"���õ� ������ �̸�: {hit.transform.name}");

            // �θ� ������Ʈ �������� (�ڽ��� �ֻ����� �θ� null�� �� ����)
            Transform parent = hit.transform.parent;

            if (parent != null) // �θ� �ִ� ���
            {
                // �θ� �Ʒ��� ��� �ڽ� ������Ʈ ��ȸ
                foreach (Transform sibling in parent)
                {
                    SpriteRenderer siblingRenderer = sibling.GetComponent<SpriteRenderer>();

                    // �ڽĿ� SpriteRenderer�� ������ ���� ����
                    if (siblingRenderer != null && selectedColor != new Color(0, 0, 0, 0))
                    {
                        // ������ �� ���̶� ����� ���� �ִ��� Ȯ��
                        if (!colorChangedMap.ContainsKey(sibling.gameObject) || !colorChangedMap[sibling.gameObject])
                        {
                            // ������ �� ���� ������� �ʾҴٸ� ������ ����
                            siblingRenderer.color = selectedColor;
                            Debug.Log($"{sibling.name}�� ������ {selectedColor}�� ����Ǿ����ϴ�.");

                            // ������ �������� ǥ���ϰ� ī���� ����
                            colorChangedMap[sibling.gameObject] = true;
                            changedShapeCount++;
                            Debug.Log($"������ ����� ���� ����: {changedShapeCount}");
                        }
                        else
                        {
                            Debug.Log($"{sibling.name}�� ������ �̹� ����� ���� �ֽ��ϴ�.");
                        }
                    }
                }
            }
            else // �θ� ���� ��� (�ֻ��� ������Ʈ)
            {
                SpriteRenderer spriteRenderer = hit.transform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null && selectedColor != new Color(0, 0, 0, 0))
                {
                    // ������ �� ���� ������� �ʾҴٸ� ������ ����
                    if (!colorChangedMap.ContainsKey(hit.transform.gameObject) || !colorChangedMap[hit.transform.gameObject])
                    {
                        spriteRenderer.color = selectedColor;
                        Debug.Log($"{hit.transform.name}�� ������ {selectedColor}�� ����Ǿ����ϴ�.");

                        // ������ �������� ǥ���ϰ� ī���� ����
                        colorChangedMap[hit.transform.gameObject] = true;
                        changedShapeCount++;
                        Debug.Log($"������ ����� ���� ����: {changedShapeCount}");
                    }
                    else
                    {
                        Debug.Log($"{hit.transform.name}�� ������ �̹� ����� ���� �ֽ��ϴ�.");
                    }

                }
            }
        }
        else
        {
            //Debug.Log("�浹�� ������Ʈ�� �����ϴ�.");
        }
    }

    public void OnButtonClicked(ColorButtonMover clickedButton)
    {
        // Ŭ���� ��ư�� ������ ��� ��ư�� ��ġ�� �����մϴ�.
        foreach (ColorButtonMover buttonMover in colorButtonMovers)
        {
            if (buttonMover != clickedButton)
            {
                buttonMover.ResetPosition();
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
                colorCode = "#F9DC00";
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
                colorCode = "#DB7093";
                break;
            // �߰� ��ư�� ���� ������ ������ �� �ֽ��ϴ�.
            default:

                break;
        }
        // HEX ���� �ڵ带 Color�� ��ȯ�մϴ�.
        if (ColorUtility.TryParseHtmlString(colorCode, out selectedColor))
        {
            Debug.Log($"������ {colorCode}�� �����Ǿ����ϴ�.");
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
