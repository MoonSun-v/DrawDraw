using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotLineManager : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 MousePosition;

    DotScore dotscore = new DotScore();

    private void Awake()
    {
        // "Maincamera" 태그를 가지고 있는 오브젝트 담색 후 Camera 컴포넌트 정보 전달
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 누르고 있는 동안 
        if (Input.GetMouseButton(0)) 
        {
            // 마우스 클릭 위치 값 -> 월드 좌표 위치 값 
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition); 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit) // 충돌이 있다면 
            {
                // 해당 오브젝트 콜리더 비활성화 (충돌 1번만 발생시키기 위해)
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;

                dotscore.DotCount += 1f;
                // print("충돌한 점의 개수 = " + dotscore.DotCount);
            }

        }
    }

    // 완성 버튼 클릭 시, 총 점수 계산
    public void DotScore()
    {
        print("충돌한 점의 개수 = " + dotscore.DotCount);
        dotscore.Score = (int) (( dotscore.DotCount / 30 ) * 100) ; // 소수점 이하는 내림 
        print("퍼센트 = " + dotscore.Score + "%");

    }
}

public class DotScore
{
    // 점의 총 개수 : 30개

    // 충돌한 점의 개수
    public float DotCount;

    // 총 점수
    public int Score;
}