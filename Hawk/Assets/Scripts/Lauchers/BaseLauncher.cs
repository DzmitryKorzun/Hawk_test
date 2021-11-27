using UnityEngine;

public class BaseLauncher : MonoBehaviour
{
    protected float bulletSpeed;
    protected int numThread = 1;
    public int  NumThread => numThread;

    public void GetBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    public virtual int SetNumBulletsPerShot()
    {
        return numThread;
    }

    public virtual void Move(Bullet bul, int numThread)
    {
        bul.transform.Translate(Vector3.forward * bulletSpeed);
    }

}
