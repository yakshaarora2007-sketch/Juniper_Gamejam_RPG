using UnityEngine;
using Pathfinding;
public class Aiagent1 : MonoBehaviour

{
    private AIPath path;
    [SerializeField] private float speed ;
    [SerializeField] private float StopDistance;
    [SerializeField] private Transform target;
    private float distanceToWaypoint;

    private void Start()
    {
        path = GetComponent<AIPath>();
    }

private void Update()
    {
        path.maxSpeed = speed;
        distanceToWaypoint = Vector2.Distance(transform.position, target.position);
        if (distanceToWaypoint < StopDistance)
        {
            path.destination = transform.position;
        }
        else
        {
            path.destination = target.position;
        }
    }

}

