using UnityEngine;
using UnityEngine.UI;

public class CheckpopupManager : MonoBehaviour
{
    public void check()
    {
        transform.gameObject.SetActive(true); // 결과 팝업 창을 화면에 표시
        //DrawArea.SetDrawActivate(false);
    }
}
