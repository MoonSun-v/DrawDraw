using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Text Text_GameResult; // ������ ����� ǥ������ Text Ui

    private CollisionCounter count; // CollisionCounter ��ũ��Ʈ�� ������ ����
    private  MonoBehaviour LineDrawManager; // LineDrawManager ��ũ��Ʈ�� ������ ����

    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� ��� �˾� â�� ������ �ʵ��� �Ѵ�.   
    }

    public void Show()
    {
        int score = count.GetCollisionCount(); // CollisionCounter���� �浹 Ƚ���� �ҷ��´�.
        Debug.Log(score);
        //Text_GameResult.text = "�浹 Ƚ�� : " + score.ToString(); // �˾��� ���� â�� ���� ������ ǥ���Ѵ�.

        transform.gameObject.SetActive(true); // ��� �˾� â�� ȭ�鿡 ǥ��
        //DrawArea.SetDrawActivate(false);
    }

}
