using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;

    private Gun myGun;
    private float speed;
    private WeaponOwner bulletOwner;
    private Collider physicalField;
    private BaseLauncher launcher;
    private int numThread;

    public float Speed 
    { 
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    public WeaponOwner BulletOwner => bulletOwner;
    public Gun MyGun => myGun;
    public float Damage => damage;
    public int NumThread => numThread;

    private void FixedUpdate()
    {
        launcher.Move(this, numThread);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.Equals(physicalField) == false) myGun.AddBulletToQueue(this.transform, numThread);
    }
    public void Setup(float speed, Gun gun, WeaponOwner weaponOwner, Collider physicalField, BaseLauncher launcher, int numThread)
    {
        this.speed = speed;
        this.myGun = gun;
        this.bulletOwner = weaponOwner;
        this.physicalField = physicalField;
        this.launcher = launcher;
        this.numThread = numThread;
    }
}
