using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private IBullet[] bullets;
    private int numberOfBulletTypes;
    void Start()
    {
        numberOfBulletTypes = bullets.Length;
    }

    private void FixedUpdate()
    {
        Fire(ObjectPooler.objectPooler.GetObject(TypeObj.bullet_type1).GetComponent<RedBullet>());
    }

    private void Fire(IBullet bullet)
    {
        bullet.bulletSetting(transform);
    }

    //private IBullet SelectionRandomBullets()
    //{
    //    Random.Range
    //}

}
