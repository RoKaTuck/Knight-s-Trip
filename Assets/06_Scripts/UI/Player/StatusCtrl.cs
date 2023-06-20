using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusCtrl : MonoBehaviour
{

    // ü��
    [SerializeField]
    private int _hp;
    private int _currentHp;

    public int _Hp { get {return _currentHp; } set { _currentHp = value; } }

    // ����
    [SerializeField]
    private int _mp;
    private int _currentMp;

    public int _Mp { get { return _currentMp; } set { _currentMp = value; } }

    // ���¹̳�
    [SerializeField]
    private int _sp;
    private int _currentSp;

    public int _Sp { get { return _currentSp; } set { _currentSp = value; } }

    // ���¹̳� ������
    [SerializeField]
    private int _spIncreaseSpeed;

    // ���¹̳� ��ȸ�� ������
    [SerializeField]
    private int _spRechargeTime;
    private int _currentSpRechargeTime;

    // ���¹̳� ���� ����
    private bool _spUsed;    

    // �ʿ��� �����̴�
    [SerializeField]
    private Slider[] _imagesGauge;

    private const int HP = 0, MP =1, SP = 2;

    private void Start()
    {
        _currentHp = _hp;
        _currentMp = _mp;        
        _currentSp = _sp;
    }

    public void IncreaseHp(int count)
    {
        if (_currentHp + count < _hp)
            _currentHp += count;
        else
            _currentHp = _hp;
    }

    public void DecreaseHp(int count)
    {
        _currentHp -= count;

        if (_currentHp <= 0)
            Debug.Log("ĳ������ Hp�� 0�� �Ǿ����ϴ�.");
    }

    public void IncreaseMp(int count)
    {
        if (_currentMp + count < _mp)
            _currentMp += count;
        else
            _currentMp = _mp;
    }

    public void DecreaseMp(int count)
    {
        _currentMp -= count;

        if (_currentMp <= 0)
            Debug.Log("ĳ������ Mp�� 0�� �Ǿ����ϴ�.");
    }

    public void IncreaseSp(int count)
    {
        if (_currentSp + count < _sp)
            _currentSp += count;
        else
            _currentSp = _sp;
    }

    public void DecreaseStamina(int count)
    {
        _spUsed = true;
        _currentSpRechargeTime = 0;

        if (_currentSp - count > 0)
            _currentSp -= count;
        else
            _currentSp = 0;
    }

    public int GetCurrentHp()
    {
        return _currentHp;
    }

    public int GetCurrentMp()
    {
        return _currentMp;
    }

    public int GetCurrentSp()
    {
        return _currentSp;
    }

    private void Update()
    {
        GagueUpdate();
        SpRechargeTime();
        SpRecover();
    }

    private void SpRechargeTime()
    {
        if(_spUsed) 
        {
            if (_currentSpRechargeTime < _spRechargeTime)
                _currentSpRechargeTime++;
            else
                _spUsed = false;
        }
    }

    private void SpRecover()
    {
        if (!_spUsed && _currentSp < _sp)
        {
            _currentSp += _spIncreaseSpeed;
        }        
    }

    private void GagueUpdate()
    {
        _imagesGauge[HP].value = (float)_currentHp / _hp;
        _imagesGauge[MP].value = (float)_currentMp / _mp;
        _imagesGauge[SP].value = (float)_currentSp / _sp;
    }

   
}
