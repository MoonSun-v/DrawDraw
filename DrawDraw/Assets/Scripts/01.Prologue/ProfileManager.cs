using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public InputField NameInput;
    private string PlayerName = null;

    private bool isDog;
    private bool isCat;

    void Start()
    {
        // 이름 입력란의 글자 수를 6자로 제한
        NameInput.characterLimit = 6;

        // 특수문자, 공백 필터링
        NameInput.onValueChanged.AddListener(ValidateInput);
    }

    // 강아지 선택
    public void SelectDog()
    {
        isDog = true; isCat = false;
    }

    // 고양이 선택
    public void SelectCat()
    {
        isDog = false; isCat = true;
    }

    // 완료 버튼 클릭
    public void Finish()
    {
        // PlayerName 업데이트
        PlayerName = NameInput.GetComponent<InputField>().text;


        // 캐릭터와 이름 검사
        if (string.IsNullOrEmpty(PlayerName))
        {
            print(isDog || isCat ? "이름을 적어주세요" : "캐릭터를 선택하고 이름을 적어주세요");
            return;
        }

        if (!isDog && !isCat)
        {
            print("캐릭터를 선택해 주세요");
            return;
        }


        // 플레이어 정보 출력
        print("플레이어 이름 = " + PlayerName);

        if (isDog) { print("플레이어 캐릭터 = 강아지"); }
        if (isCat) { print("플레이어 캐릭터 = 고양이"); }
        SceneManager.LoadScene("MapScene");

    }


    // 특수문자 필터링 메서드
    void ValidateInput(string input)
    {
        // 숫자, 알파벳, 한글 허용, 특수문자와 공백은 제거
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9가-힣]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }
}
