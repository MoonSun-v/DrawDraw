using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LineColorBTN : MonoBehaviour
{
    private Vector3 originalPosition;

    public int buttonID;

    private ColorManger buttonManager;

    void Start()
    {
        originalPosition = transform.localPosition;

        buttonManager = FindObjectOfType<ColorManger>();

    }

    public void OnButtonClick()
    {
        transform.localPosition = originalPosition + new Vector3(-70, 0, 0);

        buttonManager.OnButtonClicked(this);
    }

    public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}