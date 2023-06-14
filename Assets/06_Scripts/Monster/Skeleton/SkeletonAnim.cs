using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnim : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Awake", 0f);
    }    


    public void IdleAnim()
    {
        _animator.SetFloat("Awake", 1);
    }

    public void ChaseAnim(bool isChase)
    {
        _animator.SetBool("Chase", isChase);
    }

    public void AttackAnim(int random)
    {
        _animator.SetInteger("Attack", random);
    }

}
