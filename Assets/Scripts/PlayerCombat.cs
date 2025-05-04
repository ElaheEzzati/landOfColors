using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private bool hasYellowMagic = false;

    void Update()
    {
        if (hasYellowMagic)
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                Attack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("yellow"))
        {
            hasYellowMagic = true;
            Debug.Log("Magic Collected!");
            Destroy(other.gameObject); // Magic disappears
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit enemy");
            enemy.GetComponent<Enemy>().TakeDamage(10);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
