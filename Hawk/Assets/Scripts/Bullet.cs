using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;

    private Gun myGun;
    private float speed;
    private WeaponOwner bulletOwner;
    private Collider physicalField;
    private Vector3 bulletDirection;
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
    private void FixedUpdate()
    {
        transform.Translate(bulletDirection * speed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(physicalField)==false) myGun.AddBulletToQueue(this.transform);
    }

    public void Setup(float speed, Gun gun, WeaponOwner weaponOwner, Collider physicalField, Vector3 bulletDirection)
    {
        this.speed = speed;
        this.myGun = gun;
        this.bulletOwner = weaponOwner;
        this.physicalField = physicalField;
        this.bulletDirection = bulletDirection;
    }
}
