using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    //float speed;
    public Image image;

    private void Start()
    {
        Coin.OnCoinCollected += CoinCollectedAchievement;
        PowerUp.OnPowerUpCollected += PowerUpCollectedAchievement;
        SpeedBoost2.OnSpeedBoostCollected += SpeedBoostAchievement;
    }

    private void CoinCollectedAchievement(Coin coin)
    {
        ObjectPooler._instance.ReturnToPool(ObjectType.Coin,coin.gameObject);
        GameManager._instance.Score += coin.Point;
        //ObjectSpawner._instance.OutOffSpawnList(coin.gameObject);
    }

    private void SpeedBoostAchievement(SpeedBoost2 speedboost)
    {
        speedboost.gameObject.SetActive(false);        
        GameManager._instance.player.speed = 12f;
        //speed = 8.37f;
        image.enabled = true;
        StartCoroutine(EndSpeedBoost());

    }


    IEnumerator EndSpeedBoost()
    {
        yield return new WaitForSeconds(5);
        GameManager._instance.player.speed = 6f;
        image.enabled = false;
    }

    private void PowerUpCollectedAchievement(PowerUp powerUp)
    {
        ObjectPooler._instance.ReturnToPool(ObjectType.PowerUp, powerUp.gameObject);
        GameManager._instance.PlayerHealth += powerUp.Health;
    }
}
