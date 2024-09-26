using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : BaseCharacter
{
    public EnemyStatsSO enemyStats;

    public SpriteRenderer spriteRenderer;
    public Sprite hurtSprite;
    public bool canBeHurt;
    public float xpDrops;
    public event Action<float> OnEnemyDeath;

    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] Image healthBar;

    private void Start()
    {

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
        attack = enemyStats.attack;
        defense = enemyStats.defense;
        xpDrops = enemyStats.xpDrop;

        canBeHurt = true;
    }

    public override void OnTakeDamage(float damage)
    {
        base.OnTakeDamage(damage);
        UpdateHealthBar();
        StartCoroutine(ReturnToOriginalSprite());
    }

    public override void OnDie()
    {
        base.OnDie();
        GameManager.instance.enemyKilled++;
        if (GameManager.instance.isBoss)
        {
            Debug.Log("HELO");
            GameManager.instance.bossKilled++;
        }
        OnEnemyDeath?.Invoke(xpDrops); // Notify subscribers about the death and XP drop    }
        Destroy(gameObject);
    }

    IEnumerator ReturnToOriginalSprite()
    {
        canBeHurt = false;
        //spriteRenderer.sprite = hurtSprite;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        canBeHurt = true;
        //spriteRenderer.sprite = enemyStats.enemySprite;
        spriteRenderer.color = Color.white;
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
    }
}
