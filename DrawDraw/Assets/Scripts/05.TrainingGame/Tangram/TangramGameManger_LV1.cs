using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangramGameManager_LV1 : MonoBehaviour
{
    public GameObject patternSelectPanel;
    public Image patternImage;
    public GameObject boardImage;
    public Sprite[] patternImages;
    public GameObject[] patternSilhouettes;
    public GameObject[] patternPieces;
    public GameObject[] button;

    public Text explainText;

    // Start is called before the first frame update
    void Start()
    {
        ShowPatternSelect();

        //explainText.text = "조각을 드래그해서 맞춰보자!";

    }

    public void ShowPatternSelect()
    {
        //explainText.text = "어떤 도형을 맞춰볼래?";

        patternSelectPanel.SetActive(true);
        patternImage.gameObject.SetActive(false);
        boardImage.SetActive(false);
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
    /*
        public void SelectPattern(int patternIndex)
        {
            patternSelectPanel.SetActive(false);
            patternImage.gameObject.SetActive(true);
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
                patternImage.sprite = patternImages[patternIndex];
                patternSilhouettes[patternIndex].SetActive(true);
                patternPieces[patternIndex].SetActive(true);
                button[patternIndex].gameObject.SetActive(true);
            }
        }
    */

    public void SelectPattern(int patternIndex)
    {
        patternSelectPanel.SetActive(false);
        patternImage.gameObject.SetActive(true);

        // 3초 후에 ActivateBoardImage 호출
        Invoke("ActivateBoardImage", 3f);

        // 패턴 인덱스 설정
        if (patternIndex >= 0 && patternIndex < patternImages.Length && patternIndex < patternSilhouettes.Length)
        {
            patternImage.sprite = patternImages[patternIndex];
        }
    }

    private void ActivateBoardImage()
    {
        boardImage.SetActive(true); // 3초 후에 boardImage 활성화

        // 모든 실루엣, 조각, 버튼들을 비활성화
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

        // 패턴에 해당하는 실루엣, 조각, 버튼 활성화
        if (patternImage.sprite != null)
        {
            int patternIndex = System.Array.IndexOf(patternImages, patternImage.sprite); // 현재 선택된 패턴 인덱스 추적
            if (patternIndex >= 0 && patternIndex < patternSilhouettes.Length)
            {
                patternSilhouettes[patternIndex].SetActive(true);
                patternPieces[patternIndex].SetActive(true);
                button[patternIndex].gameObject.SetActive(true);
            }
        }
    }
}