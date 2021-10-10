using UnityEngine;

public class PersonMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPosition;
    Vector3 worldPosition = new Vector3();
    private float deltaX, deltaZ;
    private float distance;
    private float maxZ = 5.5f;
    private float minX = -3f;
    private float maxX = 3f;
    private float minZ = -5.5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = worldPosition.x - transform.position.x;
                    deltaZ = worldPosition.z - transform.position.z;
                    break;
                case TouchPhase.Moved:
                    Vector3 pos = new Vector3(Mathf.Clamp(worldPosition.x - deltaX, minX, maxX), 0, Mathf.Clamp(worldPosition.z - deltaZ, minZ, maxZ));
                    rb.MovePosition(pos);
                    break;
                case TouchPhase.Ended:
                    rb.velocity = Vector3.zero;
                    break;
            }
        }
        

    }
}
