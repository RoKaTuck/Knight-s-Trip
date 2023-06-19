using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager2 : MonoBehaviour
{    
    public List<WeaponData> weapons; // ���� �����͸� ���� ����Ʈ
    private int currentWeaponIndex = -1; // ���� ���õ� ������ �ε���

    public Animator weaponAnimator; // ���� ��ȯ �ִϸ�����
    public AudioSource reloadSound; // ������ ����

    public Text weaponInfoText; // ���� ������ ǥ���� �ؽ�Ʈ

    private void Start()
    {
        // �Ŵ��� ���� �� ù ��° ���⸦ ����
        SelectWeapon(0);
    }

    private void Update()
    {
        // ���� Ű �Է��� ���� ���� ����
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SelectWeapon(i);
                break;
            }
        }

        // R Ű�� ���� ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadWeapon();
        }

        // T Ű�� ���� ���� ��ȭ
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpgradeWeapon();
        }
    }

    private void SelectWeapon(int weaponIndex)
    {
        if (currentWeaponIndex == weaponIndex)
            return; // �̹� ���õ� ������ ��� �ߺ� ���� ����

        // ���� ���õ� ���⸦ ��Ȱ��ȭ
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }

        // ���ο� ���⸦ Ȱ��ȭ
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = weaponIndex;

        // ���� ��ȯ �ִϸ��̼� ���
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("SwitchWeapon");
        }

        // ���� ���� ������Ʈ
        UpdateWeaponInfo(weapons[weaponIndex]);
    }

    private void ReloadWeapon()
    {
        if (currentWeaponIndex == -1)
            return; // ���õ� ���Ⱑ ���� ��� ����

        // ������ ���� ���
        if (reloadSound != null)
        {
            reloadSound.Play();
        }

        // ������ ���� �߰�
        // ...
    }

    private void UpgradeWeapon()
    {
        if (currentWeaponIndex == -1)
            return; // ���õ� ���Ⱑ ���� ��� ����

        WeaponData currentWeapon = weapons[currentWeaponIndex];

        // ��ȭ ���� Ȯ�� (����, ��� ��)

        // ��ȭ�� �ʿ��� �ڿ� �Ҹ�
        int requiredGold = GetUpgradeCost(currentWeapon.level);
        if (GameManager.Instance._Gold >= requiredGold)
        {
            GameManager.Instance._Gold -= requiredGold;

            // ��ȭ ���� �� ���� ���� �� ������ ����
            currentWeapon.level++;
            currentWeapon.damage += GetDamageIncrease(currentWeapon.level);

            // ���� ���� ������Ʈ
            UpdateWeaponInfo(currentWeapon);

            // ��ȭ ����Ʈ ���
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
        // ������ ���� ��ȭ ��� ��� ����
        // ...

        return 0;
    }

    private int GetDamageIncrease(int level)
    {
        // ������ ���� ������ ������ ��� ����
        // ...

        return 0;
    }

    public void AddWeapon(WeaponData weapon)
    {
        weapons.Add(weapon);

        // �߰��� ���⸦ ��Ȱ��ȭ
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

