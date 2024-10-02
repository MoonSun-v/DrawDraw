using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonChange : MonoBehaviour
{
    // 버튼이 클릭되었을 때 변경할 이미지
    public Sprite clickedImage;

    // 원래 버튼의 이미지
    private Sprite originalImage;

    // 이 버튼의 ID
    public int buttonID;

    // 버튼의 Image 컴포넌트 참조
    private Image buttonImage;

    private ColorButtonManager buttonManager;

    // 클릭 시 변경할 scale
    public Vector3 clickedScale = new Vector3(1.75f, 1.75f, 1);  // 클릭 시 크기를 1.2배로 키움

    // 원래 scale 저장
    private Vector3 originalScale;


    void Start()
    {
        // 버튼의 Image 컴포넌트를 가져옵니다.
        buttonImage = GetComponent<Image>();

        // 원래 이미지 저장
        originalImage = buttonImage.sprite;

        // ButtonManager 컴포넌트를 찾습니다.
        buttonManager = FindObjectOfType<ColorButtonManager>();

        // 원래 scale 저장
        originalScale = transform.localScale;
    }

    // 버튼이 클릭되었을 때 호출될 메서드
    public void OnButtonClick()
    {
        // 클릭된 버튼의 이미지를 변경합니다.
        buttonImage.sprite = clickedImage;

        // 버튼의 scale을 변경합니다.
        transform.localScale = clickedScale;

        // ButtonManager에게 이 버튼이 클릭되었음을 알립니다.
        buttonManager.OnButtonClicked(this);

        // ButtonManager에게 클릭된 버튼의 ID를 전달합니다.
        buttonManager.SetSelectedButtonID(buttonID);


    }
    // 버튼의 이미지를 원래 이미지로 되돌리는 메서드
    public void ResetImage()
    {
        buttonImage.sprite = originalImage;

        // scale을 원래 크기로 되돌립니다.
        transform.localScale = originalScale;
    }


}