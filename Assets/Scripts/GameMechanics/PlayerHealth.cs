using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void Update()
    {
        //If health is less or equal to 0...
        if (health <= 0)
        {
            //...call the method PlayerLost() from the GameManager script.
            GameManager.instance.PlayerLost();
        }
    }
}
