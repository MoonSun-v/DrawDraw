using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLineAnim : MonoBehaviour
{

    [SerializeField] 
    private float animationDuration = 4f;   // �ִϸ��̼� ���� �ð�

    private LineRenderer lineRenderer;
    private int pointsCount;                // ���� �� ����
    private Vector3[] linePoints;           // ������ �� ��ġ ������ �迭



    // [ ���� �ʱ�ȭ �۾� ]
    // 
    // - ���η����� �� ���� �� �迭 �ʱ�ȭ
    // - lineRenderer�� ���� ��ġ -> linePoints�迭 ����
    // - �� �׸��� �ִϸ��̼� �ڷ�ƾ ���� 
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



    // �� [ ������ ���� �ִϸ��̼����� �׸��� �ڷ�ƾ ]
    //
    // segmentDuration : �� ���׸�Ʈ(�� ���� ����) �ִϸ��̼��� ���� �ð� ���
    // 
    // 1. �ð� ������ ���� �� �� ���̸� ���� ����
    // 2. i��° �� ������ ��� ���� ���� pos�� �����Ͽ� �ִϸ��̼� ȿ�� ����
    // 
    private IEnumerator AnimateLine()
    {
        float segmentDuration = animationDuration / pointsCount;

        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;                 // ���� �ð��� ���� �ð����� ����

            Vector3 startPosition = linePoints[i];       // ���� ���׸�Ʈ�� ���� ��ġ
            Vector3 endPosition = linePoints[i + 1];     // ���� ���׸�Ʈ�� ���� ��ġ

            Vector3 pos = startPosition;                 // ���� ��ġ�� ���� ��ġ�� 
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
