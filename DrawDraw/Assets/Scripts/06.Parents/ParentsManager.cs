using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentsManager : MonoBehaviour
{
    public GameObject StatisticsCanvas;
    public GameObject SoundCanvas;
    public GameObject ExplainCanvas;

    public GameObject[] MenuButton;

    // Start is called before the first frame update
    void Start()
    {
        StatisticsCanvas.SetActive(true);
        SoundCanvas.SetActive(false);
        ExplainCanvas.SetActive(false);

        SetTransparency(MenuButton[0], 1);
        SetTransparency(MenuButton[1], 0);
        SetTransparency(MenuButton[2], 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTransparency(GameObject button, float alpha)
    {
        if (button != null)
        {
            Image buttonImage = button.GetComponent<Image>(); // Image 컴포넌트 가져오기
            if (buttonImage != null)
            {
                Color color = buttonImage.color;
                color.a = alpha; // 투명도 설정 (0 = 완전히 투명, 1 = 완전히 불투명)
                buttonImage.color = color;
            }
        }
    }

    public void OnStatisticsButtonClick()
    {
        StatisticsCanvas.SetActive(true);
        SetTransparency(MenuButton[0], 1);

        SoundCanvas.SetActive(false);
        SetTransparency(MenuButton[1], 0);

        ExplainCanvas.SetActive(false);
        SetTransparency(MenuButton[2], 0);
    }

    public void OnSoundButtonClick()
    {
        StatisticsCanvas.SetActive(false);
        SetTransparency(MenuButton[0], 0);

        SoundCanvas.SetActive(true);
        SetTransparency(MenuButton[1], 1);

        ExplainCanvas.SetActive(false);
        SetTransparency(MenuButton[2], 0);
    }

    public void OnExplainButtonClick()
    {
        StatisticsCanvas.SetActive(false);
        SetTransparency(MenuButton[0], 0);

        SoundCanvas.SetActive(false);
        SetTransparency(MenuButton[1], 0);

        ExplainCanvas.SetActive(true);
        SetTransparency(MenuButton[2], 1);
    }
}
