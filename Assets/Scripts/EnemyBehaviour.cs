using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    [SerializeField] private int maxHealthOfEnemy;
    [SerializeField] private Animator animationController;

    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask playerLayers;
    [SerializeField] private int attackDamage = 20;
    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        maxHealth = maxHealthOfEnemy;
        currentHealth = maxHealth;
    }
    public void Attack()
    {
        enemyAnimator.SetBool("Walk", false); enemyAnimator.SetBool("Run", false);
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] playersHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

            foreach (Collider2D player in playersHit)
            {
                if (player.GetComponent<IDamageable>().currentHealth > 0)
                {
                    enemyAnimator.SetTrigger("Attack");
                    player.GetComponent<IDamageable>().takeDamage(attackDamage);
                }
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animationController.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animationController.SetBool("isDead", true);

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