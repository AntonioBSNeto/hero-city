using UnityEngine;

public enum PowerUpType { Speed, Health, Damage }

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float amount;
}