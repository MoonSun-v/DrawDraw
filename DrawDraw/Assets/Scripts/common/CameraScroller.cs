using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 카메라 스크롤링 기능 구현

public class CameraScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    // 카메라 이동 범위 제한하는 y축 값 
    public float minY;
    public float maxY;

    private Vector3 touchStart;  

    void Update()
    {
        // 입력 감지 되었을 때 : 마우스 위치 -> 월드 좌표 변환
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // 클릭 상태에서 마우스 이동 시, 카메라를 이동 한다.
        // ( ★ 최종 맵 배경 GUI 제작 완료 시 -> minY, maxY 조절 필요 )
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += new Vector3(0, direction.y * scrollSpeed, 0);

            // 이동 위치 제한
            float clampedY = Mathf.Clamp(Camera.main.transform.position.y, minY, maxY);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, clampedY, Camera.main.transform.position.z);
        }
    }
}