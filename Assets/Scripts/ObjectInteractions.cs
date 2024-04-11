using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ObjectInteractions : MonoBehaviour
{
    private SpriteRenderer currentSpriteRenderer;
    [SerializeField] private Sprite doorOpened;
    [SerializeField] private Sprite chestOpened;
    [SerializeField] private Joystick joystickScript;
    [SerializeField] private float amountOfBounce;
    private Rigidbody2D playerRigidBody;
    [SerializeField] private PlayerBehaviour playerBehaviourScript;

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
        if (collision.gameObject.CompareTag("BouncePadExtreme"))
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
            if (currentSpriteRenderer.sprite != doorOpened)
            {
                currentSpriteRenderer.sprite = doorOpened;
                SoundManager.Instance.onDoorOpenSound();
            }
        }
        if (collision.gameObject.CompareTag("NextLevelDoor"))
        {
            StartCoroutine(LoadYourAsyncScene());
        }
        if (collision.gameObject.CompareTag("Chest"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (currentSpriteRenderer.sprite != chestOpened)
            {
                currentSpriteRenderer.sprite = chestOpened;
                SoundManager.Instance.onChestOpenSound();
            }
        }
        if (collision.gameObject.CompareTag("Ladder"))
        {
            joystickScript.axisOptions = AxisOptions.Both;
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = Vector2.zero;
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            Time.timeScale = 0;
            UIManager.Instance.OnGameOverScreen();
        }
        if (collision.gameObject.CompareTag("CampFire")) playerBehaviourScript.currentHealth = playerBehaviourScript.maxHealth; playerBehaviourScript.UpdateHP();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            joystickScript.axisOptions = AxisOptions.Horizontal;
            playerRigidBody.gravityScale = 1;
        }
    }
    private IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        Time.timeScale = 1;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}