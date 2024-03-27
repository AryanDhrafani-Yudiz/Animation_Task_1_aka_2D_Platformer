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
            //transform.Translate(fixedJoystick.Horizontal * Time.deltaTime * speed, 0, 0);
        }
        else if (fixedJoystick.Horizontal < -0.1f)
        {
            spriteRenderer.flipX = true;
            transform.position += new Vector3(fixedJoystick.Horizontal * speed * Time.deltaTime, 0, 0);
            //transform.Translate(fixedJoystick.Horizontal * Time.deltaTime * speed, 0, 0);
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
