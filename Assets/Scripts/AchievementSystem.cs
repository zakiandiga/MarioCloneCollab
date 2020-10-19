using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    private void Start()
    {
        Coin.OnCoinCollected += CoinCollectedAchievement;
        PowerUp.OnPowerUpCollected += PowerUpCollectedAchievement;
    }

    private void CoinCollectedAchievement(Coin coin)
    {
        ObjectPooler._instance.ReturnToPool(ObjectType.Coin,coin.gameObject);
        GameManager._instance.Score += coin.Point;
        //ObjectSpawner._instance.OutOffSpawnList(coin.gameObject);
    }

    private void PowerUpCollectedAchievement(PowerUp powerUp)
    {
        ObjectPooler._instance.ReturnToPool(ObjectType.PowerUp, powerUp.gameObject);
        GameManager._instance.PlayerHealth += powerUp.Health;
    }
}
