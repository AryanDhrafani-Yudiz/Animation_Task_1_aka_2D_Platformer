using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private Animator animationController;

    void Start()
    {
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
}
