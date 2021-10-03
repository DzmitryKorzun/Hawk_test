using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void BulletMovement();
    public void bulletSetting(Transform startPos);
}
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;

    public float ReturnBulletDamage()
    {
        return damage;
    }

    public virtual void BulletMovement()
    {
        transform.Translate(Vector2.up * speed);
    }

}
