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
    GameObject container; 

    [Serializable]
    public class PrefabData
    {
        public TypeObj name;
        public GameObject prefab;
    }

    [SerializeField] private List<PrefabData> prefabDatas = null;
    private Dictionary<TypeObj, GameObject> prefabs = new Dictionary<TypeObj, GameObject>();
    private Dictionary<TypeObj, Queue<GameObject>> pools = new Dictionary<TypeObj, Queue<GameObject>>();
    private List<GameObject> parentObj;
    private void Awake()
    {

        //  TypeObj typeObj = new TypeObj();
        string[] tempEnumArray = Enum.GetNames(typeof(TypeObj)); //превратим ваш enum в массив строк
        int enumLen = tempEnumArray.Length; // а вот теперь получим количество
        objectPooler = this;
        foreach (PrefabData prefabData in prefabDatas)
        {
            prefabs.Add(prefabData.name, prefabData.prefab);
            pools.Add(prefabData.name, new Queue<GameObject>());
        }
        prefabDatas = null;
        Debug.Log(enumLen);
        for (int i = 0; i < enumLen; i++)
        {
            //container = new GameObject();
            parentObj.Add(Instantiate(container, this.transform));
            parentObj[i].name = tempEnumArray[i];
        }
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
