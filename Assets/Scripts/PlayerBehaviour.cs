using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private bool IsGrounded;
    [SerializeField] private Vector2 jumpDir;
    [SerializeField] private AnimationController animationControllerScript;
    private Animator playerAnimator;

    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 20;

    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        maxHealth = 100;
        currentHealth = maxHealth;
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
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        playerAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playerAnimator.SetBool("Dead 0", true);

        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}