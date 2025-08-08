using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAmmo : MonoBehaviour
{
    public TextMeshProUGUI ammoInfoText;
    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.currentGun != null)
        {
            Gun gun = player.currentGun;
            ammoInfoText.text = $"Ammo: {gun.currentAmmo}/{gun.maxAmmo}";
        }

        else
        {
            ammoInfoText.text = "";
        }
    }
}
