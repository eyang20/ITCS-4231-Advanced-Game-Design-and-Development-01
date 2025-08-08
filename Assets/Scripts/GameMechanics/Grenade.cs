using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 500f;
    public float damage = 10f;

    public GameObject explosionEffect;
    public GameObject[] mesh;

    float countdown;

    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        //Set countdown equal to delay.
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        //Countdown is subtracted by Time.deltaTime.
        countdown -= Time.deltaTime;

        //If countdown is less than or equal to zero and hasExploded is NOT true...
        if (countdown <= 0 && !hasExploded)
        {
            //...call the method Explode() and set hasExploded to true.
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        if(explosionEffect != null)
        {
            //A GameObject called explosionGameObject is an instantiated explosionEffect at the transform's position and the transforms's rotation.
            GameObject explosionGameObject = Instantiate(explosionEffect, transform.position, transform.rotation);

            //Destroys the explosionGameObject after one second.
            Destroy(explosionGameObject, 1.5f);
        }

        //An array of Collider[] called collidersToMove is an OverlapSphere at the transform's position and the size of radius.
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        //foreach Collider called nearbyObject that is in the array of collidersToMove...
        foreach (Collider nearbyObject in collidersToMove)
        {
            //...set RigidBody(s) called rb to be nearbyObject in which nearbyObject get the component <RigidBody>() from collidersToMove and...
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            //...the transform of that target and the script <Target>() if it has it and...
            Target target = nearbyObject.transform.GetComponent<Target>();

            //...if rb DOES have a <RigidBody>()...
            if (rb != null)
            {
                //...add an explosion force by force from the transform's position with the size of radius.
                rb.AddExplosionForce(force, transform.position, radius);
            }

            //..if the script <Target>() is NOT null on the game object that was hit...
            if (target != null)
            {
                //...the game object calls the method TakeDamage() from the target causing the target to take damage to it's health value by (damage).
                target.TakeDamage(damage);
            }
        }

        //Plays audio from the script GrenadeExplosionSound.
        PlayAudio(GetComponent<GrenadeExplosionSound>());

        //For 
        for (int i = 0; i < mesh.Length; i++)
        {
            //...set all game objects in the array mesh to false.
            mesh[i].SetActive(false);
        }

        //Destroys the gameObject.
        Destroy(gameObject, 1f);
    }

    void PlayAudio(GrenadeExplosionSound grenadeExplosionSound)
    {
        //Calls PlaySound() from the ShootSound script which is called shootSound for reference.
        grenadeExplosionSound.PlaySound();
    }
}
