using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthManager : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI playerHealthText;
    public GameObject player;

    public HealthBar healthBar;

    void Update()
    {
        healthBar.SetHealth(health);

        //Calls SetHealth(). Requires the gameobject player.
        SetHealth(player.gameObject.GetComponent<CharacterStats>());

        //Calls UpdateHealth(0). The zero vaule is required.
        UpdateHealth(0);
    }

    public void UpdateHealth(int healthToSubtract)
    {
        //Updates the health in the UI.
        playerHealthText.text = "Health: " + health;
    }

    void SetHealth(CharacterStats characterHealth)
    {
        //Sets health from PlayerHealthManager script to the same health value in the PlayerHealth script.
        health = characterHealth.health;
    }
}
