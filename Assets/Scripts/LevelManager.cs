using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    //public Transform respawnPoint;
    //public GameObject playerPrefab;



    //[Header("Currency")]
   // public int currency = 0;
    public TextMeshProUGUI currencyUI;
    public TextMeshProUGUI healthUI;

    private void Awake()
    {
        instance = this; 
    }

    //public void Respawn()
    //{
    //    Debug.Log("I am respawning");
    //    GameObject player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    //}

    public void ChangeCurrency(int amount)
    {
        currencyUI.text = "$ " + amount;
    }

    public void ChangeHealth(int amount)
    {
        healthUI.text = "x "+amount;
    }
}
