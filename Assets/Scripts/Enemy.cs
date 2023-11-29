using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public GameObject obj;
    //public GameObject display;
    //public UIInformation info;
    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        //info = display.GetComponent<UIInformation>();
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
    //this.enabled = false;
    obj.SetActive(false);
    //info.updateScore(100);
    }
}
