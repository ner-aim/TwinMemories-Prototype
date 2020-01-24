using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = 0.6f;

    public event System.Action onAttack;

    CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();


    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));
            if (onAttack != null)
            {
                onAttack();
            }
            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        stats.TakeDamage(myStats.damage.getValue());

    }
}
