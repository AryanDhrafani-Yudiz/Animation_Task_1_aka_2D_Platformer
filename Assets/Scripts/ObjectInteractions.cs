using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    private SpriteRenderer currentSpriteRenderer;
    [SerializeField] private Sprite doorOpened;
    [SerializeField] private Sprite chestOpened;
    [SerializeField] private Joystick joystickScript;
    [SerializeField] private float amountOfBounce;
    private Rigidbody2D playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BouncePad"))
        {
            playerRigidBody.velocity += new Vector2(playerRigidBody.velocity.x, amountOfBounce);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            currentSpriteRenderer.sprite = doorOpened;
        }
        if (collision.gameObject.CompareTag("Chest"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            currentSpriteRenderer.sprite = chestOpened;
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            joystickScript.axisOptions = AxisOptions.Both;
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = Vector2.zero;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            joystickScript.axisOptions = AxisOptions.Horizontal;
            playerRigidBody.gravityScale = 1;
        }
    }
}
