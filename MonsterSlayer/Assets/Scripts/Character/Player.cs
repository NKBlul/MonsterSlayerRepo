using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public PlayerStatsSO playerStats;
    public WeaponSO equippedWeapon;

    public float attack;
    public float currentXP;
    public float xpNeededToLevelUp;

    public void InitializePlayer(PlayerStatsSO stats)
    {
        playerStats = stats;
        characterStats = playerStats;

        element = playerStats.element;
        level = playerStats.level;
        attack = playerStats.attack;
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

    public float calculateDamage(Player attacker, Enemy defender, Elements element1, Elements element2)
    {
        float levelScalingMultiplier = 1 + (attacker.level - defender.level) * 0.1f;
        float damage = Mathf.Max((attacker.attack - defender.defense) *
            ElementInteraction.ElementDamageMultiplier(element1, element2) * levelScalingMultiplier,
            1f);

        return damage;
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

            OnLevelUp();

            Debug.Log($"Level: {level}, Remainingxp: {remainingXP}, next XP needed: {xpNeededToLevelUp}");

            if (remainingXP > 0)
            {
                currentXP = 0;
                GainXP(remainingXP);
            }
        }
        Debug.Log("CurrentXP: " + currentXP);
    }

    private void OnLevelUp()
    {
        attack = Mathf.CeilToInt(attack * 1.05f); // Increase attack by 5%
    }

    public void SubscribeToEnemy(Enemy enemy)
    {
        enemy.OnEnemyDeath += GainXP; // Subscribe to the enemy's death event
    }
}
