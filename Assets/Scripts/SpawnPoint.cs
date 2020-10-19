using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SpawnPoint
{
    public Vector2 pos;
    public bool actived;

    public SpawnPoint(Vector2 pos,bool actived)
    {
        this.pos = pos;
        this.actived = actived;
    }
}
