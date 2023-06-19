using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public int level;
    public float fireRate;
    public GameObject gameObject;
    // �߰����� ���� �Ӽ��� �ʿ信 ���� �߰��� �� �ֽ��ϴ�.
}
