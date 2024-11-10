using System.Collections;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public float attackSpeed = 5.0f;
    public float damage = 1f;

    private void Start()
    {
        StartCoroutine(AttackEnemy());
    }

    private IEnumerator AttackEnemy()
    {
        yield return new WaitForSeconds(attackSpeed);
        DamageEnemy();
        StartCoroutine(AttackEnemy());
    }

    private void DamageEnemy()
    {
        GameManager.instance.enemy.OnTakeDamage(damage);
    }
}
