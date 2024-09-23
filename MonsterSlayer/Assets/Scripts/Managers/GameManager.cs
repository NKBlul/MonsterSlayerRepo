using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

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
        player.SubscribeToEnemy(enemy); // Subscribe the player to the enemy's death event
    }
}
