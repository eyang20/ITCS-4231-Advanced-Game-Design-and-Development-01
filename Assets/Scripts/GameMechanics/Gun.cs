using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 1;
    public float range = 50f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float burstShots = 3f;

    public int maxAmmo = 150;
    public int maxAmmoClip = 10;
    public int currentAmmo;
    public bool outOfAmmo = false;

    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;
    private bool isScoping = false;

    public GameObject reloadingText;
    //public GameObject outOfAmmoText;

    public enum ShotType
    {
        fullAuto,
        semiAuto,
        burst
    }

    public ShotType shotType = ShotType.semiAuto;

    void Start()
    {
        currentAmmo = maxAmmoClip;
    }

    // Update is called once per frame
    void Update()
    {
        //If isReloading is true...
        if (isReloading)
        {
            //...return to the top of the script.
            return;
        }

        //If isReloading is not true...
        if (!isReloading)
        {
            //...set reloadingText to be disabled.
            reloadingText.SetActive(false);
        }

        bool needsReload = currentAmmo <= 0 || (Input.GetKey("r") && currentAmmo < maxAmmoClip);
        //If currentAmmo is less than or equal to zero and outOfAmmo is NOT true, or if the key r is pressed and currentAmmo is less than maxAmmoClip and outOfAmmo is NOT true...
        if (needsReload && !outOfAmmo)
        {
            //...start a Coroutine with the method Reload() and then return to the top of the script.
            StartCoroutine(Reload());
            return;
        }

        ////If the input for "Fire1" is pressed and Time.time is greater than or equal to nextTimeToFire...
        //if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        //{
        //    //...call PlayAudio() and get the component script <ShootSound>() from the reference the method has.
        //    PlayShootAudio(GetComponent<ShootSound>());

        //    //...set nextTimeToFire to be equal to Time.time + 1f / fireRate and call the method Shoot().
        //    nextTimeToFire = Time.time + 1f / fireRate;
        //    Shoot();

        //}

        ////If the input for "Fire1" is pressed, outOfAmmo IS true, and currentAmmo is less than or equal to...
        //if (Input.GetButtonDown("Fire1") && outOfAmmo && currentAmmo <= 0)
        //{
        //    //...call PlayGunClickAudio(GetComponent<OutOfAmmoClickSound>()) and gets the OutOfAmmoClickSound script for that method.
        //    PlayGunClickAudio(GetComponent<OutOfAmmoClickSound>());
        //}

        switch (shotType)
        {
            case ShotType.fullAuto:
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
                {
                    PlayShootAudio(GetComponent<ShootSound>());
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
                break;

            case ShotType.semiAuto:
                if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
                {
                    PlayShootAudio(GetComponent<ShootSound>());
                    Shoot();
                }
                break;

            case ShotType.burst:
                if (Input.GetButtonDown("Fire1") && currentAmmo >= 3)
                {
                    StartCoroutine(BurstFire(3));
                }
                break;
        }

        //If the input for "Fire2" is pressed...
        if (Input.GetButtonDown("Fire2"))
        {
            Scope();
        }

        //If maxAmmo is greater than zero...
        if (maxAmmo > 0)
        {
            //...set outOfAmmoText in a disabled state and set outOfAmmo to false.
            //outOfAmmoText.SetActive(false);
            outOfAmmo = false;
        }

        //If maxAmmo is less than or equal to zero...
        if (maxAmmo <= 0)
        {
            //...call the method OutOfAmmo() and return to the top of the script.
            OutOfAmmo();
            return;
        }
    }

    void Shoot()
    {
        //Plays the particle system muzzleFlash
        muzzleFlash.Play();

        //Decreses currentAmmo.
        currentAmmo--;

        RaycastHit hit;
        //If the Raycast from the fpsCam's position in a forward direction hits something within range...
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            //... get the transform of that target and the script <CharacterStats>() if it has it.
            CharacterStats target = hit.transform.GetComponent<CharacterStats>();

            //If the script <CharacterStats>() is NOT null on the game object that was hit...
            if (target != null)
            {
                //...do damage to the target.
                target.TakeDamage(damage);
            }

            //If the rigidbody is NOT null on the game object that was hit...
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //impactGameObject is an instantiated impactEffect at hit position, facing away from the hit object based on the normal of the mesh.
            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(-hit.normal));
            //Destroy impactGameObject after 3 seconds.
            Destroy(impactGameObject, 3f);
        }
    }

    IEnumerator BurstFire(int shots)
    {
        for (int i = 0; i < shots; i++)
        {
            if (currentAmmo > 0)
            {
                PlayShootAudio(GetComponent<ShootSound>());
                Shoot();
                yield return new WaitForSeconds(1f / fireRate);
            }
            else
            {
                PlayGunClickAudio(GetComponent<OutOfAmmoClickSound>());
                break;
            }
        }
    }

    IEnumerator Reload()
    {
        //Debug.Log("R");
        //Set isReloading to true.
        isReloading = true;

        //Calls PlayReloadAudio(GetComponent<ReloadSound>()) and gets the ReloadSound script that the method is referencing. 
        PlayReloadAudio(GetComponent<ReloadSound>());

        //Sets reloadingText to be enabled.
        reloadingText.SetActive(true);

        //Set the bool named Reloading in the animator to true.
        animator.SetBool("Reloading", true);

        //Wait to continue base on reloadTime.
        yield return new WaitForSeconds(reloadTime);

        //Set the bool named Reloading in the animator to false.
        animator.SetBool("Reloading", false);

        //Set currentAmmo equal to maxAmmoClip.
        currentAmmo = maxAmmoClip;

        //Substracts the vaule of maxAmmo by the vaule of maxAmmoClip.
        maxAmmo -= maxAmmoClip;

        //Set isReloading to false.
        isReloading = false;
    }

    void Scope()
    {
        //...set isScoping to the oppsite of what it currently is and the bool for the animator "Scoping" equal to isScoping.
        isScoping = !isScoping;
        animator.SetBool("Scoping", isScoping);
    }

    void OutOfAmmo()
    {
        //Sets outOfAmmo to true.
        outOfAmmo = true;

        //If currentAmmo is less than zero and outOfAmmo is true...
        /*if (currentAmmo <= 0 && outOfAmmo == true)
        {
            //...set outOfAmmoText in an active state.
            outOfAmmoText.SetActive(true);
        }*/
    }

    void OnEnable()
    {
        Player player = GetComponentInParent<Player>();

        if (player != null)
        {
            player.currentGun = this;
        }
    }

    void PlayShootAudio(ShootSound shootSound)
    {
        //Calls PlayShootAudio() from the ShootSound script which is called shootSound for reference.
        shootSound.PlaySound();
    }

    void PlayReloadAudio(ReloadSound reloadSound)
    {
        //Calls PlayReloadAudio() from the ShootSound script which is called shootSound for reference.
        reloadSound.PlaySound();
    }

    void PlayGunClickAudio(OutOfAmmoClickSound outOfAmmoClickSound)
    {
        //Calls PlayReloadAudio() from the ShootSound script which is called shootSound for reference.
        outOfAmmoClickSound.PlaySound();
    }
}
