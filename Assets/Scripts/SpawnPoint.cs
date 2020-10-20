using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 pos;
    public bool actived;
    public GameObject obj;

    public SpawnPoint(Vector2 pos, bool actived)
    {
        this.pos = pos;
        this.actived = actived;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ViewField")
        {
            if (obj == null && actived)
            {
                obj = ObjectSpawner._instance.RandomItemInPos(transform.position);
                if (obj)
                {
                    obj.transform.parent = this.transform;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ViewField")
        {
            if (obj)
            {
                obj.transform.parent = ObjectPooler._instance.objectParent;
                ObjectPooler._instance.ReturnToPool(obj.GetComponent<CollectableItem>().objType, obj);
                obj = null;
            }
        }
    }
}
