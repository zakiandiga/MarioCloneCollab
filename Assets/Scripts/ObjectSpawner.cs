using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner _instance;

    public float spawnTime = 0.5f;
    public Transform player;

    //public List<SpawnPoint> pointsPosition;

    //public List<GameObject> spawnList;

    public Transform SpawnPoints;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        //pointsPosition = new List<SpawnPoint>();
        InitialPoints();
        //StartCoroutine(ObjectSpawn(spawnTime));
    }
    private void Update()
    {
        //foreach (SpawnPoint point in CanSpawnWithinArea(player.position, 10.0f))
        //{
        //    RandomItemInPos(point);
        //}

        //foreach (SpawnPoint point in CanNotSpawnWithinArea(player.position, 10.0f))
        //{
        //    ItemInPoint(point);
        //}

    }

    //private IEnumerator ObjectSpawn(float time)
    //{
    //    while (true)
    //    {
    //        foreach (SpawnPoint point in CanSpawnWithinArea(player.position, 10.0f))
    //        {
    //            ItemInPoint(point);
    //        }


    //        yield return new WaitForSeconds(time);
    //    }
    //}

    public void ItemInPoint(SpawnPoint point)
    {
        ObjectPooler._instance.SpawnFromPool(ObjectType.Coin, point.pos);
        point.actived = true;
    }

    //public void ItemNotInPoint()
    //{

    //    ObjectPooler._instance.SpawnFromPool(ObjectType.Coin, point.pos);
    //}

    //public void OutOffSpawnList(GameObject obj)
    //{
    //    foreach (GameObject curObj in spawnList)
    //    {
    //        if(curObj == obj)
    //        {
    //            spawnList.Remove(curObj);
    //        }
    //    }
    //}
    public void RandomItemInPos(SpawnPoint point)
    {
        float ranNum = Random.Range(0f, 10f);
        if (ranNum > 8.0f)
        {
            ObjectPooler._instance.SpawnFromPool(ObjectType.PowerUp, point.pos);
        }
        else if ((ranNum <= 8.0f))
        {
            ObjectPooler._instance.SpawnFromPool(ObjectType.Coin, point.pos);
        }
        point.actived = true;
    }

    public GameObject RandomItemInPos(Vector2 position)
    {
        float ranNum = Random.Range(0f, 10f);
        if (ranNum > 8.0f)
        {
            return ObjectPooler._instance.SpawnFromPool(ObjectType.PowerUp, position);
        }
        else if ((ranNum <= 8.0f))
        {
            return ObjectPooler._instance.SpawnFromPool(ObjectType.Coin, position);
        }
        return null;
    }

    public Vector3 RandomPosition()
    {
        float xRandom = Random.Range(-20.0f, 20.0f);
        float yRandom = Random.Range(0.0f, 15.0f);

        Vector2 randomPos = new Vector2(xRandom, yRandom);
        return randomPos;
    }

    public void InitialSpawn()
    {

    }

    //public List<SpawnPoint> CanSpawnWithinArea(Vector2 position, float offset)
    //{
    //    List<SpawnPoint> canSpawnPoint = new List<SpawnPoint>();
    //    foreach (SpawnPoint point in pointsPosition)
    //    {
    //        if ((Mathf.Abs(point.pos.x - position.x) < offset) && (point.actived == false))
    //        {
    //            canSpawnPoint.Add(point);
    //        }
    //    }
    //    return canSpawnPoint;
    //}

    //public List<SpawnPoint> CanNotSpawnWithinArea(Vector2 position, float offset)
    //{
    //    List<SpawnPoint> canNotSpawnPoint = new List<SpawnPoint>();
    //    foreach (SpawnPoint point in pointsPosition)
    //    {
    //        if ((Mathf.Abs(point.pos.x - position.x) > offset) && (point.actived == false))
    //        {
    //            canNotSpawnPoint.Add(point);
    //        }
    //    }
    //    return canNotSpawnPoint;
    //}


    public void InitialPoints()
    {
        foreach (Transform child in SpawnPoints)
        {
            //Vector2 childPos = new Vector2(child.transform.position.x, child.transform.position.y);
            Debug.Log(child.gameObject.name);
            //pointsPosition.Add(new SpawnPoint(child.position, false));
            child.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            child.gameObject.AddComponent<SpawnPoint>().actived = true;
        }
    }
}
