using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 jumpDir;

    //public void FixedUpdate()
    //{
    //    float dir = Vector2.one * fixedJoystick.Horizontal;
    //    transform.position += new Vector3(dir , 0, 0);
    //    Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
    //    Debug.Log(direction);
    //    rb.AddForce(direction * speed * Time.fixedDeltaTime);
    //}
    void Update()
    {
        if (fixedJoystick.Horizontal > 0.1f)
        {
            Quaternion newRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = newRotation;
            Vector3 newPos = new Vector3(fixedJoystick.Horizontal, 0, 0);
            transform.position += newPos * speed * Time.deltaTime;
        }

        else if (fixedJoystick.Horizontal < -0.1f)
        {

            Quaternion newRotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = newRotation;
            Vector3 newPos = new Vector3(fixedJoystick.Horizontal, 0, 0);
            transform.position += newPos * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
        }
    }
}
