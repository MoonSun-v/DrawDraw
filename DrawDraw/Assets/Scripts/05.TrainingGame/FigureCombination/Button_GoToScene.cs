using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_GoToScene : MonoBehaviour
{
    // ����
    public void ChangeScene_map()
    {
        SceneManager.LoadScene("MapScene");
        Debug.Log("�� ȭ������ �̵��մϴ�.");
    }

    // 1�ܰ�
    public void ChangeScene_Sun()
    {
        SceneManager.LoadScene("1Sun");
        Debug.Log("�� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_Pinwheel()
    {
        SceneManager.LoadScene("1Pinwheel");
        Debug.Log("�ٶ����� ���� ������ �����մϴ�.");
    }

    //2�ܰ�
    public void ChangeScene_Rocket()
    {
        SceneManager.LoadScene("2Rocket");
        Debug.Log("���� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_Ship()
    {
        SceneManager.LoadScene("2Ship");
        Debug.Log("�� ���� ������ �����մϴ�.");
    }

    //3�ܰ�
    public void ChangeScene_Person()
    {
        SceneManager.LoadScene("3Person");
        Debug.Log("��� ���� ������ �����մϴ�.");
    }

    public void ChangeScene_TheTrain()
    {
        SceneManager.LoadScene("3TheTrain");
        Debug.Log("���� ���� ������ �����մϴ�.");
    }

    //reset
    public GameObject[] shapePrefabs; // ������ �迭
    public GameObject FinishButton; // �Ϸ� ��ư ����

    // �����պ� �ʱ� �÷� �ڵ� ����
    private Dictionary<GameObject, string> prefabOriginalColorCodes = new Dictionary<GameObject, string>();


    void Start()
    {
        // �� �������� �ʱ� �÷� �ڵ带 ����
        foreach (GameObject prefab in shapePrefabs)
        {
            if (prefab != null)
            {
                SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Color ��ü�� HEX �÷� �ڵ�� ��ȯ�Ͽ� ����
                    string colorCode = "#" + ColorUtility.ToHtmlStringRGB(spriteRenderer.color);
                    prefabOriginalColorCodes[prefab] = colorCode;
                }
            }
        }
    }

    public void OnRestartButtonClick()
    {

        if (FinishButton != null && FinishButton.activeSelf)
        {
            ResetClonedShapesColor();
        }

        else
        {
            // ���� Ȱ��ȭ�� ���� �ٽ� �ε�
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("ó������ �ٽ� �����մϴ�.");

        }
    }
    private void ResetClonedShapesColor()
    {
        // 'shape' �±׸� ���� ��� ������Ʈ�� ã��
        GameObject[] clonedShapes = GameObject.FindGameObjectsWithTag("shape");

        foreach (GameObject shape in clonedShapes)
        {
            SpriteRenderer spriteRenderer = shape.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // �����հ� ������ �ʱ� �÷� �ڵ带 ã�� ����
                foreach (var prefab in shapePrefabs)
                {
                    if (prefab != null && shape.name.StartsWith(prefab.name))
                    {
                        if (prefabOriginalColorCodes.TryGetValue(prefab, out string colorCode))
                        {
                            // ��������� UnityEngine.Color�� ���
                            if (UnityEngine.ColorUtility.TryParseHtmlString(colorCode, out UnityEngine.Color color))
                            {
                                spriteRenderer.color = color;
                            }
                        }
                        break; // ��ġ�ϴ� �������� ã������ �� �̻� �ݺ��� �ʿ� ����
                    }
                }
            }
        }

        //Debug.Log("��� 'shape' �±� ������Ʈ�� ������ �ʱ� ���·� �ǵ��ư����ϴ�.");
    }
}
