using UnityEngine;

public enum UpgradeType
{
    IncreaseCoinDrop,
    IncreaseDamage,
    Mine,
    Companion,
    // Add more as needed
}

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;
    public int currentLevel;
    public int maxLevel;
    public float baseCost;
    public float currentCost;
    public Sprite upgradeIcon;
    public UpgradeType upgradeType;
}
