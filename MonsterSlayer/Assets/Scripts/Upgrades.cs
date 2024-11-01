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
        icon.sprite = upgradeSO.upgradeIcon;

        if (currentLevel == 0)
        {
            currentCost = baseCost;
        }

        upgradeNameText.text = upgradeName;
        upgradeCostText.text = currentCost.ToString();

        button.onClick.AddListener(UpgradePowerUp);

    }

    private float CalculateUpgradeCost(int level)
    {
        // Scale the cost dynamically based on current level
        float scalingFactor = 2f;  // Adjust this to fine-tune the scaling curve
        float cost = baseCost * Mathf.Pow(scalingFactor, level + 1);
        return Mathf.Round(cost);  // Round to the nearest whole number
    }

    public void UpgradePowerUp()
    {
        if (UIManager.instance.coin >= currentCost)
        {
            if (currentLevel < maxLevel)
            {
                UIManager.instance.coin -= currentCost;
                currentLevel += 1;
                currentCost = CalculateUpgradeCost(currentLevel);
                upgradeCostText.text = currentCost.ToString();
                //upgradeCostText.text = $"Cost: {currentCost}";

                // Optional: apply the stat modifier logic or other upgrade effects here
                Debug.Log($"New Cost at Level {currentLevel}: {currentCost}");

                switch (upgradeSO.upgradeType)
                {
                    case UpgradeType.IncreaseCoinDrop:
                        HandleIncreaseCoinDropUpgrade();
                        break;
                    case UpgradeType.IncreaseDamage:
                        HandleIncreaseDamageDealtUpgrade();
                        break;
                    case UpgradeType.Mine:
                        HandleMineUpgrade();
                        break;
                    case UpgradeType.Companion:
                        HandleMineUpgrade();
                        break;
                }
            }
            else
            {
                Debug.Log("Max Level Reached");
                button.interactable = false;
            }
        }
        
    }

    void HandleIncreaseCoinDropUpgrade()
    {
        Debug.Log($"Upgrade Coin");
        UIManager.instance.coinMultiplier = 1 + Mathf.Pow(1.25f, currentLevel + 1);
        Debug.Log($"Multiplier: {UIManager.instance.coinMultiplier}");
    }

    void HandleIncreaseDamageDealtUpgrade()
    {
        Debug.Log($"Increase Damage dealt");
    }

    void HandleMineUpgrade()
    {
        Debug.Log($"Mine");
    }

    void HandleCompanionUpgrade()
    {
        Debug.Log("Companion");
    }
}
