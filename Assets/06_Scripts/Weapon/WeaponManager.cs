using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponManager : MonoBehaviour
{
    // ���� �ߺ� ��ü ���� ����
    public static bool _isChangeWeapon = false;
    public static bool _WeaponActivated = false;

    // ���� ����
    public static Transform _currentWeapon;

    // ���� ������ Ÿ��.
    [SerializeField]
    private string _currentWeaponType;
    [SerializeField]
    private GameObject _weaponPanel;

    // ���� ��ü ������, ���� ��ü�� ������ ���� ����
    [SerializeField]
    private float _changeWeaponDelayTime;
    [SerializeField]
    private float _changeWeaponEndDelayTime;

    // ���� ������ ���� ����
    [SerializeField]
    private Sword[] _swords;    

    // ���� �������� ���� ���� ������ �����ϵ��� ����.
    private Dictionary<string, Sword> _swordDictionary = new Dictionary<string, Sword>();    

    // �ʿ��� ������Ʈ
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
        TryOpenWeaponPanel();

        if (_isChangeWeapon == false)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                // ���� ��ü ����(�ҵ�)
                StartCoroutine(CRT_ChangeWeapon(new WeaponSword(), "SWORD", "Old Sword"));                
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // ���� ��ü ����(�ҵ�)
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

    private void TryOpenWeaponPanel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _WeaponActivated = !_WeaponActivated;

            if (_WeaponActivated == true)
                OpenWeaponPanel();
            else
                CloseWeaponPanel();
        }
    }

    private void OpenWeaponPanel()
    {
        _weaponPanel.SetActive(true);
    }

    private void CloseWeaponPanel()
    {
        _weaponPanel.SetActive(false);
    }

    private void WeaponChange(WeaponAttack type, string name)
    {
        if (type != null)
        {
            _swordCtrl.SwordChange(_swordDictionary[name]);
            _swordCtrl.SetWeaponType(type);
        }
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
