using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : Bullet, IBullet
{
    public void bulletSetting(Transform startTransform)
    {
        transform.position = startTransform.position;
        this.gameObject.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        ObjectPooler.objectPooler.ReturnObject(TypeObj.bullet_type1, gameObject);
    }

    private void FixedUpdate()
    {
        BulletMovement();
    }
}
