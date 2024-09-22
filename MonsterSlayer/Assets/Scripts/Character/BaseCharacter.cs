using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public CharacterStatsSO characterStats;

    public Elements element;
    protected string names;
    protected int level;
    protected float health;
    protected float maxHealth;
    protected float attack;
    protected float defense;

    private void Awake()
    {
        element = characterStats.element;
        names = characterStats.name;
        level = characterStats.level;
        health = characterStats.health;
        maxHealth = characterStats.maxHealth;
        attack = characterStats.attack;
        defense = characterStats.defense;
    }

    public float calculateDamage(BaseCharacter attacker, BaseCharacter defender, Elements element1, Elements element2)
    {
        float levelScalingMultiplier = 1 + (attacker.level - defender.level) * 0.1f;
        float damage = Mathf.Max((attacker.attack - defender.defense) *
            ElementInteraction.ElementDamageMultiplier(element1, element2) * levelScalingMultiplier,
            1f);

        return damage;
    }

    public virtual void OnTakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        Debug.Log("Dead");
    }
}
