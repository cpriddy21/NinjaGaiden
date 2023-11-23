using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public Transform crouchAttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.S))
        {
            Attack();
        } else if(Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.S))
        {
            crouchAttack();
        }
    }
    void Attack()
    {
        //Play Animation, detect enemy and damage them
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
        }
    }
    void crouchAttack()
    {
        //Play Animation, detect enemy and damage them
        animator.SetTrigger("crouchAttack");

        Collider2D[] crouchHitEnemies = Physics2D.OverlapCircleAll(crouchAttackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in crouchHitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
        }
    
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
