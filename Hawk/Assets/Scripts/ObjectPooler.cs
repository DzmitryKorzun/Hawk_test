using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeObj
{
    bullet_type1,
    bullet_type2,
}


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler objectPooler;
    [Serializable] public class PrefabData
    {
        public TypeObj name;
        public GameObject prefab;
    }
    
    [SerializeField] private List<PrefabData> prefabDatas = null;
    [SerializeField] private Dictionary<TypeObj, Queue<GameObject>> pools = new Dictionary<TypeObj, Queue<GameObject>>();
    [SerializeField] private List<GameObject> parentObj;
    private GameObject containerRef;    
    private Dictionary<TypeObj, GameObject> prefabs = new Dictionary<TypeObj, GameObject>();

    private void Awake()
    {
        containerRef = new GameObject();
        string[] tempEnumArray = Enum.GetNames(typeof(TypeObj));
        int enumLen = tempEnumArray.Length;
        objectPooler = this;
        foreach (PrefabData prefabData in prefabDatas)
        {
            prefabs.Add(prefabData.name, prefabData.prefab);
            pools.Add(prefabData.name, new Queue<GameObject>());
        }
        for (int i = 0; i < enumLen; i++)
        {
            parentObj.Add(Instantiate(containerRef, this.transform));
            parentObj[i].name = tempEnumArray[i];
        }
    }

    public GameObject GetObject(TypeObj poolName)
    {
        if (pools[poolName].Count > 0)
        {
            return pools[poolName].Dequeue(); 
        }
        return GenerateNewObject(poolName);
    }

    public void ReturnObject(TypeObj poolName, GameObject poolObject)
    {
        pools[poolName].Enqueue(poolObject);
    }

    public List<int> gettingValidateEnum<T>()
    {
        List<int> validateDate = new List<int>();

        int i = 0;
        foreach (PrefabData item in prefabDatas)
        {
            if (item.prefab.GetComponent<T>() != null)
            {
                validateDate.Add(i);
            }
            i++;
        }
        return validateDate;
    }

    private GameObject GenerateNewObject(TypeObj poolName)
    {
        int objID = (int)poolName;
        return Instantiate(prefabs[poolName], parentObj[objID].transform);
    }


}
