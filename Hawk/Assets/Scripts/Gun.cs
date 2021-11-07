using System.Collections.Generic;
using UnityEngine;

public enum WeaponOwner
{
    Person,
    Enemy
}
public enum ShotDirection
{
    Up,
    Down,
    Left,
    Right,
}

public class Gun : MonoBehaviour
{
    [SerializeField] private List<Bullet> refBullets;
    [SerializeField] private List<Bullet> allBullets;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private WeaponOwner weaponOwner;
    [SerializeField] private ShotDirection shotDirection;
    [SerializeField] private Collider physicalField;

    private Queue<Transform> queueOfBullets;
    private System.Random random = new System.Random();
    private GameObject bulletContainer;
    private Vector3 shootVectorDirection;
    private Vector3 bulletVectorRotation;
    public float BulletSpeed
    {
        set
        {
            bulletSpeed = value;
        }
    }

    void Start()
    {
        queueOfBullets = new Queue<Transform>();
        bulletContainer = new GameObject("bulletContainer");
        shootVectorDirection = BulletDirection();
    }

    private void FixedUpdate()
    {
        Fire();
    }

    private void Fire()
    {
        if (queueOfBullets.Count > 0)
        {
            Transform obj = this.queueOfBullets.Dequeue();
            obj.position = this.transform.position; 
            obj.gameObject.SetActive(true);
        }
        else
        {
            Bullet bull = Instantiate(GetRandomRefBullet(), bulletContainer.transform);
            allBullets.Add(bull);
            bull.transform.position = this.transform.position;
            bull.Setup(bulletSpeed, this, weaponOwner, physicalField, shootVectorDirection);
        }        
        if (bulletSpeed != allBullets[0].Speed)
        {
            foreach (Bullet bullet in allBullets)
            {
                bullet.Speed = bulletSpeed;
            }
        }
    }

    private Bullet GetRandomRefBullet()
    {
        int index = random.Next(refBullets.Count);
        return refBullets[index];
    }

    private Vector3 BulletDirection()
    {
        switch (shotDirection)
        {
            case ShotDirection.Up:
                return Vector3.forward;
            case ShotDirection.Down:
                return Vector3.back;
            case ShotDirection.Left:
                return Vector3.left;
            case ShotDirection.Right:
                return Vector3.right;
            default:
                return Vector3.forward;
        }
    }

    public void AddBulletToQueue(Transform bullTransform)
    {
        bullTransform.gameObject.SetActive(false);
        queueOfBullets.Enqueue(bullTransform);
    }
}
