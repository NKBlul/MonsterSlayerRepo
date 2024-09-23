using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    public float currentXP;
    public float xpNeededToLevelUp;

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
        Debug.Log(currentXP);
    }

    public void SubscribeToEnemy(Enemy enemy)
    {
        enemy.OnEnemyDeath += GainXP; // Subscribe to the enemy's death event
    }
}
