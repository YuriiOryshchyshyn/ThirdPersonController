using UnityEngine;

public class PlayerMnager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public void Startup()
    {
        print("Starting PlayerManager");

        health = 50;
        maxHealth = 50;

        status = ManagerStatus.Started;
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        print($"Health : {health} / {maxHealth}");
    }
}
