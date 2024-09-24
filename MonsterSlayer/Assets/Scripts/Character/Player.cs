using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public PlayerStatsSO playerStats;
    public WeaponSO equippedWeapon;

    public float currentXP;
    public float xpNeededToLevelUp;

    private void Start()
    {

    }

    public void InitializePlayer(PlayerStatsSO stats)
    {
        playerStats = stats;
        characterStats = playerStats;

        element = playerStats.element;
        names = playerStats.names;
        level = playerStats.level;
        health = playerStats.health;
        maxHealth = playerStats.maxHealth;
        attack = playerStats.attack;
        defense = playerStats.defense;
        currentXP = playerStats.currentXP;
        xpNeededToLevelUp = playerStats.xpNeededToLevelUp;
    }

    public void EquipWeapon(WeaponSO weapon)
    {
        equippedWeapon = weapon;
        attack = playerStats.attack + equippedWeapon.weaponDamage; // Add weapon attack to player's attack
        Debug.Log($"Equipped {weapon.weaponName} with {equippedWeapon.weaponDamage} attack power.");
    }

    public void DealDamage(Enemy enemy)
    {
        Elements elementUsed;

        if (equippedWeapon == null)
        {
            elementUsed = this.element;
        }
        else
        {
            elementUsed = equippedWeapon.weaponElements;
        }

        // Calculate the damage using the weapon's attack power and element
        float damage = calculateDamage(this, enemy, elementUsed, enemy.element);
        enemy.OnTakeDamage(damage);
    }

    public void GainXP(float xp)
    {
        float newXP = currentXP + xp;
        currentXP = newXP;
        Debug.Log($"New xp: {newXP} = {currentXP} + {xp}");
        if (newXP >= xpNeededToLevelUp)
        {
            level++;
            float remainingXP = newXP - xpNeededToLevelUp;
            xpNeededToLevelUp += 50;
            Debug.Log($"Level: {level}, Remainingxp: {remainingXP}, next XP needed: {xpNeededToLevelUp}");
            if (remainingXP > 0)
            {
                currentXP = 0;
                GainXP(remainingXP);
            }
        }
        Debug.Log("CurrentXP: " + currentXP);
    }

    public void SubscribeToEnemy(Enemy enemy)
    {
        enemy.OnEnemyDeath += GainXP; // Subscribe to the enemy's death event
    }
}
