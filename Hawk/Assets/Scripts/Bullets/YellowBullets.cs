using UnityEngine;

public class YellowBullets : Bullet, IBullet
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
        ObjectPooler.objectPooler.ReturnObject(TypeObj.bullet_type2, gameObject);
    }
    private void FixedUpdate()
    {
        BulletMovement();
    }
}
