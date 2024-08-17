using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLineAnim : MonoBehaviour
{
    [SerializeField] private float animationDuration = 4f;   // �ִϸ��̼� ���� �ð�

    private LineRenderer lineRenderer;
    private Vector3[] linePoints;                            // ������ �� ��ġ ������ �迭
    private int pointsCount;                                 // ���� �� ����



    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        pointsCount = lineRenderer.positionCount;      // �� ����
        linePoints = new Vector3[pointsCount];         // �迭 �ʱ�ȭ 

        for (int i = 0; i < pointsCount; i++)          // lineRenderer�� ���� ��ġ -> linePoints�迭 ����
        {
            linePoints[i] = lineRenderer.GetPosition(i);
        }

        StartCoroutine(AnimateLine());                 // ���׸��� �ִϸ��̼��Լ� ���� : �ڷ�ƾ 
    }



    // ������ ���� �ִϸ��̼����� �׸��� �ڷ�ƾ
    private IEnumerator AnimateLine()
    {
        // �� ���׸�Ʈ(�� ���� ����) �ִϸ��̼��� ���� �ð� ���
        float segmentDuration = animationDuration / pointsCount;


        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;                 // ���� �ð��� ���� �ð����� 

            Vector3 startPosition = linePoints[i];       // ���� ��ġ
            Vector3 endPosition = linePoints[i + 1];     // ���� ��ġ


            Vector3 pos = startPosition;                 // ���� ��ġ�� ���� ��ġ�� 
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
