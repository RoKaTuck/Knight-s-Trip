using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager2 : MonoBehaviour
{    
    public List<WeaponData> weapons; // 무기 데이터를 담을 리스트
    private int currentWeaponIndex = -1; // 현재 선택된 무기의 인덱스

    public Animator weaponAnimator; // 무기 전환 애니메이터
    public AudioSource reloadSound; // 재장전 사운드

    public Text weaponInfoText; // 무기 정보를 표시할 텍스트

    private void Start()
    {
        // 매니저 시작 시 첫 번째 무기를 선택
        SelectWeapon(0);
    }

    private void Update()
    {
        // 숫자 키 입력을 통해 무기 선택
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SelectWeapon(i);
                break;
            }
        }

        // R 키를 눌러 재장전
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon();
        }

        // T 키를 눌러 무기 강화
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpgradeWeapon();
        }
    }

    private void SelectWeapon(int weaponIndex)
    {
        if (currentWeaponIndex == weaponIndex)
            return; // 이미 선택된 무기인 경우 중복 선택 방지

        // 현재 선택된 무기를 비활성화
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // 새로운 무기를 활성화
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = weaponIndex;

        // 무기 전환 애니메이션 재생
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("SwitchWeapon");
        }

        // 무기 정보 업데이트
        UpdateWeaponInfo(weapons[weaponIndex]);
    }

    private void ReloadWeapon()
    {
        if (currentWeaponIndex == -1)
            return; // 선택된 무기가 없는 경우 중지

        // 재장전 사운드 재생
        if (reloadSound != null)
        {
            reloadSound.Play();
        }

        // 재장전 로직 추가
        // ...
    }

    private void UpgradeWeapon()
    {
        if (currentWeaponIndex == -1)
            return; // 선택된 무기가 없는 경우 중지

        WeaponData currentWeapon = weapons[currentWeaponIndex];

        // 강화 조건 확인 (레벨, 골드 등)

        // 강화에 필요한 자원 소모
        int requiredGold = GetUpgradeCost(currentWeapon.level);
        if (GameManager.Instance._Gold >= requiredGold)
        {
            GameManager.Instance._Gold -= requiredGold;

            // 강화 성공 시 무기 레벨 및 데미지 증가
            currentWeapon.level++;
            currentWeapon.damage += GetDamageIncrease(currentWeapon.level);

            // 무기 정보 업데이트
            UpdateWeaponInfo(currentWeapon);

            // 강화 이펙트 재생
            // ...

            Debug.Log("Weapon upgraded successfully!");
        }
        else
        {
            Debug.Log("Insufficient gold for upgrade!");
        }
    }

    private int GetUpgradeCost(int level)
    {
        // 레벨에 따른 강화 비용 계산 로직
        // ...

        return 0;
    }

    private int GetDamageIncrease(int level)
    {
        // 레벨에 따른 데미지 증가량 계산 로직
        // ...

        return 0;
    }

    public void AddWeapon(WeaponData weapon)
    {
        weapons.Add(weapon);

        // 추가된 무기를 비활성화
        weapon.gameObject.SetActive(false);
    }

    private void UpdateWeaponInfo(WeaponData weapon)
    {
        if (weaponInfoText != null)
        {
            weaponInfoText.text = string.Format("Name: {0}\nDamage: {1}\nFire Rate: {2}\nLevel: {3}",
                weapon.weaponName, weapon.damage, weapon.fireRate, weapon.level);
        }
    }
}

