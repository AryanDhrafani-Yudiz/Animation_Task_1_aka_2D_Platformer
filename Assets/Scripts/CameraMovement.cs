using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 velocity;
    [SerializeField] private float dampTime; //offset from the viewport center to fix damping
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (target)
        {
            Vector3 pointInScreen = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, pointInScreen.z));
            Vector3 destination = transform.position + delta;
            destination.y += 1;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
