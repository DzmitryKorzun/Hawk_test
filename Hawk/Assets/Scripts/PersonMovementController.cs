using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovementController : MonoBehaviour
{
    private Vector2 vectorDifference;

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

    void FixedUpdate()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                vectorDifference = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            }
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                Vector2 p = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                float posX = Mathf.Clamp((p + vectorDifference).x, minX, maxX);
                float posY = Mathf.Clamp((p + vectorDifference).y, minY, maxY);
                transform.position = new Vector2(posX, posY);
            }
        }
    }
}
