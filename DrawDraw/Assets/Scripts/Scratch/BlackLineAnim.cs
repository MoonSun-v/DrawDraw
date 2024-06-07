using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLineAnim : MonoBehaviour
{
    // �ִϸ��̼� ���� �ð�
    [SerializeField] private float animationDuration = 4f;

    private LineRenderer lineRenderer;
    private Vector3[] linePoints; // // ������ �� ��ġ ������ �迭
    private int pointsCount; // ���� �� ����

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        pointsCount = lineRenderer.positionCount; // �� ����
        linePoints = new Vector3[pointsCount]; // �迭 �ʱ�ȭ 

        // lineRenderer�� ���� ��ġ -> linePoints�迭 ����
        for (int i = 0; i < pointsCount; i++)
        {
            linePoints[i] = lineRenderer.GetPosition(i);
        }

        // ���׸��� �ִϸ��̼��Լ� ���� : �ڷ�ƾ 
        StartCoroutine(AnimateLine());
    }

    // ������ ���� �ִϸ��̼����� �׸��� �ڷ�ƾ
    private IEnumerator AnimateLine()
    {
        // �� ���׸�Ʈ(�� ���� ����) �ִϸ��̼��� ���� �ð� ���
        float segmentDuration = animationDuration / pointsCount;

        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time; // ���� �ð��� ���� �ð����� 

            Vector3 startPosition = linePoints[i]; // ���� ��ġ
            Vector3 endPosition = linePoints[i + 1]; // ���� ��ġ

            Vector3 pos = startPosition; // ���� ��ġ�� ���� ��ġ�� 
            while (pos != endPosition)
            {
                // t ������ ���� ����
                float t = (Time.time - startTime) / segmentDuration;
                pos = Vector3.Lerp(startPosition, endPosition, t);

                // i��° �� ������ ��� ���� ���� pos�� ������, �ִϸ��̼� ȿ��
                for (int j = i + 1; j < pointsCount; j++)
                {
                    lineRenderer.SetPosition(j, pos);
                }
                    
                yield return null;
            }
        }
    }
}
