using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat_Mobile : MonoBehaviour
{
    [SerializeField] private Abilities_Mobile abilities_Mobile;
    [SerializeField] private Player_Mobile player;
    [SerializeField]private Stats stats; 
    [SerializeField] private Animator anim;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack Variables")]
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;

    private void OnDrawGizmosSelected() {
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) != true) return;
        if(abilities_Mobile.isUseSkill == true) return;
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);
        targetEnemy = player.targetEnemy;
        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= 3f)
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
    private IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(0.18f);
        if(targetEnemy == null) yield return null; 
        stats.TakeDamage(targetEnemy, stats.damage);
        nextAttackTime = Time.time + attackInterval;
        performMeleeAttack = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttacking", false);
    }

    public void Rotation(Vector3 lookAtPosition)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                                                    ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
}
