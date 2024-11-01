using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/saveData.json";

    public static void SaveGame(Player player, Enemy enemy, GameManager gameManager)
    {
        SaveData data = new SaveData
        {
            playerData = new PlayerData(player),
            enemyData = new EnemyData(enemy),
            enemyKilled = gameManager.enemyKilled,
            coins = UIManager.instance.coin,
            coinMultiplier = UIManager.instance.coinMultiplier,
        };

        foreach (var upgrade in UpgradeManager.instance.upgrades)
        {
            data.upgrades.Add(new UpgradeData(upgrade));
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved!");
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            Debug.Log(saveFilePath);
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded!");
            // Apply saved upgrade data
            foreach (var savedUpgrade in data.upgrades)
            {
                Upgrades upgrade = UpgradeManager.instance.upgrades.Find(u => u.upgradeName == savedUpgrade.upgradeName);
                if (upgrade != null)
                {
                    upgrade.currentLevel = savedUpgrade.currentLevel;
                    upgrade.currentCost = savedUpgrade.currentCost;
                    if (upgrade.currentLevel == 0)
                    {
                        upgrade.currentCost = upgrade.baseCost;
                    }
                    else if (upgrade.currentLevel == upgrade.maxLevel)
                    {
                        upgrade.button.interactable = false;
                    }
                }
            }
            return data;
        }
        else
        {
            Debug.Log(saveFilePath);
            Debug.LogWarning("No save file found!");
            return null;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public PlayerData playerData;
    public EnemyData enemyData;
    public List<UpgradeData> upgrades;
    public int enemyKilled;
    public float coins;
    public float coinMultiplier;
    public SaveData()
    {
        upgrades = new List<UpgradeData>();
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public Elements element;
    public int level;
    public float health, maxHealth, attack, defense, currentXP, xpNeededToLevelUp;
    public WeaponSO equippedWeapon;

    public PlayerData(Player player)
    {
        playerName = player.name;
        element = player.element;
        level = player.level;
        attack = player.attack;
        currentXP = player.currentXP;
        xpNeededToLevelUp = player.xpNeededToLevelUp;

        if (player.equippedWeapon != null)
        {
            equippedWeapon = player.equippedWeapon;
        }
    }
}

[System.Serializable]
public class EnemyData
{
    public EnemyStatsSO stats;
    public Sprite enemySprite;
    public string enemyName;
    public Elements element;
    public int level;
    public float health, maxHealth, attack, defense, xpDrops;
    public int coinDrop;

    public EnemyData(Enemy enemy)
    {
        enemySprite = enemy.spriteRenderer.sprite;
        stats = enemy.enemyStats;
        enemyName = enemy.names;
        element = enemy.element;
        level = enemy.level;
        health = enemy.health;
        maxHealth = enemy.maxHealth;
        defense = enemy.defense;
        xpDrops = enemy.xpDrops;
        coinDrop = enemy.coinDrops;
    }
}

[System.Serializable]
public class UpgradeData
{
    public string upgradeName;
    public int currentLevel;
    public float currentCost;
    public UpgradeType upgradeType;
    public bool isUnlocked;

    public UpgradeData(Upgrades upgrade)
    {
        upgradeName = upgrade.upgradeName;
        currentLevel = upgrade.currentLevel;
        currentCost = upgrade.currentCost;
        upgradeType = upgrade.upgradeSO.upgradeType;
        //isUnlocked = upgrade.currentLevel > 0; // If it has a level, consider it unlocked
    }
}