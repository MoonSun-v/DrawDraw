using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ī�޶� ��ũ�Ѹ� ��� ����

public class CameraScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    // ī�޶� �̵� ���� �����ϴ� y�� �� 
    public float minY;
    public float maxY;

    private Vector3 touchStart;  

    void Update()
    {
        // �Է� ���� �Ǿ��� �� : ���콺 ��ġ -> ���� ��ǥ ��ȯ
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Ŭ�� ���¿��� ���콺 �̵� ��, ī�޶� �̵� �Ѵ�.
        // ( �� ���� �� ��� GUI ���� �Ϸ� �� -> minY, maxY ���� �ʿ� )
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += new Vector3(0, direction.y * scrollSpeed, 0);

            // �̵� ��ġ ����
            float clampedY = Mathf.Clamp(Camera.main.transform.position.y, minY, maxY);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, clampedY, Camera.main.transform.position.z);
        }
    }
}