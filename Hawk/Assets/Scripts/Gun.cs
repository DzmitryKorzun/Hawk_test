using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private IBullet[] bullets;
    private Type type;
    private Array values;
    void Start()
    {
        type = typeof(TypeObj);
        values = type.GetEnumValues();
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
        int index = UnityEngine.Random.Range(0, values.Length);
        TypeObj value = (TypeObj)values.GetValue(index);
        return value;
    }

}
