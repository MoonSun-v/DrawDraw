using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Text Text_GameResult; // ������ ����� ǥ������ Text Ui
    public Text ScoreText; // ������ ����� ������ Text Ui

    private CollisionCounter count; // CollisionCounter ��ũ��Ʈ�� ������ ����
    private  MonoBehaviour LineDrawManager; // LineDrawManager ��ũ��Ʈ�� ������ ����

    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� ��� �˾� â�� ������ �ʵ��� �Ѵ�.   
    }

    public void Show()
    {

        Text_GameResult.text = "�浹 Ƚ�� : " + ScoreText.text; // �˾��� ���� â�� ���� ������ ǥ���Ѵ�.
        transform.gameObject.SetActive(true); // ��� �˾� â�� ȭ�鿡 ǥ��
        //DrawArea.SetDrawActivate(false);
    }

}
