using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private Movement moveScript;
    [SerializeField]private Stats stats; 
    [SerializeField] private Animator anim;
    [Header("Target")]
    public GameObject targetEnemy;
    [Header("Melee Attack Variables")]
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;

    private void OnDrawGizmosSelected() {
        moveScript = GetComponent<Movement>();
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) != true) return;
        //Calculates the attack speed and interval between each attack 
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);
        targetEnemy = moveScript.targetEnemy;
        //Perform the Melee Attack if in Range
        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= moveScript.stoppingDistance)
                StartCoroutine(MeleeAttackInterval());
    }
    private IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        anim.SetBool("isAttacking", true);
        StartCoroutine(MeleeAttack());
        yield return new WaitForSeconds(attackInterval);
        if (targetEnemy = null)
        {
            anim.SetBool("isAttacking", false);
            performMeleeAttack = true;
        }
    }
    //CALL IN THE ANIMATION EVENT
    private IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(0.18f);
        if(targetEnemy == null) yield return null; 
        stats.TakeDamage(targetEnemy, stats.damage);
        //Set the next attack time
        nextAttackTime = Time.time + attackInterval;
        performMeleeAttack = true;
        //Stop calling the attack animation
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttacking", false);
    }
}
