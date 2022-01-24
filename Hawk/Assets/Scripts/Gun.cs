using System.Collections;
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
    [SerializeField] private int numBulletsPerShot;
    [SerializeField] private float firingIntensity;

    private Queue<Transform>[] queueOfBullets;
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

    private void Start()
    {
        bulletContainer = new GameObject("bulletContainer");
        Debug.Log(bulletSpeed);
        launcher.GetBulletSpeed(BulletSpeed);
        numBulletsPerShot = launcher.SetNumBulletsPerShot();
        InitQueueOfBullets();
        StartFire();
    }

    private void InitQueueOfBullets()
    {
        queueOfBullets = new Queue<Transform>[numBulletsPerShot];
        for (int i = 0; i < numBulletsPerShot; i++)
        {
            queueOfBullets[i] = new Queue<Transform>();
        }
    }

    private void Fire()
    {
        for (int i = 0; i < numBulletsPerShot; i++)
        {
            if (queueOfBullets[i].Count > 0)
            {
                Transform obj = this.queueOfBullets[i].Dequeue();
                obj.position = this.transform.position;
                obj.gameObject.SetActive(true);
            }
            else
            {
                Bullet bull = Instantiate(GetRandomRefBullet(), bulletContainer.transform);
                allBullets.Add(bull);
                bull.transform.position = this.transform.position;
                bull.Setup(bulletSpeed, this, weaponOwner, physicalField, launcher, i);
            }
            if (bulletSpeed != allBullets[0].Speed)
            {
                foreach (Bullet bullet in allBullets)
                {
                    bullet.Speed = bulletSpeed;
                }
            }
        }       
    }

    private Bullet GetRandomRefBullet()
    {
        int index = random.Next(refBullets.Count);
        return refBullets[index];
    }

    public void AddBulletToQueue(Transform bullTransform, int numThread)
    {
        bullTransform.gameObject.SetActive(false);
        queueOfBullets[numThread].Enqueue(bullTransform);
    }

    public void StartFire()
    {
        InvokeRepeating(nameof(Fire), firingIntensity, firingIntensity);
    }
}
