using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotLineManager : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 MousePosition;

    private void Awake()
    {
        // "Maincamera" �±׸� ������ �ִ� ������Ʈ ��� �� Camera ������Ʈ ���� ����
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ������ �ִ� ���� 
        if (Input.GetMouseButton(0)) 
        {
            // ���콺 Ŭ�� ��ġ �� -> ���� ��ǥ ��ġ �� 
            MousePosition= mainCamera.ScreenToWorldPoint(Input.mousePosition); 

            RaycastHit2D hit = Physics2D.Raycast(MousePosition, transform.forward, 15f);
            if (hit) // �浹�� �ִٸ� 
            {
                // �ش� ������Ʈ �ݸ��� ��Ȱ��ȭ (�浹 1���� �߻���Ű�� ����)
                hit.transform.GetComponent<CircleCollider2D>().enabled = false;
                print("���� �浹�߽��ϴ�.");
            }

        }
    }
}
