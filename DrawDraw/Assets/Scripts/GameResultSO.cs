using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 각 씬의 점수와 이전 씬 정보를 저장 : 스크립터블 오브젝트

[CreateAssetMenu(fileName = "GameResultSO", menuName = "ScriptableObjects/GameResultSO", order = 1)]
public class GameResultSO : ScriptableObject
{
    public int score; // 점수 (게임 성공/실패 여부 판결 위해서)
    public string previousScene; // 이전 씬 ("계속 할래" 버튼 클릭 시)
}
