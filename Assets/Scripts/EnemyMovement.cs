using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float distanceToPatrol;
    [SerializeField] private float speed;
    private Vector3 startingPosition;
    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        startingPosition = transform.position;
    }
    private void Update()
    {
        if (Mathf.Abs(playerTransform.position.y - transform.position.y) < 0.1f)
        {
            ChasePlayer();
        }
        else Patrol();
    }
    void Patrol()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
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
        if (Mathf.Abs(playerTransform.position.x - transform.position.x) < 5f)
        {
            transform.Translate(playerTransform.position.x * speed * Time.deltaTime, 0, 0);
        }
    }
}
