using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FirstPersonMovement firstPersonMovement;
    private CharacterStats characterStats;
    private MovementSound movementSounds;
    public Gun currentGun;

    // Start is called before the first frame update
    void Start()
    {
        if(firstPersonMovement == null)
        {
            firstPersonMovement = GetComponent<FirstPersonMovement>();
        }

        if (characterStats == null)
        {
            characterStats = GetComponent<CharacterStats>();
        }

        if (movementSounds == null)
        {
            movementSounds = GetComponent<MovementSound>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EquipGun(GameObject gunObject)
    {
        currentGun = gunObject.GetComponent<Gun>();
    }
}
