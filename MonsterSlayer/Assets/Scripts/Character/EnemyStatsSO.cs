using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStatsSO : CharacterStatsSO
{
    public Sprite enemySprite;
    public string names;
    public float health;
    public float maxHealth;
    public float defense;
    public float xpDrop;
    public float difficultyMultiplier;
}