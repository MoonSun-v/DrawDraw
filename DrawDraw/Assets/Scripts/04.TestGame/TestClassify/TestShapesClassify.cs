using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestShapesClassify : MonoBehaviour
{
    public GameObject[] Shapes;
    public GameObject[] AnswerShapes;

    public GameObject CheckPopup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        SceneManager.LoadScene("Test_ColorClassifyScene");
    }

    private void CheckAnswer()
    {
        int score = 0;  // ������ 0���� �ʱ�ȭ

        for (int i = 0; i < Shapes.Length; i++)
        {
            if (i >= AnswerShapes.Length)
            {
                Debug.LogWarning("AnswerShapes �迭�� ��Ұ� �����մϴ�.");
                return;
            }

            BoxCollider2D answerCollider = AnswerShapes[i].GetComponent<BoxCollider2D>();
            if (answerCollider == null)
            {
                Debug.LogWarning("AnswerShapes[" + i + "]�� BoxCollider2D ������Ʈ�� �����ϴ�.");
                return;
            }

            if (IsWithinCollider(Shapes[i], answerCollider))
            {
                Debug.Log("Shape " + i + "��(��) �ùٸ� �ڽ� �ݶ��̴� �ȿ� �ֽ��ϴ�.");
                score++;  // �ùٸ��� ��ġ�� �������� 1�� �߰�
            }
            else
            {
                Debug.Log("Shape " + i + "��(��) �ùٸ� �ڽ� �ݶ��̴� �ȿ� �����ϴ�.");
            }
        }

        Debug.Log("���� ����: " + score);  // ���� ���� ���
    }

    private bool IsWithinCollider(GameObject shape, BoxCollider2D collider)
    {
        Bounds shapeBounds = shape.GetComponent<Collider2D>().bounds;
        return collider.bounds.Intersects(shapeBounds);
    }

}
