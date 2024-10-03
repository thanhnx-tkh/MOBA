using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ZoneAttackEnemy zoneAttack;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Stats stats;
    [SerializeField] private Animator anim;
    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack Variables")]
    public bool performMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;

    private float rotateVelocity;

    public float rotateSpeedMovement = 0.05f;

    float motionSmoothTime = 0.1f;


    private void Update()
    {
        Animation();
        attackInterval = stats.attackSpeed / ((500 + stats.attackSpeed) * 0.01f);
        targetEnemy = zoneAttack.target;
        if (targetEnemy == null) agent.stoppingDistance = 0;

        if (targetEnemy != null && performMeleeAttack && Time.time > nextAttackTime)
        {
            MoveTowardsEnemy(targetEnemy);
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= 3f)
            {
                //agent.velocity = Vector3.zero;
                //agent.isStopped = true;
                StartCoroutine(MeleeAttackInterval());
            }
        }
    }
    public IEnumerator MeleeAttackInterval()
    {

        performMeleeAttack = false;

        anim.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackInterval);

        if (targetEnemy = null)
        {
            anim.SetBool("isAttacking", false);
            performMeleeAttack = true;
        }
    }
    //CALL IN THE ANIMATION EVENT
    private void MeleeAttack()
    {
        if (targetEnemy == null) return;
        stats.TakeDamage(targetEnemy, stats.damage);
        //Set the next attack time
        nextAttackTime = Time.time + attackInterval;
        performMeleeAttack = true;
        //Stop calling the attack animation 
        anim.SetBool("isAttacking", false);
    }


    public void MoveTowardsEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
        agent.SetDestination(targetEnemy.transform.position);
        agent.stoppingDistance = 3f;

        Rotation(targetEnemy.transform.position);
    }

    public void Rotation(Vector3 lookAtPosition)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                                                    ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
    public void Animation()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        Debug.Log("" + speed);
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }
}
