using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public string _swordName; // Į�� �̸�
    public int _reinforceLevel; // ��ȭ �ܰ�
    public int _swordType; // 0���� �Ϲ� �ҵ� 1���� �ռҵ�
    public float _swordSpeed; // ���ݼӵ� ����
    public int _damage; // Į�� ���ݷ�

    public ParticleSystem _muzzleFlash;
    public AudioClip _swordSound;
}
