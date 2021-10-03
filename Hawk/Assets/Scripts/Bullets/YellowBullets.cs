using UnityEngine;

public class YellowBullets : Bullet, IBullet
{
    public void bulletSetting(Transform startTransform)
    {
        transform.position = startTransform.position;
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
