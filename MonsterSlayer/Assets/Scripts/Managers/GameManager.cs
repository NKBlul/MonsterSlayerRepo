using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] List<EnemyStatsSO> basicEnemy;
    [SerializeField] List<EnemyStatsSO> bossEnemy;

    public Player player;
    public Enemy enemy;

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
        SpawnPlayer();
        SpawnEnemy();
    }

    void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab);
        player = playerObject.GetComponent<Player>();
    }

    void SpawnEnemy()
    {
        GameObject enemyObject = Instantiate(enemyPrefab);
        enemy = enemyObject.GetComponent<Enemy>();

        enemy.enemyStats = GetRandomEnemy();

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
}
