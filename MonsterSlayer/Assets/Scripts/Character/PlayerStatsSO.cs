using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsSO : CharacterStatsSO
{
    public float xpRequiredForNextLevel;
    public float currentXP;
}
