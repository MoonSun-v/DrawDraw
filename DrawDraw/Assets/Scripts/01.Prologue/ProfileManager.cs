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



    // ★ [ 이름 입력 제한 조건 ]
    // - 이름 입력란의 글자 수 6자로 제한
    // - 특수문자, 공백 필터링
    void Start()
    {
        NameInput.characterLimit = 6;
        NameInput.onValueChanged.AddListener(ValidateInput);
    }



    // ★ [ 강아지 / 고양이 선택 메소드 ]
    public void SelectDog(){ isDog = true; isCat = false; }
    public void SelectCat(){ isDog = false; isCat = true; }



    // ★ [ 완료 버튼 클릭 ]
    //  캐릭터와 이름 정보 입력 확인 조건문
    //  플레이어 정보 출력 (임시, 추후 GUI완성 시 변경 필요)
    // -----------
    //  ProfileDataSetting() : 플레이어 프로필 작성 정보 저장 
    // -----------
    //  다음 씬으로 이동 
    // 
    public void Finish()
    {

        PlayerName = NameInput.GetComponent<InputField>().text;

        // 캐릭터와 이름 확인 조건문 
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

        // 플레이어 정보 출력 (임시)
        print("플레이어 이름 = " + PlayerName);
        if (isDog) { print("플레이어 캐릭터 = 강아지"); }
        if (isCat) { print("플레이어 캐릭터 = 고양이"); }

        // --------------------------------------------------------------------------------------

        ProfileSetting(); // 플레이어 프로필 작성 정보 저장 

        // --------------------------------------------------------------------------------------

        SceneManager.LoadScene("SelectScene");

    }


    // ★ [ 특수문자 필터링 메서드 ]
    // 숫자, 알파벳, 한글 허용, 특수문자와 공백은 제거
    void ValidateInput(string input)
    {
        string filtered = Regex.Replace(input, @"[^a-zA-Z0-9가-힣]", "");
        if (input != filtered)
        {
            NameInput.text = filtered;
        }
    }



    // ★ [ 프로필 정보 저장하는 메소드 ]
    // 
    // PlayerCharacter : 플레이어 캐릭터 ( Dog 는 0 or Cat 은 1 ) 
    // PlayerName      : 플레이어 이름
    void ProfileSetting()
    {
        if (isDog) { GameData.instance.playerdata.PlayerCharacter = false; }
        else if (isCat) { GameData.instance.playerdata.PlayerCharacter = true; }

        GameData.instance.playerdata.PlayerName = PlayerName;

        GameData.instance.SavePlayerData();
        GameData.instance.LoadPlayerData();
    }
}
