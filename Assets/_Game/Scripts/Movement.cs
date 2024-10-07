using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private ParticleSystem clickEffect;
    [SerializeField] private NavMeshAgent agent;
    public float rotateSpeedMovement = 0.05f;
    private float rotateVelocity;

    private bool isMoving = true;

    public Animator anim;
    float motionSmoothTime = 0.1f;

    [Header("Enemy Targeting")]
    public GameObject targetEnemy;
    public float stoppingDistance;
    [SerializeField] private HighlightManager hmScript;

    public Camera cameraMain;

    private void OnDrawGizmosSelected()
    {
        hmScript = GetComponent<HighlightManager>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start() {
        clickEffect.Stop();
    }
    private void Update()
    {
        if(GameManager.IsState(GameState.GamePlay) != true) return;
        Animation();
        Move();
    }
    public void Animation()
    {
        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
    }

    public void Move()
    {
        if(!isMoving) return;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(cameraMain.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Ground")
                {
                    MoveToPosition(hit.point);
                    clickEffect.transform.position = hit.point + new Vector3(0, 0.1f, 0);
					clickEffect.Play();
                }
                else if (hit.collider.CompareTag("Enemy"))
                {
                    MoveTowardsEnemy(hit.collider.gameObject);
                }
            }
        }
        if (targetEnemy != null)
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) > stoppingDistance)
            {
                agent.SetDestination(targetEnemy.transform.position);
            }
        }
    }
    public void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        agent.stoppingDistance = 0;
        Rotation(position);

        if (targetEnemy != null)
        {
            hmScript.DeselectHighlight();
            targetEnemy = null;
        }
    }
    public void MoveTowardsEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
        agent.SetDestination(targetEnemy.transform.position);
        agent.stoppingDistance = stoppingDistance;

        Rotation(targetEnemy.transform.position);
        if(targetEnemy == null) return;
        hmScript.SelectedHighlight();
    }
    public void Rotation(Vector3 lookAtPosition)
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(lookAtPosition - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y,
                                                    ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void StopMoving(){
        isMoving = false;
        agent.ResetPath();
    }

    public void ActiveMoving(){
        isMoving = true;
        agent.ResetPath();
    }

}