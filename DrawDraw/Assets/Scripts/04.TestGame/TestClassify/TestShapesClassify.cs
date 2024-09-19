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
        SceneManager.LoadScene("Test_ColorClassifyScene");
    }

    private void CheckAnswer()
    {
        int score = 0;  // 점수를 0으로 초기화

        for (int i = 0; i < Shapes.Length; i++)
        {
            if (i >= AnswerShapes.Length)
            {
                Debug.LogWarning("AnswerShapes 배열의 요소가 부족합니다.");
                return;
            }

            BoxCollider2D answerCollider = AnswerShapes[i].GetComponent<BoxCollider2D>();
            if (answerCollider == null)
            {
                Debug.LogWarning("AnswerShapes[" + i + "]에 BoxCollider2D 컴포넌트가 없습니다.");
                return;
            }

            if (IsWithinCollider(Shapes[i], answerCollider))
            {
                Debug.Log("Shape " + i + "이(가) 올바른 박스 콜라이더 안에 있습니다.");
                score++;  // 올바르게 위치한 도형마다 1점 추가
            }
            else
            {
                Debug.Log("Shape " + i + "이(가) 올바른 박스 콜라이더 안에 없습니다.");
            }
        }

        Debug.Log("최종 점수: " + score);  // 최종 점수 출력
    }

    private bool IsWithinCollider(GameObject shape, BoxCollider2D collider)
    {
        Bounds shapeBounds = shape.GetComponent<Collider2D>().bounds;
        return collider.bounds.Intersects(shapeBounds);
    }

}
