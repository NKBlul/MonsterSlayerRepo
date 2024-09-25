using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStatsSO : CharacterStatsSO
{
    public Sprite enemySprite;
    public float xpDrop;
    public float difficultyMultiplier;
}