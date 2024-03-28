using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick fixedJoystick;
    private SpriteRenderer spriteRenderer;

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
        if (fixedJoystick.Vertical > 0.1f) transform.position += new Vector3(0, fixedJoystick.Vertical * speed * Time.deltaTime, 0);
        else if (fixedJoystick.Vertical < -0.1f) transform.position += new Vector3(0, fixedJoystick.Vertical * speed * Time.deltaTime, 0);
    }
}