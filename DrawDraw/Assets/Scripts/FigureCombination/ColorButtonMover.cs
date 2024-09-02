using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonMover : MonoBehaviour
{
    private Vector3 originalPosition;
    private ButtonManager buttonManager;

    void Start()
    {
        // 버튼의 초기 위치를 저장합니다.
        originalPosition = transform.localPosition;

        // ButtonManager 컴포넌트를 찾습니다.
        buttonManager = FindObjectOfType<ButtonManager>();
    }

    // 버튼이 클릭되었을 때 호출될 메서드
    public void OnButtonClick()
    {
        // 클릭된 버튼의 위치를 왼쪽으로 이동시킵니다.
        transform.localPosition = originalPosition + new Vector3(-70, 0, 0);

        // ButtonManager에게 이 버튼이 클릭되었음을 알립니다.
        buttonManager.OnButtonClicked(this);
    }

    // 버튼의 위치를 원래 위치로 되돌리는 메서드
    public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}