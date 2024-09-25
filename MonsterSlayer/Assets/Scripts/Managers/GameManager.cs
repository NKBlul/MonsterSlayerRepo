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

        if (enemyKilled % 5 != 0 || enemyKilled == 0)
        {
            EnemyStatsSO randomEnemy = GetRandomEnemy();
            Debug.Log(randomEnemy);
            enemy.InitializeEnemy(randomEnemy);
        }
        else if (enemyKilled % 5 == 0)
        {
            EnemyStatsSO bossEnemy = GetRandomBoss();
            Debug.Log(bossEnemy);
            enemy.InitializeEnemy(bossEnemy);
        }

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
}
