using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{
    public int segments = 100;  // 세그먼트의 수, 값이 클수록 더 매끄러운 원이 됩니다.
    public float xRadius = 5;   // X축 반지름
    public float yRadius = 5;   // Y축 반지름
    public Color gizmoColor = Color.green;  // Gizmo 색상

    private LineRenderer line;
    // 초기화 및 LineRenderer 설정
    void Start()
    {
        // 현재 게임 오브젝트에 있는 LineRenderer 컴포넌트를 가져옴
        line = gameObject.GetComponent<LineRenderer>();

        // LineRenderer의 점 개수 설정
        line.positionCount = segments + 1;

        // 월드 공간을 기준으로 그릴지 여부 (false로 설정하여 로컬 공간 기준으로 그림)
        line.useWorldSpace = false;

        // 원형 점들을 생성
        CreatePoints();
    }

    // 원형을 그릴 점들을 생성하는 함수
    void CreatePoints()
    {
        // 각 점의 각도 (도 단위)
        float angle = 10f;

        // 세그먼트 수만큼 점을 생성하여 LineRenderer에 설정
        for (int i = 0; i < (segments + 1); i++)
        {
            // 각도에 따른 X, Y 좌표 계산
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;

            // LineRenderer에 좌표 설정
            line.SetPosition(i, new Vector3(x, y, 0));

            // 다음 점을 위한 각도 증가
            angle += (360f / segments);
        }
    }

    // 에디터 모드에서 원형을 그리는 함수 (Gizmos 사용)
    void OnDrawGizmos()
    {
        // Gizmo의 색상 설정
        Gizmos.color = gizmoColor;

        // 각 점의 각도 (도 단위)
        float angle = 10f;

        // 첫 번째 점과 마지막 점을 저장할 변수
        Vector3 firstPoint = Vector3.zero;
        Vector3 lastPoint = Vector3.zero;

        // 세그먼트 수만큼 점을 생성하여 Gizmo에 원형을 그림
        for (int i = 0; i < segments + 1; i++)
        {
            // 각도에 따른 X, Y 좌표 계산 및 게임 오브젝트의 위치를 더함
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;
            Vector3 point = new Vector3(x, y, 0) + transform.position; // 게임 오브젝트의 위치를 반영

            // 첫 점 이후부터는 이전 점과 현재 점을 연결하는 선을 그림
            if (i > 0)
            {
                Gizmos.DrawLine(lastPoint, point);
            }
            else
            {
                // 첫 번째 점 저장
                firstPoint = point;
            }

            // 마지막 점 업데이트
            lastPoint = point;

            // 다음 점을 위한 각도 증가
            angle += (360f / segments);
        }

        // 마지막 점과 첫 번째 점을 연결하여 원을 완성
        Gizmos.DrawLine(lastPoint, firstPoint);
    }
}