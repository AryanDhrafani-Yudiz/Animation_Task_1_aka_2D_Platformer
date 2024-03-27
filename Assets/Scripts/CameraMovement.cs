using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float dampTime = 0.3f; //offset from the viewport center to fix damping
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            // Set this to the Y position you want the camera locked to
            //destination.y = 0;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
