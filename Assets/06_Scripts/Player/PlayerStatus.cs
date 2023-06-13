using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public int UID;
    public string Name;
    public float Dmg;
    public float Hp;
    public float Def;
    public float Speed;

    public PlayerStatus(int uid, string name, float dmg, float hp, float def, float speed)
    {
        UID = uid;
        Name = name;
        Dmg = dmg;
        Hp = hp;
        Def = def;
        Speed = speed;
    }
}
