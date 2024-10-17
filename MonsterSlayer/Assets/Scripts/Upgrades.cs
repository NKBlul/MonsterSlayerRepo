using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public UpgradeSO upgradeSO;

    public string upgradeName;
    public string description;
    public int currentLevel;
    public int maxLevel;
    public float baseCost;
    public float currentCost;
    public Image icon;
    public float statModifier;

    private void Awake()
    {
        upgradeName = upgradeSO.upgradeName;
        description = upgradeSO.upgradeDescription;
        currentLevel = upgradeSO.currentLevel;
        maxLevel = upgradeSO.maxLevel;
        baseCost = upgradeSO.baseCost;
        currentCost = upgradeSO.currentCost;
        statModifier = upgradeSO.statModifier;
        icon.sprite = upgradeSO.upgradeIcon;
    }

    public float GetUpgradeCost()
    {
        return (0.5f * Mathf.Pow(baseCost, 1.5f)) + 15.0f;
    }
}
