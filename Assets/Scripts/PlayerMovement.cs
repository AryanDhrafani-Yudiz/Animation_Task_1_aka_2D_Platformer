using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private FixedJoystick fixedJoystick;

    void Update()
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