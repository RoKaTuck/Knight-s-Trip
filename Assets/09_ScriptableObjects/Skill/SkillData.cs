using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object / Skill Data", order = int.MaxValue)]
public class SkillData : ScriptableObject
{
    public float _damage;
    public float _cool;

    public GameObject _particle;
    public Sprite _icon;
}
