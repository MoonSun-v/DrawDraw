using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    public float minY;
    public float maxY;

    private Vector3 touchStart;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += new Vector3(0, direction.y * scrollSpeed, 0);

            float clampedY = Mathf.Clamp(Camera.main.transform.position.y, minY, maxY);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, clampedY, Camera.main.transform.position.z);
        }
    }
}