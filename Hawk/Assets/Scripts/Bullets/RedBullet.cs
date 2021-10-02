using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : Bullet, IBullet
{
    private Transform bulletStartPosition;

    public void BulletMovement()
    {
        transform.Translate(Vector2.up * speed);
    }

    public void bulletSetting(Transform startPos)
    {
        bulletStartPosition = startPos;
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
