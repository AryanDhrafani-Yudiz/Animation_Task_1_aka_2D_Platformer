using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    [SerializeField] private Animator animationController;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int maxHealthOfEnemy;

    void Start()
    {
        maxHealth = maxHealthOfEnemy;
        currentHealth = maxHealth;
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