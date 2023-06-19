using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int damage;
    public int level;
    public float fireRate;
    public GameObject gameObject;
    // 추가적인 무기 속성을 필요에 따라 추가할 수 있습니다.
}
