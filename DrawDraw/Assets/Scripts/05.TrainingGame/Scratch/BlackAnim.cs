using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAnim : MonoBehaviour
{

    public GameObject[] lineObjects; // ���� �������� ���� ������Ʈ �迭
    public float drawSpeed = 1f;     // �� �׸��� �ӵ�


    void Start()
    {
        StartCoroutine(ActivateAndDrawLines());
    }


    // �� [ ������Ʈ ������� Ȱ��ȭ -> �� �׸��� �ڷ�ƾ ]
    //  
    IEnumerator ActivateAndDrawLines()
    {
        foreach (var obj in lineObjects)
        {
            obj.SetActive(true); 

            LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
            yield return StartCoroutine(DrawLine(lineRenderer)); 
        }
    }


    // �� [ ���� �������� �� �׸��� �ڷ�ƾ ]
    // 
    // 1. 0��° 1��° �� ��ġ ��������, ��� �ð� �ʱ�ȭ 
    // 2. �� �׸��� ���� �ݺ� ����
    //    - ��� �ð� ������Ʈ -> �ð� ���� ���
    //    - �ι�° �� ��ġ ���� ���� (Vector3.Lerp : �� �� ���̸� ���� ���� )
    //    - �� ������ ��� 
    // 3. �� �� �׸� ��, �� ��° ���� ��ġ ���� ����
    // 
    IEnumerator DrawLine(LineRenderer lineRenderer)
    {
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);
        float elapsedTime = 0f;

        while (elapsedTime < drawSpeed)
        {
            elapsedTime += Time.deltaTime; 
            float t = Mathf.Clamp01(elapsedTime / drawSpeed);

            lineRenderer.SetPosition(1, Vector3.Lerp(startPoint, endPoint, t));
            
            yield return null; 
        }

        lineRenderer.SetPosition(1, endPoint);
    }
}
