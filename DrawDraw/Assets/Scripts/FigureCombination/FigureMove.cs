using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FigureMove : MonoBehaviour
{
    public GameObject prefabToInstantiate; // �巡�� �� ������ ������Ʈ ������
    private GameObject instantiatedObject;
    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ������Ʈ�� �����ؿ�.
        Debug.Log("�巡�� ����");
        instantiatedObject = Instantiate(prefabToInstantiate, canvas.transform);
        instantiatedObject.transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ������Ʈ�� ���콺 ��ġ�� �̵��ؿ�.
        Debug.Log("�巡�� ��");
        if (instantiatedObject != null)
        {
            instantiatedObject.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("�巡�� ��");
        // �巡�װ� ������ �� �߰����� ó���� �ؿ�.
        instantiatedObject = null;
    }
}