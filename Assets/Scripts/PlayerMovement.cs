using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool IsGrounded;
    [SerializeField] private Vector2 jumpDir;
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private AnimationController animationControllerScript;
    [SerializeField] private UIManager UIManagerScript;

    [SerializeField] private LayerMask groundLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        IsGrounded = Physics2D.CircleCast(transform.position, 0.3f, Vector3.down, 0.07f, 1 << 3); // To Check If Player Can Jump Or Not Based On Is He/She On Ground
    }
    void Update()
    {
        if (UIManager.Instance.gamePlayScreen)
        {
            if (fixedJoystick.Horizontal > 0.1f)
            {
                transform.rotation = Quaternion.identity;
                transform.position += new Vector3(fixedJoystick.Horizontal * speed * Time.deltaTime, 0, 0);
            }
            else if (fixedJoystick.Horizontal < -0.1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.position += new Vector3(fixedJoystick.Horizontal * speed * Time.deltaTime, 0, 0);
            }
            if (fixedJoystick.Vertical > 0.1f) transform.position += new Vector3(0, fixedJoystick.Vertical * speed * Time.deltaTime, 0);
            else if (fixedJoystick.Vertical < -0.1f) transform.position += new Vector3(0, fixedJoystick.Vertical * speed * Time.deltaTime, 0);
        }
    }
    public void playerJump()
    {
        if (IsGrounded)
        {
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
            animationControllerScript.JumpAnimation();
        }
    }
}