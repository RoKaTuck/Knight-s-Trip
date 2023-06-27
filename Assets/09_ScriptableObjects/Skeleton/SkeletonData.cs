using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skeleton Data", menuName = "Scriptable Object / Skeleton Data", order = int.MaxValue)]
public class SkeletonData : ScriptableObject
{
    public string _skeletonName;
    public int _hp;
    public int _dmg;
    public int _def;
    public float _speed;
    public int _exp;

    public enum eSkeletonType
    {
        Normal,
        Boss
    }
}
