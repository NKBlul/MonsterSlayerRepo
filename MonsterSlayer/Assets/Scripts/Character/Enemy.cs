using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    public float xpDrops;
    public event Action<float> OnEnemyDeath;

    private void Start()
    {
        health = maxHealth;
    }

    public override void OnDie()
    {
        base.OnDie();

        OnEnemyDeath?.Invoke(xpDrops); // Notify subscribers about the death and XP drop    }
    }
}
