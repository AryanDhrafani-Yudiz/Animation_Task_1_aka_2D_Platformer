using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float distanceToPatrol;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;
    private Vector3 startingPosition;
    private Animator enemyAnimator;
    private EnemyBehaviour enemyBehaviourScript;

    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyBehaviourScript = GetComponent<EnemyBehaviour>();
        startingPosition = transform.position;
    }
    private void Update()
    {
        if (enemyBehaviourScript.currentHealth > 0)
        {
            if (Mathf.Abs(playerTransform.position.y - transform.position.y) < 0.1f && Mathf.Abs(playerTransform.position.x - transform.position.x) < 7f)
            {
                ChasePlayer();
            }
            else Patrol();
        }
    }
    void Patrol()
    {
        transform.Translate(patrolSpeed * Time.deltaTime, 0, 0);
        enemyAnimator.SetBool("Walk", true); enemyAnimator.SetBool("Run", false);
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
        enemyAnimator.SetBool("Walk", false); enemyAnimator.SetBool("Run", true);
        if (Mathf.Abs(playerTransform.position.x - transform.position.x) > 1f)
        {
            if (playerTransform.position.x - transform.position.x > 0f) transform.rotation = Quaternion.Euler(Vector3.zero);
            else transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerTransform.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        }
        else enemyBehaviourScript.Attack();
    }
}