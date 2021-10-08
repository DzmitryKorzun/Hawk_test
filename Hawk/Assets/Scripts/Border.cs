using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public delegate void BorderDelegate(Collider obj);
    public event BorderDelegate bulletBarrierCollisionEvent;

    private void OnTriggerEnter(Collider other)
    {
        bulletBarrierCollisionEvent?.Invoke(other);
    }
}
