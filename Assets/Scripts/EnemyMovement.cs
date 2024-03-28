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


    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private int attackDamage = 20;
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
        else Attack();
    }
    public void Attack()
    {
        enemyAnimator.SetBool("Run", false);
        if (Time.time >= nextAttackTime)
        {
            enemyAnimator.SetTrigger("Attack");

            Collider2D[] playersHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

            foreach (Collider2D player in playersHit)
            {
                player.GetComponent<PlayerBehaviour>().takeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
}
