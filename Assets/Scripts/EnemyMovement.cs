using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float distanceToPatrol;
    [SerializeField] private float speed;
    private float direction = 1.0f;
    private Vector3 patrolPositionLimit;

    private void Start()
    {
        patrolPositionLimit = transform.position;
    }
    private void Update()
    {
        Patrol();
    }
    void Patrol()
    {
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
        if (transform.position.x > patrolPositionLimit.x + distanceToPatrol)
        {
            direction = 1;
        }
        else if (transform.position.x < patrolPositionLimit.x - distanceToPatrol)
        {
            direction = -1;
        }
    }
}
