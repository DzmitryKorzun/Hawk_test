using UnityEngine;

public class BaseLauncher : MonoBehaviour
{
    [SerializeField] protected Gun gun;

    protected float bulletSpeed;

    private void Awake()
    {
        GetBulletSpeed();
    }

    protected void GetBulletSpeed()
    {
        bulletSpeed = gun.BulletSpeed;
    }

    public virtual void Move(Bullet bul)
    {
        bul.transform.Translate(Vector3.forward * bulletSpeed);
    }

}
