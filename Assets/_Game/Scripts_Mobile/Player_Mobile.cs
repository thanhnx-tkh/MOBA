using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player_Mobile : MonoBehaviour
{
    private Outline highlightOutline;
    public float moveSpeed = 3.0f;
    public Joystick joystick;
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;

    private NavMeshAgent agent;
    private Animator anim;

    public GameObject targetEnemy;

    public Button fightButton;

    public float stoppingDistance;

    public bool isMoveJoystick;

    public Rigidbody rb;

    private bool isMoving = true;


    void Start()
    {
        isMoveJoystick = true;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fightButton.onClick.AddListener(FightButton);
    }

    void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) != true) return;
        Animation();

        if (isMoving)
        {
            Move();
        }

    }
    public void Move()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            isMoveJoystick = true;
            if (highlightOutline != null)
            {
                highlightOutline.enabled = false;
            }
            targetEnemy = null;
            agent.ResetPath();
            rb.velocity = moveDirection * moveSpeed;

            float speed = moveDirection.magnitude;

            anim.SetFloat("Speed", speed);

            if (rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
        }
        else if (isMoveJoystick == true)
        {
            rb.velocity = Vector3.zero;
            anim.SetFloat("Speed", 0.0f);
        }
    }
    public void FightButton()
    {
        GetNearestEnemy();
        if (targetEnemy != null)
        {
            MoveTowardsEnemy(targetEnemy);
        }

    }

    public void GetNearestEnemy()
    {
        List<Enemy> enemies = GameManager.Ins.enemies;
        Enemy nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        targetEnemy = nearestEnemy.gameObject;
    }

    public void MoveTowardsEnemy(GameObject enemy)
    {
        isMoveJoystick = false;
        agent.isStopped = false;
        agent.SetDestination(enemy.transform.position);
        agent.stoppingDistance = stoppingDistance;

        Rotation(enemy.transform.position);
        if (enemy == null) return;

        // Active Highlight
        highlightOutline = enemy.GetComponent<Outline>();
        highlightOutline.enabled = true;
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
        if (isMoveJoystick == true) return;
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, 0.01f, Time.deltaTime);
    }
    public void StopMoving()
    {
        isMoving = false;
        agent.ResetPath();
    }

    public void ActiveMoving()
    {
        isMoving = true;
        agent.ResetPath();
    }
}
