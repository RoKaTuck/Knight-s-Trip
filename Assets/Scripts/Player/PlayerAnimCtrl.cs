using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void MoveAnim(float inputX, float inputZ)
    {
        _animator.SetFloat("InputX", inputX);
        _animator.SetFloat("InputZ", inputZ);
    }

    public void WarriorNormalAttackAnim()
    {
        _animator.SetTrigger("NormalAttack");
    }

    public void WarriorChargingAnim()
    {

    }

    public void DeathAnim()
    {

    }
}
