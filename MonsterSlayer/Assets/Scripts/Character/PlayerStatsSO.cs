using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsSO : CharacterStatsSO
{
    public float attack;
    public float xpNeededToLevelUp;
    public float currentXP;
}
