using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem
{
    public int _atk;
    public int _def;
    public int _upgradeLevel;

    public virtual void Upgrade(Item item) { }
}

public class WeaponItem : UpgradeItem
{       
    public WeaponItem(int atk, int upgradeLevel)
    {
        _atk = atk;
        _upgradeLevel = upgradeLevel;
    }

    public override void Upgrade(Item item)
    {
        int ran = Random.Range(0, 101);

        if(ran <= 50)
        {
            _upgradeLevel += 1;

            _atk = _atk + _upgradeLevel * item._dmg;
        }
        else if(ran > 50)
        {
            Debug.Log("강화 실패");
        }
    }
}

public class ArmorItem : UpgradeItem
{    
    public ArmorItem(int def, int upgradeLevel)
    {
        _def = def;
        _upgradeLevel = upgradeLevel;
    }

    public override void Upgrade(Item item)
    {
        int ran = Random.Range(0, 101);

        if (ran <= 50)
        {
            _upgradeLevel += 1;

            _def = _def + _upgradeLevel * item._def;
        }
        else if (ran > 50)
        {
            Debug.Log("강화 실패");
        }
    }
}

