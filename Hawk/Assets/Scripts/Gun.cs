using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private List<GameObject> bullets;
    [SerializeField] private Queue<Transform> queueOfBullets;
    [SerializeField] private Border border;
    private System.Random random = new System.Random();
    private GameObject bulletContainer;
    [SerializeField] private float speedOfBullets;
    [SerializeField] private List<Transform> guns;
    private delegate void bulletMovementDelegate(float speed);
    bulletMovementDelegate bulletMovement;

   // [HideInInspector] public sealed override float bulletSpeed { get => base.bulletSpeed; set => base.bulletSpeed = value; }

    void Start()
    {
        queueOfBullets = new Queue<Transform>();
        border.bulletBarrierCollisionEvent += AddBulletToQueue;
        bulletContainer = new GameObject("bulletContainer");
    }

    private void FixedUpdate()
    {
        Fire();
        bulletMovement(speedOfBullets);
    }

    private void Fire()
    {
        foreach (var item in guns)
        {
            if (queueOfBullets.Count > 0)
            {
                var obj = this.queueOfBullets.Dequeue();
                obj.position = item.transform.position;
            }
            else
            {
                GameObject bull = Instantiate(GetRandomRefBullet());
                bulletMovement += bull.GetComponent<Bullet>().BulletMovenent;
                bull.transform.position = item.position;
            }
        }
     
    }

    private GameObject GetRandomRefBullet()
    {
        int index = random.Next(bullets.Count);
        return bullets[index];
    }

    public void AddBulletToQueue(Collider bullet)
    {
        queueOfBullets.Enqueue(bullet.transform);
    }

}
