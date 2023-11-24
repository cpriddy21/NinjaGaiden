using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int maxHealth = 1;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
            Die();
    }
    void Die()
    {
    GetComponent<Collider2D>().enabled = false;
    this.enabled = false;
    }
}
