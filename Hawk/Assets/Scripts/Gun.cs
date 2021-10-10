using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private List<GameObject> bullets;
    [SerializeField] private List<Transform> guns;
    [SerializeField] private Border border;
    [SerializeField] private float speedOfBullets;
    private Queue<Transform> queueOfBullets;
    private System.Random random = new System.Random();
    private GameObject bulletContainer;    
    private delegate void bulletMovementDelegate(float speed);
    private bulletMovementDelegate bulletMovement;
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
        foreach (Transform item in guns)
        {
            if (queueOfBullets.Count > 0)
            {
                Transform obj = this.queueOfBullets.Dequeue();
                obj.position = item.transform.position;
            }
            else
            {
                GameObject bull = Instantiate(GetRandomRefBullet(), bulletContainer.transform);
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
    private void AddBulletToQueue(Collider bullet)
    {
        queueOfBullets.Enqueue(bullet.transform);
    }

}
