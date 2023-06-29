using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Skill[] _skills;

    private const int SKILL1 = 0, SKILL2 = 1;

    // 필요한 컴포넌트
    [SerializeField]
    private PlayerAnimCtrl _animCtrl;
    private ObjectPoolingSystem _poolingSystem;

    private void Start()
    {
        _poolingSystem = ObjectPoolingSystem._instance;
    }

    public void Skill1Active()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3) && _skills[SKILL1]._CanSkill == true)
        {
            _animCtrl.Skill1();
            SkillActive(SKILL1);
        }
    }

    public void Skill2Active()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && _skills[SKILL2]._CanSkill == true)
        {
            _animCtrl.Skill2();
            SkillActive(SKILL2);
        }
    }

    private void SkillActive(int skillNum)
    {
        _skills[skillNum].SkillCoolTime(_skills[skillNum]._skillData._cool);        
        //GameObject particle = Instantiate(_skills[skillNum]._skillData._particle,
          //                    transform.position, transform.rotation, transform);

        GameObject particle = _poolingSystem.InstantiateAPS(_skills[skillNum]._skillData.name,
                              transform.position, transform.rotation, Vector3.one, 
                              transform.gameObject);

        particle.transform.localPosition = _skills[skillNum]._skillData._particle.transform.position;
        particle.transform.localEulerAngles = _skills[skillNum]._skillData._particle.transform.eulerAngles;

        particle.transform.SetParent(null);
    }
}
