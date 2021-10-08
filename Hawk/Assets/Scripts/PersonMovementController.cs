using UnityEngine;

public class PersonMovementController : MonoBehaviour
{
    private Vector2 vectorDifference;
    private Vector2 startPosition;
    private float maxY;
    private float minY;
    private float maxX;
    private float minX;

    private void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        maxX = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x;
        maxY = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y;
    }

    private void FixedUpdate()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPosition = Input.GetTouch(0).position;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 pos = transform.position;
            pos = (startPosition - Input.GetTouch(0).position) * 2 * Time.deltaTime;
            Debug.Log(pos);
            this.gameObject.transform.position = pos;
        }

    }
}
