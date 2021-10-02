using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private IBullet[] bullets;
    private int numberOfBulletTypes;
    void Start()
    {

    }

    private void FixedUpdate()
    {
        Fire(ObjectPooler.objectPooler.GetObject(SelectionRandomBullets()).GetComponent<IBullet>());
    }

    private void Fire(IBullet bullet)
    {
        bullet.bulletSetting(this.transform);
    }

    private TypeObj SelectionRandomBullets()
    {
        Type type = typeof(TypeObj);
        Array values = type.GetEnumValues();  
        int index = UnityEngine.Random.Range(0, values.Length);
        TypeObj value = (TypeObj)values.GetValue(index);
        return value;
    }

}
