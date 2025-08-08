using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 10f;
    public float grenadeCount = 3f;
    public float grenadeCountMax = 5f;
    public float delayTime = 3f;

    public bool isThrowing = false;

    public GameObject grenadePrefab;

    // Update is called once per frame
    void Update()
    {
        //If isThrowing is true...
        if (isThrowing)
        {
            //...return to the top of the script.
            return;
        }

        //If the "g" is pressed and grenadeCount is greater than 0...
        if (Input.GetKeyDown("g") && grenadeCount > 0)
        {
            //...call ThrowGrenade().
            StartCoroutine(ThrowGrenade());
            return;
        }
    }

    IEnumerator ThrowGrenade()
    {
        //Set isThrowing to true.
        isThrowing = true;

        //Subtracts the vaule grenadeCount by one.
        grenadeCount--;

        //A GameObject called grenade is an instantiated grenadePrefab at the transform's position and the transform's rotation.
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);

        //A Rigidbody called rb is the <Rigidbody>() component from the GameObject grenade.
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        //Adds a force to rb in a forrward motion from the transform mulitplied by the vaule throwForce with the ForceMode being VelocityChange.
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        //Wait to continue base on delayTime.
        yield return new WaitForSeconds(delayTime);

        //Set isThrowing to false.
        isThrowing = false;
    }
}
