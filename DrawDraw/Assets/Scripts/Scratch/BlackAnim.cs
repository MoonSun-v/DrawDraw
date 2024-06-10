using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAnim : MonoBehaviour
{
    public GameObject[] lineObjects; // 라인 렌더러를 가진 오브젝트 배열
    public float drawSpeed = 1f; // 선 그리는 속도

    void Start()
    {
        StartCoroutine(ActivateAndDrawLines());
    }

    // 오브젝트를 순서대로 활성화 -> 선 그리는 : 코루틴
    IEnumerator ActivateAndDrawLines()
    {
        foreach (var obj in lineObjects)
        {
            obj.SetActive(true); // 활성화

            LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
            yield return StartCoroutine(DrawLine(lineRenderer)); // 선그리는 코루틴 시작 
        }
    }

    // 라인 렌더러로 선 그리는 코루틴
    IEnumerator DrawLine(LineRenderer lineRenderer)
    {
        // 0번째 1번째 점 위치 가져오기 
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        // 경과 시간 
        float elapsedTime = 0f;

        // 선 그리는 동안 반복 실행
        while (elapsedTime < drawSpeed)
        {
            elapsedTime += Time.deltaTime; // 시간 체크 

            // 시간 비율 계산 
            float t = Mathf.Clamp01(elapsedTime / drawSpeed);

            // 두번째 점 위치 선형 보간 
            // Vector3.Lerp : 두 점 사이를 선형 보간 
            lineRenderer.SetPosition(1, Vector3.Lerp(startPoint, endPoint, t));
            
            // 한 프레임 끝날 때 까지 대기 
            yield return null; 
        }

        // 선을 다 그린 후 두 번째 점의 위치 최종 설정
        lineRenderer.SetPosition(1, endPoint);
    }
}
