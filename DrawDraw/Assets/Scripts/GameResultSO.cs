using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �� ���� ������ ���� �� ������ ���� : ��ũ���ͺ� ������Ʈ

[CreateAssetMenu(fileName = "GameResultSO", menuName = "ScriptableObjects/GameResultSO", order = 1)]
public class GameResultSO : ScriptableObject
{
    public int score; // ���� (���� ����/���� ���� �ǰ� ���ؼ�)
    public string previousScene; // ���� �� ("��� �ҷ�" ��ư Ŭ�� ��)
}
