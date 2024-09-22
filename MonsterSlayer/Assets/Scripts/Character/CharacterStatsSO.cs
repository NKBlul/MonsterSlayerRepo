using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Scritables/Stats", order = 1)]
public class CharacterStatsSO : ScriptableObject
{
    public Elements element;
    public string names;
    public int level;
    public float health;
    public float maxHealth;
    public float attack;
    public float defense;
}
