using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedBoost : CollectableItem
{
    public static event Action<SpeedBoost> OnSpeedBoostCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (OnSpeedBoostCollected != null)
            {
                OnSpeedBoostCollected(this);
            }
        }
    }
}
