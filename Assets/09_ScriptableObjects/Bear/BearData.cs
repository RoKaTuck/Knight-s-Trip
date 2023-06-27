using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이름 체력 데미지 방어력 스피드
[CreateAssetMenu(fileName = "Bear Data", menuName = "Scriptable Object / Bear Data", order = int.MaxValue)]
public class BearData : ScriptableObject
{
    public string _bearName;
    public int _hp;
    public int _dmg;
    public int _def;
    public int _speed;
    public int _exp;

    public enum eBearType
    {
        Normal,
        Boss
    }
}
