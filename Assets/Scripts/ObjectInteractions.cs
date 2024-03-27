using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    private SpriteRenderer currentSpriteRenderer;
    [SerializeField] private Sprite doorOpened;
    [SerializeField] private Sprite chestOpened;
    [SerializeField] private Joystick joystickScript;
    private Rigidbody2D playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            currentSpriteRenderer.sprite = doorOpened;
        }
        if (collision.gameObject.CompareTag("Stairs"))
        {
            joystickScript.axisOptions = AxisOptions.Both;
            playerRigidBody.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {
            joystickScript.axisOptions = AxisOptions.Horizontal;
            playerRigidBody.gravityScale = 1;
        }
    }
}
