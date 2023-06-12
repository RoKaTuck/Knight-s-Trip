using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bear Data", menuName = "Scriptable Object / Bear Data", order = int.MaxValue)]
public class BearData : ScriptableObject
{
    [SerializeField]
    private string _bearName;
    public string BearName { get { return _bearName; } }
    [SerializeField]
    private int _hp;
    public int Hp { get { return _hp; } }
    [SerializeField]
    private int _atk;
    public int Atk { get { return _atk; } }
    [SerializeField]
    private int _def;
    public int Def { get { return _def; } }
    [SerializeField]
    private float _moveSpeed;
    public float Movespeed { get { return _moveSpeed; } }
}
