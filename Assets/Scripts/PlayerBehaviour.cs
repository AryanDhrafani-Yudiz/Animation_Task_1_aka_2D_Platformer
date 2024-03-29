using UnityEngine;
using TMPro;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationController animationControllerScript;
    [SerializeField] private UIManager uiManagerScript;
    private Animator playerAnimator;
    [SerializeField] TextMeshProUGUI hpObject;

    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 20;

    [SerializeField] private int maxHealthOfPlayer;
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        maxHealth = maxHealthOfPlayer;
        currentHealth = maxHealth;
        hpObject.text = currentHealth.ToString();
    }
    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            animationControllerScript.AttackAnimation();

            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in enemiesHit)
            {
                enemy.GetComponent<IDamageable>().takeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        hpObject.text = currentHealth.ToString();
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
        uiManagerScript.OnGameOverScreen();
        this.enabled = false;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}