using UnityEngine;

public enum Elements
{
    Elementless,
    Fire,
    Water,
    Earth,
    Ice,
    Air,
    Shadow,
    Light,
}

public static class ElementInteraction
{
    #region element interaction explanation
    /*
     Fire
     Strengths:     
     Strong against Ice (melts ice).     
     Strong against Earth (can burn through foliage/vegetation).
     
     Water
     Strengths:
     Strong against Fire (extinguishes flames).     
     Strong against Air (creates storms, dampens winds).
     
     Earth   
     Strengths:     
     Strong against Water (soaks up moisture, forms barriers).   
     Strong against Air (blocks wind, stabilizes).
     
     Air (Wind)
     Strengths:     
     Strong against Fire (fans flames, can blow out fires).
     Strong against Ice (can create blizzards that break ice).
     
     Ice
     Strengths:  
     Strong against Earth (can freeze soil, hardens).
     Strong against Water (creates barriers, freezes lakes).
     */
    #endregion

    private static readonly float[,] elementMultipliers = new float[,]
    {
        //EL,  Fire, Water, Earth, Ice, Air, Shadow, Light
        {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f}, //EL
        {1.0f, 1.0f, 1.5f, 0.67f, 0.67f, 1.5f, 1.0f, 1.0f}, //Fire
        {1.0f, 0.67f, 1.0f, 1.5f, 1.5f, 0.67f, 1.0f, 1.0f}, //Water
        {1.0f, 1.5f, 0.67f, 1.0f, 1.5f, 0.67f, 1.0f, 1.0f}, //Earth
        {1.0f, 1.5f, 0.67f, 0.67f, 1.0f, 1.5f, 1.0f, 1.0f}, //Ice
        {1.0f, 0.67f, 1.5f, 1.5f, 0.67f, 1.0f, 1.0f, 1.0f}, //Air
        {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.5f}, //Shadow
        {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.5f, 1.0f}, //Light
    };

    public static float ElementDamageMultiplier(Elements attacker, Elements defender)
    {
        int attackerIndex = (int)attacker;
        int defenderIndex = (int)defender;
        float multiplier = elementMultipliers[defenderIndex, attackerIndex];

        Debug.Log($"Attacker element: {attacker}, Defender element: {defender}, multiplier: {multiplier}");
        return multiplier;
    }
}
