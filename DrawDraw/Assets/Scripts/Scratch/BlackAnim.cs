using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAnim : MonoBehaviour
{
    public GameObject[] lineObjects; // ���� �������� ���� ������Ʈ �迭
    public float drawSpeed = 1f; // �� �׸��� �ӵ�

    void Start()
    {
        StartCoroutine(ActivateAndDrawLines());
    }

    // ������Ʈ�� ������� Ȱ��ȭ -> �� �׸��� : �ڷ�ƾ
    IEnumerator ActivateAndDrawLines()
    {
        foreach (var obj in lineObjects)
        {
            obj.SetActive(true); // Ȱ��ȭ

            LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
            yield return StartCoroutine(DrawLine(lineRenderer)); // ���׸��� �ڷ�ƾ ���� 
        }
    }

    // ���� �������� �� �׸��� �ڷ�ƾ
    IEnumerator DrawLine(LineRenderer lineRenderer)
    {
        // 0��° 1��° �� ��ġ �������� 
        Vector3 startPoint = lineRenderer.GetPosition(0);
        Vector3 endPoint = lineRenderer.GetPosition(1);

        // ��� �ð� 
        float elapsedTime = 0f;

        // �� �׸��� ���� �ݺ� ����
        while (elapsedTime < drawSpeed)
        {
            elapsedTime += Time.deltaTime; // �ð� üũ 

            // �ð� ���� ��� 
            float t = Mathf.Clamp01(elapsedTime / drawSpeed);

            // �ι�° �� ��ġ ���� ���� 
            // Vector3.Lerp : �� �� ���̸� ���� ���� 
            lineRenderer.SetPosition(1, Vector3.Lerp(startPoint, endPoint, t));
            
            // �� ������ ���� �� ���� ��� 
            yield return null; 
        }

        // ���� �� �׸� �� �� ��° ���� ��ġ ���� ����
        lineRenderer.SetPosition(1, endPoint);
    }
}
