using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangramGameManager_LV2 : MonoBehaviour
{
    public GameObject patternSelectPanel;
    public GameObject patternbutton;
    public Sprite[] patternImages;
    private Image patternButtonImage;
    public GameObject[] patternSilhouettes;
    public GameObject[] patternPieces;
    public GameObject[] button;

    public Text explainText;

    private Sprite initialPatternImage; // 처음 선택한 이미지를 저장할 변수
    private bool isPatternChanged = false; // 패턴이 변경되었는지 확인하는 변수


    void Start()
    {
        patternButtonImage = patternbutton.GetComponent<Image>();

        ShowPatternSelect();

        //explainText.text = "조각을 드래그해서 맞춰보자!";

    }

    public void ShowPatternSelect()
    {
        //explainText.text = "어떤 도형을 맞춰볼래?";

        patternSelectPanel.SetActive(true);
        patternButtonImage.gameObject.SetActive(true);
        foreach (var silhouette in patternSilhouettes)
        {
            silhouette.SetActive(false);
        }
        foreach (var piece in patternPieces)
        {
            piece.SetActive(false);
        }
        foreach (var btn in button)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void SelectPattern(int patternIndex)
    {
        patternSelectPanel.SetActive(false);
        patternButtonImage.gameObject.SetActive(true); // 패턴 버튼 이미지 활성화
        foreach (var silhouette in patternSilhouettes)
        {
            silhouette.SetActive(false);
        }
        foreach (var piece in patternPieces)
        {
            piece.SetActive(false);
        }
        foreach (var btn in button)
        {
            btn.gameObject.SetActive(false);
        }

        if (patternIndex >= 0 && patternIndex < patternImages.Length && patternIndex < patternSilhouettes.Length)
        {
            initialPatternImage = patternImages[patternIndex];

            patternButtonImage.sprite = patternImages[patternIndex];
            patternSilhouettes[patternIndex].SetActive(true);
            patternPieces[patternIndex].SetActive(true);
            button[patternIndex].gameObject.SetActive(true);

            StartCoroutine(ChangePatternImageAfterDelay(3.0f));
        }
    }

    private IEnumerator ChangePatternImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (patternImages.Length > 2)
        {
            patternButtonImage.sprite = patternImages[2];
            isPatternChanged = true;
        }
    }

    public void OnPatternButtonClick()
    {
        // 패턴이 변경된 상태라면 처음 이미지로 3초간 돌아왔다가 다시 patternImages[2]로 변경
        if (isPatternChanged)
        {
            StartCoroutine(ShowInitialPatternTemporarily());
        }
    }

    public IEnumerator ShowInitialPatternTemporarily()
    {
        // 처음 선택했던 패턴 이미지를 3초 동안 보여줌
        patternButtonImage.sprite = initialPatternImage;

        yield return new WaitForSeconds(3.0f);

        // 다시 patternImages[2]로 돌아옴
        patternButtonImage.sprite = patternImages[2];
    }
}
