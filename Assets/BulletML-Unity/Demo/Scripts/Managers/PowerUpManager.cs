using UnityEngine;
using Pixelnest.BulletML.Demo;

public class PowerUpManager : MonoBehaviour
{
    public void ApplyPowerUp(DemoPlayerController player, PowerUp powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUpType.Speed:
                player.IncreaseSpeed(powerUp.amount);
                break;
            case PowerUpType.Health:
                player.RecoverHealth(powerUp.amount);
                break;
            case PowerUpType.Damage:
                player.IncreaseDamage(powerUp.amount);
                break;
        }
    }
}