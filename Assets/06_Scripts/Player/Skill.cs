using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData _skillData;
    public Image _skillImage;
    public Image _coolTime;

    private void Start()
    {
        _skillImage.sprite = _skillData._icon;
    }
}
