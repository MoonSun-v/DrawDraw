using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{// 라인 렌더러 컴포넌트 참조
    private LineRenderer lineRenderer;

    // 충돌 여부를 저장할 변수
    private bool collided = false;

    // 충돌 횟수를 저장할 변수
    private int collisionCount = 0;

    // 완성 버튼
    public Button completeButton;

    private void Start()
    {
        // 라인 렌더러 컴포넌트 가져오기
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        // 이미 충돌한 경우 더 이상 충돌 검사를 하지 않음
        if (collided)
            return;

        // 라인 렌더러의 시작점과 끝점 가져오기
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

        // 라인과 태그가 "baseSquare"인 오브젝트 간의 충돌 검사
        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint);

        // 충돌이 발생했는지 확인
        if (hit.collider != null && hit.collider.CompareTag("baseSquare"))
        {
            // 충돌한 오브젝트의 이름을 디버그 출력
            Debug.Log("충돌한 오브젝트: " + hit.collider.gameObject.name);

            // 충돌했음을 표시
            collided = true;

            // 충돌 횟수 증가
            collisionCount++;

        }
        else
        {
            // 사각형 영역을 벗어났으므로 충돌했음을 초기화
            collided = false;
        }
    }

    // 완성 버튼 클릭 시 호출되는 함수
    public void ShowCollisionCount()
    {
        // 충돌 횟수 출력
        Debug.Log("충돌 횟수: " + collisionCount);
    }
}
