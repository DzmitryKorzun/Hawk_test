using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage;
    public void BulletMovenent(float speed)
    {
        transform.Translate(Vector3.forward * speed);
    }

}
