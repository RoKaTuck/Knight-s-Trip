using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusCtrl : MonoBehaviour
{

    // 체력
    [SerializeField]
    private int _hp;
    private int _currentHp;

    // 마나
    [SerializeField]
    private int _mp;
    private int _currentMp;

    // 스태미나
    [SerializeField]
    private int _sp;
    private int _currentSp;

    // 스태미나 증가량
    [SerializeField]
    private int _spIncreaseSpeed;

    // 스태미나 재회복 딜레이
    [SerializeField]
    private int _spRechargeTime;
    private int _currentSpRechargeTime;

    // 스태미나 감소 여부
    private bool _spUsed;

    // 방어력
    [SerializeField]
    private int _dp;
    private int _currentDp;

    // 필요한 슬라이더
    [SerializeField]
    private Slider[] _imagesGauge;

    private const int HP = 0, MP =1, SP = 2;

    private void Start()
    {
        _currentHp = _hp;
        _currentMp = _mp;
        _currentDp = _dp;
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
            Debug.Log("캐릭터의 Hp가 0이 되었습니다.");
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
            Debug.Log("캐릭터의 Mp가 0이 되었습니다.");
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
