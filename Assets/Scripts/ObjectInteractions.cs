using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    private SpriteRenderer currentSpriteRenderer;
    [SerializeField] private Sprite doorOpened;
    [SerializeField] private Sprite chestOpened;
    [SerializeField] private Joystick joystickScript;
    [SerializeField] private float amountOfBounce;
    private Rigidbody2D playerRigidBody;
    [SerializeField] private UIManager uiScript;
    [SerializeField] private PlayerBehaviour playerBehaviourScript;
    [SerializeField] private SceneLoader sceneLoaderScript;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BouncePad"))
        {
            playerRigidBody.velocity += new Vector2(playerRigidBody.velocity.x, amountOfBounce);
            SoundManager.Instance.onBouncePadSound();
        }
        else if (collision.gameObject.CompareTag("BouncePadExtreme"))
        {
            playerRigidBody.velocity += new Vector2(playerRigidBody.velocity.x, amountOfBounce * 2);
            SoundManager.Instance.onBouncePadSound();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            currentSpriteRenderer.sprite = doorOpened;
            SoundManager.Instance.onDoorOpenSound();
        }
        else if (collision.gameObject.CompareTag("NextLevelDoor"))
        {
            sceneLoaderScript.LoadNextLevel();
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            currentSpriteRenderer.sprite = chestOpened;
            SoundManager.Instance.onChestOpenSound();
        }
        else if (collision.gameObject.CompareTag("Ladder"))
        {
            joystickScript.axisOptions = AxisOptions.Both;
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = Vector2.zero;
        }
        else if (collision.gameObject.CompareTag("Water"))
        {
            Time.timeScale = 0;
            uiScript.OnGameOverScreen();
        }
        else if (collision.gameObject.CompareTag("CampFire")) playerBehaviourScript.currentHealth = playerBehaviourScript.maxHealth; playerBehaviourScript.UpdateHP();
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