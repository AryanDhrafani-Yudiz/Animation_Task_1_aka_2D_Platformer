using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;
    private Animator playerAnimator;
    private Rigidbody2D playerRigidBody;
    public bool IsGrounded;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (fixedJoystick.Horizontal != 0)
        {
            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }
    }
    public void PlayerJumpAnimation()
    {
        playerAnimator.SetTrigger("Jump");
        IsGrounded = false;
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