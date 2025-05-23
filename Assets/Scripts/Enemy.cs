using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 30;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("hurt");
        if(currentHealth <=0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy died");
        animator.SetBool("dead", true);
        
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
