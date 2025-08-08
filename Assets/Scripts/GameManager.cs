using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverScreen;

    public bool gameOver = false;
    public bool isEndless = false;

    public int health;

    public int money = 0;

    //If there is another GameManager script in the scene it will be destroyed.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Keep money at 0 if it goes under.
        if(money < 0)
        {
            money = 0;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
        {
            GiveMoney(100);
        }
    }

    public void PlayerLost()
    {
        gameOver = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //When this actives the UI pulls up the text for game over and later text to restart the game.
        gameOverScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    public void GiveMoney(int amount)
    {
        money += amount;
    }
}
