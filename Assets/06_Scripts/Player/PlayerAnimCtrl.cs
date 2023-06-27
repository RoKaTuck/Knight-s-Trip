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

    public void SprintAnim(bool isRun)
    {
        _animator.SetBool("Sprint", isRun);
    }

    public void SwordAttackAnim(float attackSpeed)
    {
        _animator.SetFloat("AttackSpeed", attackSpeed);
        _animator.SetTrigger("NormalAttack");        
    }

    public void LongSwordAttackAnim(float attackSpeed)
    {
        _animator.SetTrigger("LongSwordAttack");
        _animator.SetFloat("AttackSpeed", attackSpeed);
    }

    public void JumpAnim(bool jump)
    {
        _animator.SetBool("Jump", jump);
    }

    public void DeathAnim()
    {
        _animator.SetTrigger("Death");
    }

    public void RollAnim()
    {
        _animator.SetTrigger("Roll");
    }
}
