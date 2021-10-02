using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : Bullet, IBullet
{
    private Transform bulletTransform;

    private void Awake()
    {
        bulletTransform = this.transform;
    }
    public void BulletMovement()
    {
        transform.Translate(Vector2.up * speed);
    }

    public void bulletSetting(Transform startTransform)
    {
        bulletTransform.position = startTransform.position;
        this.gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        ObjectPooler.objectPooler.ReturnObject(TypeObj.bullet_type1, gameObject);
    }

    private void OnEnable()
    {
        //this.transform.position = bulletStartPosition.position;
    }

    private void FixedUpdate()
    {
        BulletMovement();
    }
}
