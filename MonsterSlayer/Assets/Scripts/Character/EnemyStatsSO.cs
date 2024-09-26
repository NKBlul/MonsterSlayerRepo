using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStatsSO : CharacterStatsSO
{
    public Sprite enemySprite;
    //public List<Sprite> sprites;
    public float xpDrop;
    public float difficultyMultiplier;
}