using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health;

    public float speed = 12f;
    public float baseSpeed = 12f;

    public void TakeDamage(int amount)
    {
        Debug.Log($"{gameObject.name} is taking {amount} damage!");

        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (CompareTag("Player") || gameObject.GetComponent<Stronghold>())
        {
            //...call the method PlayerLost() from the GameManager script.
            GameManager.instance.PlayerLost();
        }

        //else if(CompareTag("Enemy"))
        //{
        //    GameObject enemySpawner = 
        //    Destroy(gameObject);
        //}   
        
        else
        {
            GameManager.instance.GiveMoney(100);
            Destroy(gameObject);
        }
    }
}
