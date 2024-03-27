using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    public bool IsGrounded;
    private float deadZoneOffset = 0.1f;
    private float runningOffset = 0.7f;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerAnimator.SetBool("Dead 0", true);
        }
        if (fixedJoystick.Horizontal > deadZoneOffset || fixedJoystick.Horizontal < -deadZoneOffset)
        {
            if (fixedJoystick.Horizontal > runningOffset || fixedJoystick.Horizontal < -runningOffset)
            {
                playerAnimator.SetBool("Run", true);
                playerAnimator.SetBool("Walk", false);
            }
            else
            {
                playerAnimator.SetBool("Walk", true);
                playerAnimator.SetBool("Run", false);
            }
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
            playerAnimator.SetBool("Run", false);
        }
    }
    public void JumpAnimation()
    {
        playerAnimator.SetTrigger("Jump");
        IsGrounded = false;
    }
    public void AttackAnimation()
    {
        playerAnimator.SetTrigger("Attack");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            playerAnimator.ResetTrigger("Jump");
        }
    }
}