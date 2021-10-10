using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float damage;

    //private void FixedUpdate()
    //{
    //    BulletMovenent(0.30f);
    //}


    public void BulletMovenent(float speed)
    {
        transform.Translate(Vector3.forward * speed);
    }

}
