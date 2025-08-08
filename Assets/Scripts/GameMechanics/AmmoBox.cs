using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //If the game object that collides with this object has the tag "Player"...
        if (other.CompareTag("Player"))
        {
            Gun[] guns = other.GetComponentsInChildren<Gun>();

            foreach (Gun gun in guns)
            {
                //...call RefillAmmo(), gets the component <Gun>() from a child of the player that has the component. 
                RefillAmmo(gun);
            }

            GrenadeThrower[] grenades = other.GetComponentsInChildren<GrenadeThrower>();

            foreach (GrenadeThrower grenade in grenades)
            {
                //...call RefillAmmo(), gets the component <Gun>() from a child of the player that has the component. 
                RefillGrenades(grenade);
            }

            //Set the ammoBox in an inactive state.
            gameObject.SetActive(false);
        }
    }

    void RefillAmmo(Gun gun)
    {
        //Adds the value of maxAmmoClip from gun to maxAmmo from gun.
        gun.maxAmmo += gun.maxAmmoClip;
    }    
    
    void RefillGrenades(GrenadeThrower grenade)
    {
        //Adds the value of maxAmmoClip from gun to maxAmmo from gun.
        grenade.grenadeCount = grenade.grenadeCountMax;
    }
}
