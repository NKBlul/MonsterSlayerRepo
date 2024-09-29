using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected CharacterStatsSO characterStats;

    public Elements element;
    public int level;

    public float calculateDamage(Player attacker, Enemy defender, Elements element1, Elements element2)
    {
        float levelScalingMultiplier = 1 + (attacker.level - defender.level) * 0.1f;
        float damage = Mathf.Max((attacker.attack - defender.defense) *
            ElementInteraction.ElementDamageMultiplier(element1, element2) * levelScalingMultiplier,
            1f);

        return damage;
    }
}
