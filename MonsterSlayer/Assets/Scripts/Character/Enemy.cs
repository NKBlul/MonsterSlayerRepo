using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public EnemyStatsSO enemyStats;

    public float xpDrops;
    public event Action<float> OnEnemyDeath;

    private void Start()
    {

    }

    public void InitializeEnemy(EnemyStatsSO stats)
    {
        enemyStats = stats;
        characterStats = enemyStats;

        element = enemyStats.element;
        names = enemyStats.name;
        level = enemyStats.level;
        health = enemyStats.health;
        maxHealth = enemyStats.maxHealth;
        attack = enemyStats.attack;
        defense = enemyStats.defense;
        xpDrops = enemyStats.xpDrop;
    }

    public override void OnDie()
    {
        base.OnDie();

        OnEnemyDeath?.Invoke(xpDrops); // Notify subscribers about the death and XP drop    }
        Destroy(gameObject);
    }
}
