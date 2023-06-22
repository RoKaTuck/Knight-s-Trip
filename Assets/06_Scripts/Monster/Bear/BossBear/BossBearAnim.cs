using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearAnim : MonoBehaviour
{
    // 필요한 컴포넌트
    private Animator _bossBearAnim;

    private void Awake()
    {
        _bossBearAnim = GetComponent<Animator>();
    }

    public void SleepAnim()
    {
        _bossBearAnim.SetTrigger("Sleep");
    }

    public void AwakeAnim()
    {
        _bossBearAnim.SetTrigger("Howling");        
    }

    public void ResetHowling()
    {
        _bossBearAnim.ResetTrigger("Howling");
    }

    public void ChaseAnim(bool isChase)
    {
        _bossBearAnim.SetBool("Chase", isChase);
    }

    public void NormalAttacAnim(int num)
    {
        _bossBearAnim.SetInteger("NormalAttack", num);
    }

    public void DeathAnim()
    {
        _bossBearAnim.SetTrigger("Death");
    }
}
