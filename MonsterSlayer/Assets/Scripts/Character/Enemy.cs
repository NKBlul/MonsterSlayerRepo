using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : BaseCharacter
{
    public EnemyStatsSO enemyStats;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public string names;
    public float health;
    public float maxHealth;
    public float defense;
    public bool canBeHurt;
    public float xpDrops;
    public int coinDrops;
    public event Action<float> OnEnemyDeath;

    private void Start()
    {
        animator.Play(enemyStats.clip.name);
    }

    public void InitializeEnemy(EnemyStatsSO stats)
    {
        enemyStats = stats;
        characterStats = enemyStats;

        spriteRenderer.sprite = enemyStats.enemySprite;
        element = enemyStats.element;
        names = enemyStats.names;
        level = enemyStats.level;
        health = enemyStats.health;
        maxHealth = enemyStats.maxHealth;
        defense = enemyStats.defense;
        xpDrops = enemyStats.xpDrop;
        coinDrops = enemyStats.coinDrop;

        canBeHurt = true;
    }

    IEnumerator FlashHurtEffect()
    {
        canBeHurt = false;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        canBeHurt = true;
        spriteRenderer.color = Color.white;
    }
    

    public void OnTakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Damage dealth: {damage}, remaining health: {health}");
        UIManager.instance.UpdateHealthBar(health, maxHealth);
        UIManager.instance.UpdateCoinText(coinDrops);
        if (health <= 0)
        {
            OnDie();
        }
        StartCoroutine(FlashHurtEffect());
    }

    public void OnDie()
    {
        Debug.Log("Dead");
        GameManager.instance.enemyKilled++;
        if (GameManager.instance.isBoss)
        {
            Debug.Log("HELO");
            GameManager.instance.bossKilled++;
        }
        OnEnemyDeath?.Invoke(xpDrops); // Notify subscribers about the death and XP drop    }
        Destroy(gameObject);
    }
}
