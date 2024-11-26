using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// [ �� ��ũ���ͺ� ������Ʈ �� ] 
// �� ���� ������ ���� �� ������ �����ϴ� �뵵�� ����Ѵ�.

[CreateAssetMenu(fileName = "GameResultSO", menuName = "ScriptableObjects/GameResultSO", order = 1)]

// �� score          : ����    (���� ����/���� ���� �ǰ� ���ؼ�)
// �� previousScene  : ���� �� ("��� �ҷ�" ��ư Ŭ�� �� �ǵ��ư��� ����)
public class GameResultSO : ScriptableObject
{
    public int score; 
    public string previousScene; 
}
