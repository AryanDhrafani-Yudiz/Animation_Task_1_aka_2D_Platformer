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
    [SerializeField] private UIManager uiScript;
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
        if (collision.gameObject.CompareTag("BouncePadExtreme"))
        {
            playerRigidBody.velocity += new Vector2(playerRigidBody.velocity.x, amountOfBounce * 2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            currentSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            currentSpriteRenderer.sprite = doorOpened;
        }
        if (collision.gameObject.CompareTag("NextLevelDoor"))
        {
            StartCoroutine(LoadYourAsyncScene());
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
        if (collision.gameObject.CompareTag("Water"))
        {
            Time.timeScale = 0;
            uiScript.OnGameOverScreen();
        }
        if (collision.gameObject.CompareTag("CampFire")) GetComponent<PlayerBehaviour>().currentHealth = GetComponent<PlayerBehaviour>().maxHealth;
    }
    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        Time.timeScale = 1;

        while (!asyncLoad.isDone)
        {
            yield return null;
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