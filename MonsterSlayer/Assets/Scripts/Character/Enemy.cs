using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public EnemyStatsSO enemyStats;

    public float xpDrops;
    public event Action<float> OnEnemyDeath;

    protected override void Awake()
    {
        base.Awake();

        //if (characterStats is EnemyStatsSO enemystats)
        //{
        //    enemyStats = enemystats;
        //    xpDrops = enemyStats.xpDrop;
        //}
    }

    private void Start()
    {
        if (enemyStats != null)
        {
            characterStats = enemyStats;
            xpDrops = enemyStats.xpDrop;
            health = characterStats.maxHealth; // Initialize health from SO
        }
    }

    public override void OnDie()
    {
        base.OnDie();

        OnEnemyDeath?.Invoke(xpDrops); // Notify subscribers about the death and XP drop    }
        Destroy(gameObject);
    }
}
