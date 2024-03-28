using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool IsGrounded;
    [SerializeField] private Vector2 jumpDir;
    [SerializeField] private AnimationController animationControllerScript;

    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 20;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector3.down, 0.07f, 1 << 3);
    }
    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            animationControllerScript.AttackAnimation();

            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                enemy.GetComponent<Enemy>().takeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    public void playerJump()
    {
        if (IsGrounded)
        {
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
            animationControllerScript.JumpAnimation();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}