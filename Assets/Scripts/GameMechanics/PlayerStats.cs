using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float speed = 12f;
    public float baseSpeed = 12f;
    public bool isPoweredUp = false;

    //If there is another GlobalManager script in the scene it will be destroyed.
    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isPoweredUp)
        {
            Invoke("ResetStats", 300f * Time.deltaTime);
        }

        else
        {
            speed = baseSpeed;
        }
    }

    void ResetStats()
    {
        speed = baseSpeed;
    }
}
