using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] List<EnemyStatsSO> basicEnemy;
    [SerializeField] List<EnemyStatsSO> bossEnemy;

    [SerializeField] PlayerStatsSO playerStats;
    [SerializeField] EnemyStatsSO enemyStats;

    public Player player;
    public Enemy enemy;

    public int enemyKilled;
    public bool isBoss;
    public int bossKilled;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGame();
    }

    void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab);
        player = playerObject.GetComponent<Player>();

        player.InitializePlayer(playerStats);
    }

    void SpawnEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefab);
        enemy = enemyObject.GetComponent<Enemy>();
        EnemyStatsSO enemyStatUsed = null;

        if (enemyKilled % 5 != 0 || enemyKilled == 0)
        {
            enemyStatUsed = GetRandomEnemy();
            isBoss = false;
        }
        else if (enemyKilled % 5 == 0)
        {
            enemyStatUsed = GetRandomBoss();
            isBoss = true;
        }

        enemy.InitializeEnemy(enemyStatUsed);
        AdjustEnemyStats(enemy, bossKilled, isBoss);
        player.SubscribeToEnemy(enemy);
        enemy.OnEnemyDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath(float xp)
    {
        // When the enemy dies, give the player XP
        player.GainXP(xp);
        SpawnEnemy();
    }

    EnemyStatsSO GetRandomEnemy()
    {
        int randomEnemyIndex = Random.Range(0, basicEnemy.Count);

        return basicEnemy[randomEnemyIndex];
    }

    EnemyStatsSO GetRandomBoss()
    {
        int randBossIndex = Random.Range(0, bossEnemy.Count);

        return bossEnemy[randBossIndex];
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveGame(player, enemy, this); // Save when quitting
        Debug.Log("Quit");
    }

    public void LoadGame()
    {
        SaveData saveData = SaveSystem.LoadGame();

        if (saveData != null)
        {
            SpawnPlayer();
            SpawnEnemy();

            // Apply the saved player and enemy data
            ApplyLoadedPlayerData(saveData.playerData);
            ApplyLoadedEnemyData(saveData.enemyData);

            enemy.SetName();
            enemy.UpdateHealthBar();

            // Load other game progress
            enemyKilled = saveData.enemyKilled;
        }
        else
        {
            SpawnPlayer();
            SpawnEnemy();
        }
    }

    void ApplyLoadedPlayerData(PlayerData playerData)
    {
        player.element = playerData.element;
        player.level = playerData.level;
        player.health = playerData.health;
        player.maxHealth = playerData.maxHealth;
        player.attack = playerData.attack;
        player.defense = playerData.defense;
        player.currentXP = playerData.currentXP;
        player.xpNeededToLevelUp = playerData.xpNeededToLevelUp;

        if (playerData.equippedWeapon != null)
        {
            player.EquipWeapon(playerData.equippedWeapon);
        }

        Debug.Log("Player Data Loaded");
    }

    void ApplyLoadedEnemyData(EnemyData enemyData)
    {
        enemy.enemyStats = enemyData.stats;
        enemy.spriteRenderer.sprite = enemyData.enemySprite;
        enemy.names = enemyData.enemyName;
        enemy.element = enemyData.element;
        enemy.level = enemyData.level;
        enemy.health = enemyData.health;
        enemy.maxHealth = enemyData.maxHealth;
        enemy.attack = enemyData.attack;
        enemy.defense = enemyData.defense;
        enemy.xpDrops = enemyData.xpDrops;

        Debug.Log("Enemy data loaded");
    }


    //public void AdjustEnemyStats(Enemy enemy, int enemiesKilled, bool isBoss)
    //{
    //    float difficultyMultiplier = 1 + (enemiesKilled / 5) * 0.1f;
    //    float bossMultiplier = isBoss ? 1.1f : 1.0f;
    //
    //    enemy.health = enemy.health * difficultyMultiplier * bossMultiplier;
    //    enemy.maxHealth = enemy.maxHealth * difficultyMultiplier * bossMultiplier;
    //    enemy.attack = enemy.attack * difficultyMultiplier * bossMultiplier;
    //    enemy.defense = enemy.defense * difficultyMultiplier * bossMultiplier;
    //    enemy.xpDrops = enemy.xpDrops * difficultyMultiplier * bossMultiplier;
    //
    //    Debug.Log($"Health: {enemy.health}, Max Health: {enemy.maxHealth}, Attack: {enemy.attack}, Defense: {enemy.defense}," +
    //        $"XPDrop: {enemy.xpDrops}");
    //}

    public void AdjustEnemyStats(Enemy enemy, int bossKills, bool isBoss)
    {
        // Apply scaling based on the number of bosses killed
        float scalingFactor = 1 + (bossKills * 0.25f);
        float bossMultiplier = isBoss ? 1.25f : 1.0f;

        if (enemyKilled > 5)
        {
            enemy.health = Mathf.CeilToInt(enemy.health * scalingFactor * bossMultiplier);
            enemy.maxHealth = Mathf.CeilToInt(enemy.maxHealth * scalingFactor * bossMultiplier);
            enemy.attack = Mathf.CeilToInt(enemy.attack * scalingFactor * bossMultiplier);
            enemy.defense = Mathf.CeilToInt(enemy.defense * scalingFactor * bossMultiplier);
            enemy.xpDrops = Mathf.CeilToInt(enemy.xpDrops * scalingFactor * bossMultiplier);
        }
        
        Debug.Log($"Enemy stats after scaling: Health: {enemy.health}, Max Health: {enemy.maxHealth}, Attack: {enemy.attack}, Defense: {enemy.defense}, XPDrop: {enemy.xpDrops}");
    }
}
