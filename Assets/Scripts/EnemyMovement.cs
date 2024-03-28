using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float distanceToPatrol;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    private Vector3 startingPosition;
    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        startingPosition = transform.position;
    }
    private void Update()
    {
        if (Mathf.Abs(playerTransform.position.y - transform.position.y) < 0.1f && Mathf.Abs(playerTransform.position.x - transform.position.x) < 7f)
        {
            ChasePlayer();
        }
        else Patrol();
    }
    void Patrol()
    {
        transform.Translate(patrolSpeed * Time.deltaTime, 0, 0);
        enemyAnimator.SetBool("Run", true);
        if (transform.position.x > startingPosition.x + distanceToPatrol)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (transform.position.x < startingPosition.x - distanceToPatrol)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    void ChasePlayer()
    {
        if (Mathf.Abs(playerTransform.position.x - transform.position.x) > 1f)
        {
            if (playerTransform.position.x - transform.position.x > 0f) transform.rotation = Quaternion.Euler(Vector3.zero);
            else transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        }
        else AttackPlayer();
    }
    void AttackPlayer()
    {
        enemyAnimator.SetBool("Run", false);
        enemyAnimator.SetTrigger("Attack");
    }
}
