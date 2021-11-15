using System.Collections.Generic;
using UnityEngine;

public enum WeaponOwner
{
    Person,
    Enemy
}

public class Gun : MonoBehaviour
{
    [SerializeField] private List<Bullet> refBullets;
    [SerializeField] private List<Bullet> allBullets;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private WeaponOwner weaponOwner;
    [SerializeField] private Collider physicalField;
    [SerializeField] private BaseLauncher launcher;

    private Queue<Transform> queueOfBullets;
    private System.Random random = new System.Random();
    private GameObject bulletContainer;

    public float BulletSpeed
    {
        set
        {
            bulletSpeed = value;
        }
        get
        {
            return bulletSpeed;
        }
    }

    void Start()
    {
        queueOfBullets = new Queue<Transform>();
        bulletContainer = new GameObject("bulletContainer");
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
            bull.Setup(bulletSpeed, this, weaponOwner, physicalField, launcher);
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

    public void AddBulletToQueue(Transform bullTransform)
    {
        bullTransform.gameObject.SetActive(false);
        queueOfBullets.Enqueue(bullTransform);
    }
}
