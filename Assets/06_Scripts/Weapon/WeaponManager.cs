using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponManager : MonoBehaviour
{
    // ���� �ߺ� ��ü ���� ����
    public static bool _isChangeWeapon = false;

    // ���� ����
    public static Transform _currentWeapon;

    // ���� ������ Ÿ��.
    [SerializeField]
    private string _currentWeaponType;

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
    private PlayerAttackCtrl _playerAttackCtrl;
    


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _swords.Length; i++)
        {
            _swordDictionary.Add(_swords[i]._swordName, _swords[i]);
        }               
    }

    // Update is called once per frame
    void Update()
    {
        if(_isChangeWeapon == false)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                // ���� ��ü ����(�ҵ�)
                StartCoroutine(CRT_ChangeWeapon("SWORD", "Old Sword"));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // ���� ��ü ����(�ҵ�)
                StartCoroutine(CRT_ChangeWeapon("LONGSWORD", "Old LongSword"));
            }
        }
    }

    public IEnumerator CRT_ChangeWeapon(string type, string name)
    {
        _isChangeWeapon = true;

        yield return new WaitForSeconds(_changeWeaponDelayTime);

        CancelPreWeaponAcCtion();
        WeaponChange(type, name);

        yield return new WaitForSeconds(_changeWeaponEndDelayTime);

        _currentWeaponType = type;
        _isChangeWeapon = false;
    }

    private void WeaponChange(string type, string name)
    {
        if(type == "SWORD" || type == "LONGSWORD")
            _playerAttackCtrl.SwordChange(_swordDictionary[name]);
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
