using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerUp : CollectableItem
{
    public static event Action<PowerUp> OnPowerUpCollected;

    //PowerUp health
    private int health = 1;
    public int Health { get => health; }

    protected override void SetType()
    {
        objType = ObjectType.PowerUp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (OnPowerUpCollected != null)
            {
                OnPowerUpCollected(this);
            }
        }
    }
}
