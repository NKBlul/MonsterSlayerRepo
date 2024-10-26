using TMPro;
using Unity.VisualScripting;
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

    public TextMeshProUGUI upgradeNameText;
    public TextMeshProUGUI upgradeDescriptionText;
    public TextMeshProUGUI upgradeCostText;

    public Button button;

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

        upgradeNameText.text = upgradeName;

        button.onClick.AddListener(UpgradePowerUp);

    }

    private float CalculateUpgradeCost(int level)
    {
        // Scale the cost dynamically based on current level
        float scalingFactor = 2f;  // Adjust this to fine-tune the scaling curve
        float cost = baseCost * Mathf.Pow(scalingFactor, level - 1);
        return Mathf.Round(cost);  // Round to the nearest whole number
    }

    public void UpgradePowerUp()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel += 1;
            currentCost = CalculateUpgradeCost(currentLevel);
            //upgradeCostText.text = $"Cost: {currentCost}";

            // Optional: apply the stat modifier logic or other upgrade effects here
            Debug.Log($"New Cost at Level {currentLevel}: {currentCost}");
        }
        else
        {
            Debug.Log("Max Level Reached");
        }
    }
}
