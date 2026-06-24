using UnityEngine;
using UnityEngine.AI;
public class navmesh_enemy : MonoBehaviour
{

 [SerializeField] Transform target; //taregt follower
 [SerializeField] float detectionRange = 7f; // Range in which follows
[SerializeField] float attackRange = 1.5f; // range of attack
float attackTimer;
[SerializeField] float attackCooldown = 1f; //time between each attack

[SerializeField] LayerMask obstacleLayer;// walls
[SerializeField] LayerMask losLayer;

Vector3 lastKnownPosition;

[SerializeField] float memoryDuration = 3f;
float memoryTimer;
 NavMeshAgent agent;
enum EnemyState
{
    Idle,
    Chase,
    Attack
}

EnemyState currentState;

    private void  Start() {
        agent=GetComponent<NavMeshAgent>();
        agent.updateRotation=true;
        agent.updateUpAxis=false;

        currentState = EnemyState.Idle;
        
    }
private void Update()
{
    
    attackTimer += Time.deltaTime;
    memoryTimer -= Time.deltaTime;
    float distance =
        Vector2.Distance(
            transform.position,
            target.position
        );
    
    
    if(distance > detectionRange)
    {
        currentState = EnemyState.Idle;
    }
    else if(HasLineOfSight())
    {
        lastKnownPosition = target.position;

        memoryTimer = memoryDuration;

        if(distance > attackRange)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Attack;
        }
    }
    else if(memoryTimer > 0)
    {
        currentState = EnemyState.Chase;
    }
    else
    {
        currentState = EnemyState.Idle;
    }



    switch(currentState)
    {
        case EnemyState.Idle:
            agent.ResetPath();
            break;

        case EnemyState.Chase:
                    if(HasLineOfSight())
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(lastKnownPosition);
            }

            break;

        case EnemyState.Attack:

            agent.ResetPath();

            if(attackTimer >= attackCooldown)
            {
                Debug.Log("ATTACK");

                attackTimer = 0f;
            }

            break;
    }

    Vector2 direction = agent.velocity.normalized;

    if (direction.sqrMagnitude > 0.01f)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            10f * Time.deltaTime
        );
    }
bool HasLineOfSight()
{
    Vector2 direction =
        target.position - transform.position;

    RaycastHit2D hit = Physics2D.Raycast(
        transform.position,
        direction.normalized,
        detectionRange,
        losLayer
    );

    Debug.DrawRay(
    transform.position,
    direction.normalized * detectionRange,
    Color.red
    );
    if(hit.collider != null)
{
    Debug.Log("Hit: " + hit.collider.name);
}
else
{
    Debug.Log("Hit Nothing");
}

    if(hit.collider == null)
    {
         return false;
       }
    return hit.transform ==target;
    
}

}
}
