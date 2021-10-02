using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovementController : MonoBehaviour
{

    void FixedUpdate()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                Vector3 p = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                p.z = transform.position.z;
                transform.position = p;
            }
        }
    }
}
