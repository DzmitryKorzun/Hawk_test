using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    [SerializeField] protected float speedOfBullets;

    public virtual float bulletSpeed
    {
        get { return this.speedOfBullets; }
        set { this.speedOfBullets = value; }
    }

}
