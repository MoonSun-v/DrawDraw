using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColor : MonoBehaviour
{
    public GameObject LineArea;
    public GameObject ColorArea;

    public GameObject baseSquare;
    public GameObject baseSquare1;
    public GameObject baseSquare2;

    public GameObject checkPopup;

    private bool color = false;
    public CheckpopupManager CheckpopupManager;

    public void OnClick_color() // 확인창 완성 버튼을 클릭 -> 결과 보여주기
    {
        if (color)
        {
            CheckpopupManager.OnClick_finish();
        }
        else
        {
            LineArea.SetActive(false);
            ColorArea.SetActive(true);

            baseSquare.transform.position += Vector3.left * 1.5f;
            baseSquare1.SetActive(false);
            baseSquare2.SetActive(false);

            MoveClones();

            color = true;
        }

        checkPopup.SetActive(false);
    }

    // 생성된 모든 클론을 이동시키는 함수
    void MoveClones()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("LineBrush"); // 태그로 프리팹 찾기

        foreach (GameObject clone in clones)
        {
            clone.transform.position += Vector3.left * 1.5f;
        }
    }  


}
