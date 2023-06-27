using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public string _swordName; // 칼의 이름
    public int _reinforceLevel; // 강화 단계
    public int _swordType; // 0번은 일반 소드 1번은 롱소드
    public float _swordSpeed; // 공격속도 보정
    public int _damage; // 칼의 공격력

    public ParticleSystem _muzzleFlash;
    public AudioClip _swordSound;
}
