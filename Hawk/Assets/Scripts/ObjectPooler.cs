using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeObj
{
    bullet_type1,
    bullet_type2
}
public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler objectPooler;


    [Serializable]
    public class PrefabData
    {
        public TypeObj name;
        public GameObject prefab;
    }

    [SerializeField] private List<PrefabData> prefabDatas = null;
    private Dictionary<TypeObj, GameObject> prefabs = new Dictionary<TypeObj, GameObject>();
    private Dictionary<TypeObj, Queue<GameObject>> pools = new Dictionary<TypeObj, Queue<GameObject>>();

    private void Awake()
    {
        objectPooler = this;
        foreach (PrefabData prefabData in prefabDatas)
        {
            prefabs.Add(prefabData.name, prefabData.prefab);
            pools.Add(prefabData.name, new Queue<GameObject>());
        }
        prefabDatas = null;
    }

    public GameObject GetObject(TypeObj poolName)
    {
        if (pools[poolName].Count > 0)
        {
            return pools[poolName].Dequeue();
        }

        return Instantiate(prefabs[poolName]);
    }

    public void ReturnObject(TypeObj poolName, GameObject poolObject)
    {
        pools[poolName].Enqueue(poolObject);
    }
}
