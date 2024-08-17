using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLineAnim : MonoBehaviour
{
    [SerializeField] private float animationDuration = 4f;   // 애니메이션 지속 시간

    private LineRenderer lineRenderer;
    private Vector3[] linePoints;                            // 라인의 점 위치 저장할 배열
    private int pointsCount;                                 // 라인 점 개수



    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        pointsCount = lineRenderer.positionCount;      // 점 개수
        linePoints = new Vector3[pointsCount];         // 배열 초기화 

        for (int i = 0; i < pointsCount; i++)          // lineRenderer의 점의 위치 -> linePoints배열 저장
        {
            linePoints[i] = lineRenderer.GetPosition(i);
        }

        StartCoroutine(AnimateLine());                 // 선그리는 애니메이션함수 시작 : 코루틴 
    }



    // 라인의 점을 애니메이션으로 그리는 코루틴
    private IEnumerator AnimateLine()
    {
        // 각 세그먼트(점 간의 구간) 애니메이션의 지속 시간 계산
        float segmentDuration = animationDuration / pointsCount;


        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;                 // 현재 시간을 시작 시간으로 

            Vector3 startPosition = linePoints[i];       // 시작 위치
            Vector3 endPosition = linePoints[i + 1];     // 종료 위치


            Vector3 pos = startPosition;                 // 현재 위치를 시작 위치로 
            while (pos != endPosition)
            {
                // t 값으로 선형 보간
                float t = (Time.time - startTime) / segmentDuration;
                pos = Vector3.Lerp(startPosition, endPosition, t);


                // i번째 점 이후의 모든 점을 현재 pos로 설정해, 애니메이션 효과
                for (int j = i + 1; j < pointsCount; j++)
                {
                    lineRenderer.SetPosition(j, pos);
                }
                    
                yield return null;
            }
        }
    }
}
