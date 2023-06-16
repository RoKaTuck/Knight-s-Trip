using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponManager : MonoBehaviour
{
    // 무기 중복 교체 실행 방지
    public static bool _isChangeWeapon = false;

    // 현재 무기
    public static Transform _currentWeapon;

    // 현재 무기의 타입.
    [SerializeField]
    private string _currentWeaponType;

    // 무기 교체 딜레이, 무기 교체가 완전히 끝난 시점
    [SerializeField]
    private float _changeWeaponDelayTime;
    [SerializeField]
    private float _changeWeaponEndDelayTime;

    // 무기 종류들 전부 관리
    [SerializeField]
    private Sword[] _swords;    

    // 관리 차원에서 쉽게 무기 접근이 가능하도록 만듦.
    private Dictionary<string, Sword> _swordDictionary = new Dictionary<string, Sword>();    

    // 필요한 컴포넌트
    [SerializeField]
    private SwordCtrl _swordCtrl;
    
    void Start()
    {
        for (int i = 0; i < _swords.Length; i++)
        {
            _swordDictionary.Add(_swords[i]._swordName, _swords[i]);
        }               
    }

    void Update()
    {
        if(_isChangeWeapon == false)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                // 무기 교체 실행(소드)
                StartCoroutine(CRT_ChangeWeapon(new WeaponSword(), "SWORD", "Old Katana"));                
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // 무기 교체 실행(소드)
                StartCoroutine(CRT_ChangeWeapon(new WeaponLongSword(), "LONGSWORD","Old LongSword"));                
            }
        }
    }

    public IEnumerator CRT_ChangeWeapon(WeaponAttack attacktype, string type, string name)
    {
        _isChangeWeapon = true;

        yield return new WaitForSeconds(_changeWeaponDelayTime);

        CancelPreWeaponAcCtion();
        WeaponChange(attacktype, name);

        yield return new WaitForSeconds(_changeWeaponEndDelayTime);

        _currentWeaponType = type;
        _isChangeWeapon = false;
    }

    private void WeaponChange(WeaponAttack type, string name)
    {
        if (type != null)
        {
            _swordCtrl.SwordChange(_swordDictionary[name]);
            _swordCtrl.SetWeaponType(type);
        }
        
        //if(type == "SWORD")
        //    _swordCtrl.SetWeaponType(new WeaponSword());
        //else if(type == "LONGSWORD")
        //    _swordCtrl.SetWeaponType(new WeaponLongSword());
    }

    private void CancelPreWeaponAcCtion()
    {
        switch (_currentWeaponType)
        {
            case "SWORD":
                break;
        }
    }
}
