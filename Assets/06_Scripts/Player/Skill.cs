using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData _skillData;
    public Image _skillImage;
    public Image _coolTime;

    private bool _canSkill = true;
    public bool _CanSkill { get { return _canSkill; } set { _canSkill = value; } }

    private void Start()
    {
        _skillImage.sprite = _skillData._icon;
    }

    public void SkillCoolTime(float cool)
    {
        _CanSkill = false;
        StartCoroutine(CRT_CoolTime(cool));
    }

    private IEnumerator CRT_CoolTime(float cool)
    {
        while(cool > 1.0f)
        {
            cool -= Time.deltaTime;
            _coolTime.fillAmount = (1.0f / cool);

            yield return new WaitForFixedUpdate();
        }

        _CanSkill = true;

        yield break;
    }
}
