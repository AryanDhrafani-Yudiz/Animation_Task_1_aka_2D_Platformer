using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private FixedJoystick fixedJoystick;
    private Animator playerAnimator;
    private float deadZoneOffset = 0.1f;
    private float runningOffset = 0.7f;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
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
    public void AttackAnimation()
    {
        playerAnimator.SetTrigger("Attack");
    }
    public void JumpAnimation()
    {
        playerAnimator.SetTrigger("Jump");
    }
}