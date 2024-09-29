using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/saveData.json";

    public static void SaveGame(Player player, Enemy enemy, GameManager gameManager)
    {
        SaveData data = new SaveData
        {
            playerData = new PlayerData(player),
            enemyData = new EnemyData(enemy),
            enemyKilled = gameManager.enemyKilled
        };

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
    public int enemyKilled;
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
    }
}