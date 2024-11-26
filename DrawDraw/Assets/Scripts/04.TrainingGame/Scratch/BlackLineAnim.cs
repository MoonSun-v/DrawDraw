using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLineAnim : MonoBehaviour
{

    [SerializeField] 
    private float animationDuration = 4f;   // 애니메이션 지속 시간

    private LineRenderer lineRenderer;
    private int pointsCount;                // 라인 점 개수
    private Vector3[] linePoints;           // 라인의 점 위치 저장할 배열



    // [ 여러 초기화 작업 ]
    // 
    // - 라인렌더러 점 개수 및 배열 초기화
    // - lineRenderer의 점의 위치 -> linePoints배열 저장
    // - 선 그리는 애니메이션 코루틴 시작 
    //
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        pointsCount = lineRenderer.positionCount;      
        linePoints = new Vector3[pointsCount];         

        for (int i = 0; i < pointsCount; i++)          
        {
            linePoints[i] = lineRenderer.GetPosition(i);
        }

        StartCoroutine(AnimateLine());                
    }



    // ★ [ 라인의 점을 애니메이션으로 그리는 코루틴 ]
    //
    // segmentDuration : 각 세그먼트(점 간의 구간) 애니메이션의 지속 시간 계산
    // 
    // 1. 시간 비율에 따라 두 점 사이를 선형 보간
    // 2. i번째 점 이후의 모든 점을 현재 pos로 설정하여 애니메이션 효과 적용
    // 
    private IEnumerator AnimateLine()
    {
        float segmentDuration = animationDuration / pointsCount;

        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;                 // 현재 시간을 시작 시간으로 설정

            Vector3 startPosition = linePoints[i];       // 현재 세그먼트의 시작 위치
            Vector3 endPosition = linePoints[i + 1];     // 현재 세그먼트의 종료 위치

            Vector3 pos = startPosition;                 // 현재 위치를 시작 위치로 
            while (pos != endPosition)
            {
                float t = (Time.time - startTime) / segmentDuration;  // 1
                pos = Vector3.Lerp(startPosition, endPosition, t);

                for (int j = i + 1; j < pointsCount; j++)             // 2
                {
                    lineRenderer.SetPosition(j, pos);
                }
                    
                yield return null;
            }
        }
    }
}
