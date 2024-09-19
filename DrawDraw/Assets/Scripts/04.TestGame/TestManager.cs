using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountSecCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator CountSecCoroutine()
    {
        countText.text = "3";
        yield return new WaitForSeconds(1.0f);
        countText.text = "2";
        yield return new WaitForSeconds(1.0f);
        countText.text = "1";
        yield return new WaitForSeconds(1.0f);
        countText.text = "����!";

        SceneManager.LoadScene("Test_1Line"); //�� �׸��� ������ ���� ���� �ʿ� -> ���� �Ϸ�!
    }
}
