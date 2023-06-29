using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public int _atk = 20;
    public int _def = 5;
    private int _originalAtk = 20;
    private int _originalDef = 5;

    public int _Atk { get { return _atk; } set { _atk = value; } }
    public int _Def { get { return _def; } set { _def = value; } }

    // 필요한 컴포넌트
    [SerializeField]
    private PlayerMoveCtrl _moveCtrl;
    [SerializeField]
    private PlayerAttackCtrl _attackCtrl;
    [SerializeField]
    private DeathUi _deathUi;
    [SerializeField]
    private StatusCtrl _statusCtrl;
    [SerializeField]
    private PlayerSkill _playerSkill;

    private void FixedUpdate()
    {
        if (_moveCtrl._Move == false)
            return;

        if (EntranceDungeonUi._isUiActivated == false && NpcCtrl._isInteracting == false)
            _moveCtrl.RigidMove();
        else
        {
            _moveCtrl.MoveState(0, 0);
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            return;
        }

        if (Inventory._inventoryActivated == false && NpcCtrl._isInteracting == false)
        {
            _moveCtrl.CameraRotation();
            _moveCtrl.CharacterRotation();
        }
    }

    private void Update()
    {        
        if (EntranceDungeonUi._isUiActivated == false)
        {
            _moveCtrl.IsGround();
            _moveCtrl.TryJump();
            _moveCtrl.TryRun();            
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Inventory._inventoryActivated == false && EntranceDungeonUi._isUiActivated == false && NpcCtrl._isInteracting == false)
        {
            _attackCtrl.Attack();
            _playerSkill.Skill1Active();
            _playerSkill.Skill2Active();
        }
    }
    
    public void Hit(int count)
    {
        _statusCtrl.DecreaseHp(count);

        if (_statusCtrl.GetCurrentHp() <= 0)
        {
            Death();
        }
    }

    public void OnDeath()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        _deathUi.StartFadeIn();
        enabled = false;
    }

    public void Death()
    {        
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        _moveCtrl.Death();                
    }

    public void UpdateStat()
    {
        _Atk = GameManager.Instance._Level * _originalAtk;
        _Def = GameManager.Instance._Level * _originalDef;
    }
}
