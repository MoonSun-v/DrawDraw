using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LineColorBTN : MonoBehaviour
{
    private Vector3 originalPosition;

    public int buttonID;

    private ColorManger ColorManger;

    void Start()
    {
        originalPosition = transform.localPosition;

        ColorManger = FindObjectOfType<ColorManger>();

    }

    public void OnCyayonClick()
    {
        transform.localPosition = originalPosition + new Vector3(-70, 0, 0);
        ColorManger.OnButtonClicked(this);
        ColorManger.SetSelectedButtonID(buttonID);
    }

    public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}