using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 300f;
    private float timer;
    public GameObject winScreen;
    public TextMeshProUGUI timerText;

    private bool gameEnded = false;

    void Start()
    {
        timer = gameDuration;
        winScreen.SetActive(false);
    }

    void Update()
    {
        DisplayTime();

        if (gameEnded || GameManager.instance.gameOver == true || GameManager.instance.isEndless == true)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        winScreen.SetActive(true);
        timerText.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timerText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
