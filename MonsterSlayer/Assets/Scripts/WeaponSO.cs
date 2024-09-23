using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public WeaponType weaponType;
    public Elements weaponElements;
    public float weaponDamage;
    public string weaponDesc;
}

public enum WeaponType
{
    Sword,
    BowandArrow,
    Dagger,
    Hammer
}