using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ObjectType type;
        public GameObject prefab;
        public int poolSize;
    }

    public static ObjectPooler _instance;

    public Transform objectParent;
    public List<Pool> poolList;
    public static Dictionary<ObjectType, Queue<GameObject>> poolDict;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        InitializePool();
    }

    //Initialize pool
    public void InitializePool()
    {
        poolDict = new Dictionary<ObjectType, Queue<GameObject>>();
        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, objectParent);
                obj.SetActive(false);

                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.type, objectPool);
        }
    }

    //return gameObject from the pool
    public GameObject SpawnFromPool(ObjectType type, Vector2 position)
    {
        if (poolDict != null)
        {
            if (!poolDict.ContainsKey(type))
            {
                return null;
            }
            if (poolDict[type].Count > 0)
            {
                GameObject objectgToSpawn = poolDict[type].Dequeue();
                
                objectgToSpawn.SetActive(true);
                objectgToSpawn.transform.position = position;
                return objectgToSpawn;
            }
        }
        return null;
    }

    //Return gameObject to pool
    public void ReturnToPool(ObjectType type, GameObject gameObj)
    {
        gameObj.SetActive(false);
        poolDict[type].Enqueue(gameObj);
    }

    public void GetObjByPosition(ObjectType type,Vector2 pos)
    {
        foreach (GameObject obj in poolDict[type])
        {
          
        }
        
    }

}
public enum ObjectType
{
    Coin,
    PowerUp
}
