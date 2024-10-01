using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangramGameManager_LV2 : MonoBehaviour
{
    public GameObject patternSelectPanel;
    public GameObject patternbutton;
    public Sprite[] patternImages;
    public GameObject boardImage;
    private Image patternButtonImage;
    public GameObject[] patternSilhouettes;
    public GameObject[] patternPieces;
    public GameObject[] button;

    public Text explainText;

    private Sprite initialPatternImage; // ó�� ������ �̹����� ������ ����
    private bool isPatternChanged = false; // ������ ����Ǿ����� Ȯ���ϴ� ����


    void Start()
    {
        patternButtonImage = patternbutton.GetComponent<Image>();

        ShowPatternSelect();

        //explainText.text = "������ �巡���ؼ� ���纸��!";

    }

    public void ShowPatternSelect()
    {
        //explainText.text = "� ������ ���纼��?";

        patternSelectPanel.SetActive(true);
        patternButtonImage.gameObject.SetActive(true);
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

    public void SelectPattern(int patternIndex)
    {
        patternSelectPanel.SetActive(false);
        patternButtonImage.gameObject.SetActive(true); // ���� ��ư �̹��� Ȱ��ȭ
        
        Invoke("ActivateBoardImage", 3f);

        if (patternIndex >= 0 && patternIndex < patternImages.Length && patternIndex < patternSilhouettes.Length)
        {
            initialPatternImage = patternImages[patternIndex];

            patternButtonImage.sprite = patternImages[patternIndex];
            
            StartCoroutine(ChangePatternImageAfterDelay(5.0f));
        }
    }

    private void ActivateBoardImage()
    {
        boardImage.SetActive(true); // 3�� �Ŀ� boardImage Ȱ��ȭ

        // ��� �Ƿ翧, ����, ��ư���� ��Ȱ��ȭ
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

        // ���Ͽ� �ش��ϴ� �Ƿ翧, ����, ��ư Ȱ��ȭ
        if (patternButtonImage.sprite != null)
        {
            int patternIndex = System.Array.IndexOf(patternImages, patternButtonImage.sprite); // ���� ���õ� ���� �ε��� ����
            if (patternIndex >= 0 && patternIndex < patternSilhouettes.Length)
            {
                patternSilhouettes[patternIndex].SetActive(true);
                patternPieces[patternIndex].SetActive(true);
                button[patternIndex].gameObject.SetActive(true);
            }
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
        // ������ ����� ���¶�� ó�� �̹����� 3�ʰ� ���ƿԴٰ� �ٽ� patternImages[2]�� ����
        if (isPatternChanged)
        {
            StartCoroutine(ShowInitialPatternTemporarily());
        }
    }

    public IEnumerator ShowInitialPatternTemporarily()
    {
        // ó�� �����ߴ� ���� �̹����� 3�� ���� ������
        patternButtonImage.sprite = initialPatternImage;

        yield return new WaitForSeconds(3.0f);

        // �ٽ� patternImages[2]�� ���ƿ�
        patternButtonImage.sprite = patternImages[2];
    }
}
/*
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

    private Sprite initialPatternImage; // ó�� ������ �̹����� ������ ����
    private bool isPatternChanged = false; // ������ ����Ǿ����� Ȯ���ϴ� ����


    void Start()
    {
        patternButtonImage = patternbutton.GetComponent<Image>();

        ShowPatternSelect();

        //explainText.text = "������ �巡���ؼ� ���纸��!";

    }

    public void ShowPatternSelect()
    {
        //explainText.text = "� ������ ���纼��?";

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
        patternButtonImage.gameObject.SetActive(true); // ���� ��ư �̹��� Ȱ��ȭ
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
        // ������ ����� ���¶�� ó�� �̹����� 3�ʰ� ���ƿԴٰ� �ٽ� patternImages[2]�� ����
        if (isPatternChanged)
        {
            StartCoroutine(ShowInitialPatternTemporarily());
        }
    }

    public IEnumerator ShowInitialPatternTemporarily()
    {
        // ó�� �����ߴ� ���� �̹����� 3�� ���� ������
        patternButtonImage.sprite = initialPatternImage;

        yield return new WaitForSeconds(3.0f);

        // �ٽ� patternImages[2]�� ���ƿ�
        patternButtonImage.sprite = patternImages[2];
    }
}
*/