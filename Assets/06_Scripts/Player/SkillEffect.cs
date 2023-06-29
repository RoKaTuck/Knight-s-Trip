using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public SkillData _skillData;

    private void OnParticleCollision(GameObject other)
    {
        MonsterCtrl monster = other.GetComponent<MonsterCtrl>();
        
        if(monster != null && monster.gameObject.layer == 8)
        {
            monster.Hit(_skillData._damage);
        }
    }
}
