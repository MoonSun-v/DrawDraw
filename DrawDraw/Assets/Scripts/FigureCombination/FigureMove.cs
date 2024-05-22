using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FigureMove : MonoBehaviour
{
    public GameObject prefabToInstantiate; // 드래그 시 생성할 오브젝트 프리팹
    private GameObject instantiatedObject;
    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 오브젝트를 생성해요.
        Debug.Log("드래그 시작");
        instantiatedObject = Instantiate(prefabToInstantiate, canvas.transform);
        instantiatedObject.transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중일 때 오브젝트를 마우스 위치로 이동해요.
        Debug.Log("드래그 중");
        if (instantiatedObject != null)
        {
            instantiatedObject.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 끝");
        // 드래그가 끝났을 때 추가적인 처리를 해요.
        instantiatedObject = null;
    }
}