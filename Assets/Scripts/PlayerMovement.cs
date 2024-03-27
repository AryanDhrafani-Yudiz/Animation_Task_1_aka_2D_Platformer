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
        #region 
        //if (fixedJoystick.Vertical > 0.1f) transform.Translate(Vector3.up * Time.deltaTime * speed * fixedJoystick.Vertical);
        //else if (fixedJoystick.Vertical < -0.1f) transform.Translate(Vector3.down * Time.deltaTime * speed * fixedJoystick.Vertical);

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    joystickScript.axisOptions = AxisOptions.Horizontal;
        //}
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    joystickScript.axisOptions = AxisOptions.Vertical;
        //}
        #endregion
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
