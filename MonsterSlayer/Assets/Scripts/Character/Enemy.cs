using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{
    private void Start()
    {
        health = maxHealth;
    }
}
