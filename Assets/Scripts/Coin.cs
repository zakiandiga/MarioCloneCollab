using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Coin : CollectableItem
{

    private int point = 1;

    //Coin Point
    public int Point { get => point; }

    public static event Action<Coin> OnCoinCollected;

    protected override void SetType()
    {
        objType = ObjectType.Coin;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (OnCoinCollected != null)
            {
                OnCoinCollected(this);
            }
        }
    }
}
