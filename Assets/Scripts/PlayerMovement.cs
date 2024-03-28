using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 jumpDir;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Joystick joystickScript;
    [SerializeField] private AnimationController animationControllerScript;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int attackDamage = 20;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (fixedJoystick.Horizontal > 0.1f)
        {
            spriteRenderer.flipX = false;
            transform.position += new Vector3(fixedJoystick.Horizontal * speed * Time.deltaTime, 0, 0);
        }
        else if (fixedJoystick.Horizontal < -0.1f)
        {
            spriteRenderer.flipX = true;
            transform.position += new Vector3(fixedJoystick.Horizontal * speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (animationControllerScript.IsGrounded)
            {
                rb.AddForce(jumpDir, ForceMode2D.Impulse);
                animationControllerScript.JumpAnimation();
            }
        }
        if (fixedJoystick.Vertical > 0.1f) transform.position += new Vector3(0, fixedJoystick.Vertical * speed * Time.deltaTime, 0);
        else if (fixedJoystick.Vertical < -0.1f) transform.position += new Vector3(0, fixedJoystick.Vertical * speed * Time.deltaTime, 0);

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        animationControllerScript.AttackAnimation();

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void playerJump()
    {
        if (animationControllerScript.IsGrounded)
        {
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
            animationControllerScript.JumpAnimation();
        }
    }
}
