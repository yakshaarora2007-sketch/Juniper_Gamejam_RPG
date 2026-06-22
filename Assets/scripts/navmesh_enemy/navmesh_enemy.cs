using UnityEngine;
using UnityEngine.AI;
public class navmesh_enemy : MonoBehaviour
{
 [SerializeField] Transform target;
 NavMeshAgent agent;


    private void  Start() {
        agent=GetComponent<NavMeshAgent>();
        agent.updateRotation=true;
        agent.updateUpAxis=false;
        
    }
private void Update()
{
    agent.SetDestination(target.position);

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
}
}
