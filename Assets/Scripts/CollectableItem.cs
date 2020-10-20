using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CollectableItem : MonoBehaviour
{
    //public static event Action<PowerUp> OnCollected;
    public ObjectType objType;

    protected virtual void SetType()
    {
        this.objType = ObjectType.NoType;
    }

}
