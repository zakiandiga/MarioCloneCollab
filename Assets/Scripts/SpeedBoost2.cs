using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedBoost2 : MonoBehaviour
{
    public static event Action<SpeedBoost2> OnSpeedBoostCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collide");

            if (OnSpeedBoostCollected != null)
            {
                OnSpeedBoostCollected(this);
            }
        }
    }
}
