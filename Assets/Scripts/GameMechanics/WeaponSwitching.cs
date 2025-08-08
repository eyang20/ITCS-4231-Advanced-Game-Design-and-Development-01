using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        //If the Mouse ScrollWheel is scrolled up...
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //...and if selectedWeapon is greater than or equal to transform.childCount minus 1...
            if (selectedWeapon >= transform.childCount - 1)
            {
                //...set selectedWeapon equal to 0.
                selectedWeapon = 0;
            }

            //...else...
            else
            {
                //...increase selectedWeapon by one.
                selectedWeapon++;
            }
        }

        //If the Mouse ScrollWheel is scrolled down...
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //...and if selectedWeapon is less than or equal to transform.childCount minus 1...
            if (selectedWeapon <= transform.childCount - 1)
            {
                //...set selectedWeapon equal to 0.
                selectedWeapon = 0;
            }

            //...else...
            else
            {
                //...decrease selectedWeapon by one.
                selectedWeapon--;
            }
        }

        //If the key one is pressed...
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //...set selectedWeapon equal to 0.
            selectedWeapon = 0;
        }

        //If the key two is pressed and the childCount of the transform is greater than or equal to two...
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            //...set selectedWeapon equal to 1.
            selectedWeapon = 1;
        }
        
        //If the key 3 is pressed and the childCount of the transform is greater than or equal to three...
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 2)
        {
            //...set selectedWeapon equal to 2.
            selectedWeapon = 2;
        }

        //If previousSelectedWeapon is NOT equal to selectedWeapon...
        if (previousSelectedWeapon != selectedWeapon)
        {
            //...call SelectWeapon().
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        //For each transform called weapon in this transform that the script is attached to...
        foreach(Transform weapon in transform)
        {
            //...if i is equal to the value of selectedWeapon...
            if (i == selectedWeapon)
            {
                //...identify weapon as a gameObject and set it in an active state...
                weapon.gameObject.SetActive(true);
            }

            //...else...
            else
            {
                //...identify weapon as a gameObject and set it in an inactive state. 
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
