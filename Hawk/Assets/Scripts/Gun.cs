using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();
    [SerializeField] private Queue<GameObject> queueOfBullets = new Queue<GameObject>();
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Border border;
    private System.Random random = new System.Random();
    private GameObject bulletContainer;

    private delegate void bulletMovementDelegate(float speed);
    bulletMovementDelegate bulletMovement;
    private void Awake()
    {

    }

    void Start()
    {
        border.bulletBarrierCollisionEvent += AddBulletToQueue;
        bulletContainer = new GameObject("bulletContainer");
    }

    private void FixedUpdate()
    {
        Fire();
        bulletMovement(bulletSpeed);
    }

    private void Fire()
    {
        if (queueOfBullets.Count > 0)
        {
            queueOfBullets.Dequeue().transform.position = this.transform.position;
        }
        else
        {
            GameObject bull = Instantiate(GetRandomRefBullet());
            bulletMovement += bull.GetComponent<Bullet>().BulletMovenent;
            bull.transform.position = this.transform.position;
        }
    }

    private GameObject GetRandomRefBullet()
    {
        int index = random.Next(bullets.Count);
        return bullets[index];
    }

    private void AddBulletToQueue(Collider bullet)
    {
        queueOfBullets.Enqueue(bullet.gameObject);
    }

}
